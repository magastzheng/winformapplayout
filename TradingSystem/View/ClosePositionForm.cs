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

namespace TradingSystem.View
{
    public partial class ClosePositionForm : Forms.DefaultForm
    {
        private const string GridCloseId = "closeposition";
        private const string GridSecurityId = "closepositionsecurity";

        private GridConfig _gridConfig;
        private TradingInstanceDAO _tradeinstdao = new TradingInstanceDAO();
        private TradingInstanceSecurityDAO _tradeinstsecudao = new TradingInstanceSecurityDAO();
        private TemplateStockDAO _tempstockdao = new TemplateStockDAO();

        private SortableBindingList<ClosePositionItem> _instDataSource = new SortableBindingList<ClosePositionItem>(new List<ClosePositionItem>());
        private SortableBindingList<ClosePositionSecurityItem> _secuDataSource = new SortableBindingList<ClosePositionSecurityItem>(new List<ClosePositionSecurityItem>());

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

                    _secuDataSource.Add(closeSecuItem);
                }
            }

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


            this.closeGridView.DataSource = _instDataSource;
            this.securityGridView.DataSource = _secuDataSource;

            return true;
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            _instDataSource.Clear();
            _secuDataSource.Clear();

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
