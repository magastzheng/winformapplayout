using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Model.Data;
using Model;
using Model.SecurityInfo;

namespace TradingSystem.View
{
    public partial class ClosePositionForm : Forms.DefaultForm
    {
        private const string GridCloseId = "closeposition";
        private const string GridSecurityId = "closepositionsecurity";
        private const string GridCloseCmdId = "closepositioncmd";

        private GridConfig _gridConfig;
        private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private TradingInstanceSecurityDAO _tradeinstsecudao = new TradingInstanceSecurityDAO();
        private TemplateStockDAO _tempstockdao = new TemplateStockDAO();
        private StockTemplateDAO _tempdbdao = new StockTemplateDAO();
        private FuturesContractDAO _fcdbdao = new FuturesContractDAO();
        private SecurityInfoDAO _secudbdao = new SecurityInfoDAO();

        private SortableBindingList<ClosePositionItem> _instDataSource = new SortableBindingList<ClosePositionItem>(new List<ClosePositionItem>());
        private SortableBindingList<ClosePositionSecurityItem> _secuDataSource = new SortableBindingList<ClosePositionSecurityItem>(new List<ClosePositionSecurityItem>());
        private SortableBindingList<ClosePositionCmdItem> _cmdDataSource = new SortableBindingList<ClosePositionCmdItem>(new List<ClosePositionCmdItem>());

        private List<SecurityItem> _securityInfoList = new List<SecurityItem>();

        public ClosePositionForm()
            :base()
        {
            InitializeComponent();
        }

        public ClosePositionForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            this.closeGridView.UpdateRelatedDataGridHandler += new UpdateRelatedDataGrid(GridView_Close_UpdateRelatedDataGridHandler);
        }

        #region GridView UpdateRelated

        private void GridView_Close_UpdateRelatedDataGridHandler(UpdateDirection direction, int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= _instDataSource.Count)
                return;

            ClosePositionItem closeItem = _instDataSource[rowIndex];
            switch (direction)
            {
                case UpdateDirection.Select:
                    {
                        LoadSecurity(closeItem);
                        LoadCloseCommand(closeItem);
                    }
                    break;
                case UpdateDirection.UnSelect:
                    { 
                        //Remove the unselected security items
                        var secuItems = _secuDataSource.Where(p => p.InstanceId == closeItem.InstanceId).ToList();
                        if (secuItems != null && secuItems.Count() > 0)
                        {
                            foreach (var secuItem in secuItems)
                            {
                                _secuDataSource.Remove(secuItem);
                            }
                        }

                        var cmdItems = _cmdDataSource.Where(p => p.InstanceId == closeItem.InstanceId).ToList();
                        if (cmdItems != null && cmdItems.Count > 0)
                        {
                            foreach (var cmdItem in cmdItems)
                            {
                                _cmdDataSource.Remove(cmdItem);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void LoadSecurity(ClosePositionItem closeItem)
        {
            var secuItems = _tradeinstsecudao.Get(closeItem.InstanceId);
            var tempstockitems = _tempstockdao.Get(closeItem.TemplateId);

            if (secuItems != null && secuItems.Count > 0)
            {
                foreach (var secuItem in secuItems)
                {
                    ClosePositionSecurityItem closeSecuItem = new ClosePositionSecurityItem 
                    {
                        Selection = true,
                        InstanceId = secuItem.InstanceId,
                        SecuCode = secuItem.SecuCode,
                        SecuType = secuItem.SecuType,
                        
                        HoldingAmount = secuItem.PositionAmount,
                        AvailableAmount = secuItem.AvailableAmount,
                    };

                    var secuInfo = _securityInfoList.Find(p => p.SecuCode.Equals(closeSecuItem.SecuCode) && p.SecuType == closeSecuItem.SecuType);
                    if (secuInfo != null)
                    { 
                        closeSecuItem.SecuName = secuInfo.SecuName;
                        closeSecuItem.ExchangeCode = secuInfo.ExchangeCode;
                    }

                    _secuDataSource.Add(closeSecuItem);
                }
            }

        }

        private void LoadCloseCommand(ClosePositionItem closeItem)
        {
            ClosePositionCmdItem cmdItem = new ClosePositionCmdItem 
            {
                InstanceId = closeItem.InstanceId,
                InstanceCode = closeItem.InstanceCode,
                Selection = true,
                SpotTemplate = closeItem.TemplateId.ToString(),
                MonitorName = closeItem.MonitorName,
                TradeDirection = ((int)EntrustDirection.Buy).ToString()
            };

            if (_secuDataSource != null && _secuDataSource.Count > 0)
            {
                var futuresItem = _secuDataSource.First(p => p.SecuType == Model.SecurityInfo.SecurityType.Futures);
                if (futuresItem != null)
                {
                    cmdItem.FuturesContract = futuresItem.SecuCode;
                }
            }

            _cmdDataSource.Add(cmdItem);
        }

        #endregion

        #region load control
        private bool Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.closeGridView, _gridConfig.GetGid(GridCloseId));
            Dictionary<string, string> closeColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(ClosePositionItem));
            TSDataGridViewHelper.SetDataBinding(this.closeGridView, closeColDataMap);

            //set the securityGridView
            TSDataGridViewHelper.AddColumns(this.securityGridView, _gridConfig.GetGid(GridSecurityId));
            Dictionary<string, string> securityColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(ClosePositionSecurityItem));
            TSDataGridViewHelper.SetDataBinding(this.securityGridView, securityColDataMap);

            //set the command gridview
            TSDataGridViewHelper.AddColumns(this.cmdGridView, _gridConfig.GetGid(GridCloseCmdId));
            Dictionary<string, string> cmdColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(ClosePositionCmdItem));
            TSDataGridViewHelper.SetDataBinding(this.cmdGridView, cmdColDataMap);

