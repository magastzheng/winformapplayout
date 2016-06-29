using Config;
using Controls.Entity;
using Controls.GridView;
using Forms;
using Model;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DBAccess;
using Model.config;
using TradingSystem.TradeUtil;
using BLL.Entrust;

namespace TradingSystem.Dialog
{
    public partial class CancelRedoDialog : Forms.BaseFixedForm
    {
        private const string GridCancelRedoId = "entrustcanceladd";

        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();
        private EntrustSecurityDAO _entrustsecudao = new EntrustSecurityDAO();
        private EntrustBLL _entrustBLL = new EntrustBLL();

        private SortableBindingList<CancelRedoItem> _secuDataSource = new SortableBindingList<CancelRedoItem>(new List<CancelRedoItem>());
        private List<TradingCommandItem> _tradeCommandItems = new List<TradingCommandItem>();

        GridConfig _gridConfig;

        public CancelRedoDialog()
            : base()
        {
            InitializeComponent();
        }

        public CancelRedoDialog(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            LoadControl += new FormLoadHandler(Form_LoadControl);
            LoadData += new FormLoadHandler(Form_LoadData);

            this.btnSelectAll.Click += new EventHandler(Button_SelectAll_Click);
            this.btnUnSelectAll.Click += new EventHandler(Button_UnSelectAll_Click);
            this.btnConfirm.Click += new EventHandler(Button_Confirm_Click);
            this.btnCancel.Click += new EventHandler(Button_Cancel_Click);
        }

        #region loadcontrol

        private bool Form_LoadControl(object sender, object data)
        {
            //Load Command Trading
            TSDataGridViewHelper.AddColumns(this.secuGridView, _gridConfig.GetGid(GridCancelRedoId));
            Dictionary<string, string> gridColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(CancelRedoItem));
            TSDataGridViewHelper.SetDataBinding(this.secuGridView, gridColDataMap);
            this.secuGridView.DataSource = _secuDataSource;

            //Load combobox 
            LoadEntrustControl();

            return true;
        }

        private void LoadEntrustControl()
        {
            var spotPrices = ConfigManager.Instance.GetComboConfig().GetComboOption("spotprice");
            ComboBoxUtil.SetComboBox(this.cbSpotBuyPrice, spotPrices);

            var spotSellPrices = new ComboOption
            {
                Name = spotPrices.Name,
                Selected = spotPrices.Selected,
                Items = spotPrices.Items.OrderBy(p => p.Order2).ToList()
            };
            ComboBoxUtil.SetComboBox(this.cbSpotSellPrice, spotSellPrices);

            var futurePrice = ConfigManager.Instance.GetComboConfig().GetComboOption("futureprice");
            ComboBoxUtil.SetComboBox(this.cbFuturesBuyPrice, futurePrice);

            var futureSellPrices = new ComboOption
            {
                Name = futurePrice.Name,
                Selected = futurePrice.Selected,
                Items = futurePrice.Items.OrderBy(p => p.Order2).ToList()
            };

            ComboBoxUtil.SetComboBox(this.cbFuturesSellPrice, futureSellPrices);

            var shPrice = ConfigManager.Instance.GetComboConfig().GetComboOption("shanghaiexchangepricetype");
            ComboBoxUtil.SetComboBox(this.cbSHExchangePrice, shPrice);

            var szPrice = ConfigManager.Instance.GetComboConfig().GetComboOption("shenzhenexchangepricetype");
            ComboBoxUtil.SetComboBox(this.cbSZExchangePrice, szPrice);
        }

        #endregion

        #region loaddata

        private bool Form_LoadData(object sender, object data)
        {
            if (data == null)
            {
                return false;
            }

            if (!(data is List<TradingCommandItem>))
            {
                return false;
            }

            _secuDataSource.Clear();
            _tradeCommandItems = data as List<TradingCommandItem>;
            foreach (var cmdItem in _tradeCommandItems)
            { 
                //var entrustItems = _entrustcmddao.GetByEntrustStatus(
                var entrustSecuItems = _entrustsecudao.GetCancelRedo(cmdItem.CommandId);
                if (entrustSecuItems != null && entrustSecuItems.Count() > 0)
                {
                    foreach (var p in entrustSecuItems)
                    {
                        CancelRedoItem cancelRedoItem = new CancelRedoItem {
                            Selection = true,
                            CommandId = cmdItem.CommandId,
                            EntrustAmount = p.EntrustAmount,
                            EntrustDirection = p.EntrustDirection,
                            EntrustPrice = p.EntrustPrice,
                            SecuCode = p.SecuCode,
                            SecuType = p.SecuType,
                            EntrustNo = p.SubmitId,
                            CommandPrice = p.PriceType.ToString(),
                            ReportPrice = p.EntrustPrice,
                            LeftAmount = p.EntrustAmount - p.DealAmount,
                            ReportAmount = p.EntrustAmount,
                            DealAmount = p.DealAmount,
                            EntrustDate = p.EntrustDate,
                            SubmitId = p.SubmitId,
                        };

                        _secuDataSource.Add(cancelRedoItem);
                    }
                }
            }

            return true;
        }

        #endregion

        #region button click event handler

        private void Button_SelectAll_Click(object sender, EventArgs e)
        {
            _secuDataSource.Where(p => !p.Selection).ToList().ForEach(p => p.Selection = true);

            this.secuGridView.Invalidate();
        }

        private void Button_UnSelectAll_Click(object sender, EventArgs e)
        {
            _secuDataSource.Where(p => p.Selection).ToList().ForEach(p => p.Selection = false);

            this.secuGridView.Invalidate();
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            //Get the price type
            PriceType spotBuyPrice = PriceTypeUtil.GetPriceType(this.cbSpotBuyPrice);
            PriceType spotSellPrice = PriceTypeUtil.GetPriceType(this.cbSpotSellPrice);
            PriceType futureBuyPrice = PriceTypeUtil.GetPriceType(this.cbFuturesBuyPrice);
            PriceType futureSellPrice = PriceTypeUtil.GetPriceType(this.cbFuturesSellPrice);

            var selectItems = _secuDataSource.Where(p => p.Selection).ToList();
            var commandIds = selectItems.Select(p => p.CommandId).Distinct().ToList();
            //var entrustItems = selectItems
            //    .GroupBy(p => new { p.SubmitId, p.CommandId })
            //    .Select(g => g.First())
            //    .ToList();
            
            int ret = _entrustBLL.Cancel(selectItems);

            //Call the UFX to cancel

            //Update the EntrustCommand

        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