            this.closeGridView.DataSource = _instDataSource;
            this.securityGridView.DataSource = _secuDataSource;
            this.cmdGridView.DataSource = _cmdDataSource;

            LoadCommandComboBoxOption();

            return true;
        }

        private void LoadCommandComboBoxOption()
        {
            var tradeDirectionOption = ConfigManager.Instance.GetComboConfig().GetComboOption("tradedirection");
            TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "tradedirection", tradeDirectionOption);

            var templates = _tempdbdao.Get(-1);
            if (templates != null && templates.Count > 0)
            {
                ComboOption tempOption = new ComboOption 
                {
                    Items = new List<ComboOptionItem>(),
                };

                foreach (var temp in templates)
                {
                    ComboOptionItem option = new ComboOptionItem 
                    {
                        Id = temp.TemplateId.ToString(),
                        Name = string.Format("{0} {1}",temp.TemplateId, temp.TemplateName)
                    };

                    tempOption.Items.Add(option);
                }

                TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "spottemplate", tempOption);
            }

            var futures = _fcdbdao.Get("");
            if (futures != null && futures.Count > 0)
            {
                ComboOption futuresOption = new ComboOption
                {
                    Items = new List<ComboOptionItem>(),
                };

                foreach (var future in futures)
                {
                    ComboOptionItem option = new ComboOptionItem 
                    {
                        Id = future.Code,
                        Name = future.Code
                    };

                    futuresOption.Items.Add(option);
                }

                TSDataGridViewHelper.SetDataBinding(this.cmdGridView, "futurescontract", futuresOption);
            }
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            _instDataSource.Clear();
            _secuDataSource.Clear();
            _cmdDataSource.Clear();
            _securityInfoList.Clear();

            _securityInfoList = _secudbdao.Get(SecurityType.All);

            var tradeInstances = _tradeinstdao.GetCombine(-1);
            if (tradeInstances != null && tradeInstances.Count > 0)
            {
                foreach (var instance in tradeInstances)
                {
                    ClosePositionItem closeItem = new ClosePositionItem 
                    {
                        InstanceId = instance.InstanceId,
                        InstanceCode = instance.InstanceCode,
                        MonitorId = instance.MonitorUnitId,
                        MonitorName = instance.MonitorUnitName,
                        TemplateId = instance.TemplateId,
                    };

                    _instDataSource.Add(closeItem);
                }
            }
            return true;
        }

        #endregion
    }
}
