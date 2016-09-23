using Config;
using Controls.Entity;
using Controls.GridView;
using Forms;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Model.config;
using TradingSystem.TradeUtil;
using Model.SecurityInfo;
using BLL.SecurityInfo;
using Model.EnumType;
using Model.Binding.BindingUtil;
using Quote;
using System.Text;
using Model.BLL;
using BLL.Frontend;

namespace TradingSystem.Dialog
{
    public partial class CancelRedoDialog : Forms.BaseDialog
    {
        private const string GridCancelRedoId = "entrustcancelredo";

        private EntrustBLL _entrustBLL = new EntrustBLL();
        private WithdrawBLL _withdrawBLL = new WithdrawBLL();

        private SortableBindingList<CancelRedoItem> _secuDataSource = new SortableBindingList<CancelRedoItem>(new List<CancelRedoItem>());
        //private List<EntrustCommandItem> _entrustCommandItems = new List<EntrustCommandItem>();

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


            this.cbSpotBuyPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbSpotSellPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbFuturesBuyPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbFuturesSellPrice.SelectedIndexChanged += new EventHandler(ComboBox_PriceType_SelectedIndexChange);
            this.cbSHExchangePrice.SelectedIndexChanged += new EventHandler(ComboBox_SHExchangePrice_SelectedIndexChanged);
            this.cbSZExchangePrice.SelectedIndexChanged += new EventHandler(ComboBox_SZExchangePrice_SelectedIndexChanged);
        }

        #region price type

        private void ComboBox_PriceType_SelectedIndexChange(object sender, EventArgs e)
        {
            if (sender == null || !(sender is ComboBox))
                return;
            ComboBox cb = sender as ComboBox;

            PriceType priceType = PriceTypeHelper.GetPriceType(cb);
            EntrustDirection direction = EntrustDirection.BuySpot;
            SecurityType secuType = SecurityType.All;
            switch (cb.Name)
            {
                case "cbSpotBuyPrice":
                    {
                        direction = EntrustDirection.BuySpot;
                        secuType = SecurityType.Stock;
                    }
                    break;
                case "cbSpotSellPrice":
                    {
                        direction = EntrustDirection.SellSpot;
                        secuType = SecurityType.Stock;
                    }
                    break;
                case "cbFuturesBuyPrice":
                    {
                        direction = EntrustDirection.BuyClose;
                        secuType = SecurityType.Futures;
                    }
                    break;
                case "cbFuturesSellPrice":
                    {
                        direction = EntrustDirection.SellOpen;
                        secuType = SecurityType.Futures;
                    }
                    break;
                default:
                    break;
            }

            var stockItems = _secuDataSource.Where(p => p.EDirection == direction && p.SecuType == secuType).ToList();
            if (stockItems.Count > 0)
            {
                stockItems.ForEach(p => { p.EPriceSetting = priceType; });

                //TODO: update the price by setting

                //query the price and set it
                List<SecurityItem> secuList = new List<SecurityItem>();
                var uniqueSecuItems = _secuDataSource.GroupBy(p => p.SecuCode).Select(p => p.First());
                foreach (var secuItem in uniqueSecuItems)
                {
                    var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                    secuList.Add(findItem);
                }

                List<PriceType> priceTypes = new List<PriceType>() { priceType };
                QuoteCenter.Instance.Query(secuList, priceTypes);
                foreach (var secuItem in stockItems)
                {
                    var targetItem = secuList.Find(p => p.SecuCode.Equals(secuItem.SecuCode) && (p.SecuType == SecurityType.Stock || p.SecuType == SecurityType.Futures));
                    var marketData = QuoteCenter.Instance.GetMarketData(targetItem);
                    secuItem.EntrustPrice = QuotePriceHelper.GetPrice(priceType, marketData);
                }
            }

            this.secuGridView.Invalidate();
        }

        #endregion

        #region market price type

        private void ComboBox_SHExchangePrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntrustPriceType priceType = EntrustPriceTypeHelper.GetPriceType(this.cbSHExchangePrice);
            _secuDataSource.Where(p => p.SecuType == SecurityType.Stock && p.ExchangeCode.Equals("SSE"))
                .ToList()
                .ForEach(o => o.EEntrustPriceType = priceType);

            this.secuGridView.Invalidate();
        }

        private void ComboBox_SZExchangePrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntrustPriceType priceType = EntrustPriceTypeHelper.GetPriceType(this.cbSZExchangePrice);

            _secuDataSource.Where(p => p.SecuType == SecurityType.Stock && p.ExchangeCode.Equals("SZSE"))
               .ToList()
               .ForEach(o => o.EEntrustPriceType = priceType);

            this.secuGridView.Invalidate();
        }

        #endregion

        #region loadcontrol

        private bool Form_LoadControl(object sender, object data)
        {
            //Load Command Trading
            TSDataGridViewHelper.AddColumns(this.secuGridView, _gridConfig.GetGid(GridCancelRedoId));
            Dictionary<string, string> gridColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(CancelRedoItem));
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

            if (!(data is List<CancelRedoItem>))
            {
                return false;
            }

            //Get the price type
            PriceType spotBuyPrice = PriceTypeHelper.GetPriceType(this.cbSpotBuyPrice);
            PriceType spotSellPrice = PriceTypeHelper.GetPriceType(this.cbSpotSellPrice);
            PriceType futureBuyPrice = PriceTypeHelper.GetPriceType(this.cbFuturesBuyPrice);
            PriceType futureSellPrice = PriceTypeHelper.GetPriceType(this.cbFuturesSellPrice);
            EntrustPriceType shPriceType = EntrustPriceTypeHelper.GetPriceType(this.cbSHExchangePrice);
            EntrustPriceType szPriceType = EntrustPriceTypeHelper.GetPriceType(this.cbSZExchangePrice);

            _secuDataSource.Clear();
            var cancelSecuItems = data as List<CancelRedoItem>;
            foreach (var cancelRedoItem in cancelSecuItems)
            {
                if (cancelRedoItem.ExchangeCode.Equals("SZSE"))
                {
                    cancelRedoItem.EEntrustPriceType = szPriceType;
                }
                else if (cancelRedoItem.ExchangeCode.Equals("SSE"))
                {
                    cancelRedoItem.EEntrustPriceType = shPriceType;
                }
                else
                {
                    cancelRedoItem.EEntrustPriceType = cancelRedoItem.EOriginPriceType;
                }

                if (cancelRedoItem.SecuType == SecurityType.Stock && cancelRedoItem.EDirection == EntrustDirection.BuySpot)
                {
                    cancelRedoItem.EPriceSetting = spotBuyPrice;
                }
                else if (cancelRedoItem.SecuType == SecurityType.Stock && cancelRedoItem.EDirection == EntrustDirection.BuySpot)
                {
                    cancelRedoItem.EPriceSetting = spotSellPrice;
                }
                else if (cancelRedoItem.SecuType == SecurityType.Futures && cancelRedoItem.EDirection == EntrustDirection.SellOpen)
                {
                    cancelRedoItem.EPriceSetting = futureSellPrice;
                }
                else if (cancelRedoItem.SecuType == SecurityType.Futures && cancelRedoItem.EDirection == EntrustDirection.BuyClose)
                {
                    cancelRedoItem.EPriceSetting = futureBuyPrice;
                }

                _secuDataSource.Add(cancelRedoItem);
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
            string outMsg = string.Empty;
            //if (!ValidateEntrustSecurities(_entrustCommandItems, out outMsg))
            //{
            //    string msg = string.Format("证券未勾选或勾选证券均未设置委托数量, [交易指令;提交号]为: {0}", outMsg);
            //    MessageBox.Show(this, msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            var selectItems = _secuDataSource.Where(p => p.Selection).ToList();

            var commandIds = selectItems.Select(p => p.CommandId).Distinct().ToList();
            var submitIds = selectItems.Select(p => p.SubmitId).Distinct().ToList();
            var failedCancelItems = new List<CancelRedoItem>();
            var successCancelItems = new List<CancelRedoItem>(); 
            foreach (var commandId in commandIds)
            {
                foreach (var submitId in submitIds)
                {
                    var secuItems = selectItems.Where(p => p.Selection && p.CommandId == commandId && p.SubmitId == submitId).ToList();
                    var cancelItems = _withdrawBLL.CancelSecuItem(submitId, commandId, secuItems, null);
                    if (cancelItems.Count > 0)
                    {
                        successCancelItems.AddRange(cancelItems);
                    }

                    if (cancelItems.Count != secuItems.Count)
                    {
                        //TODO: report failed items
                    }
                }
            }

            //Get the price type
            PriceType spotBuyPrice = PriceTypeHelper.GetPriceType(this.cbSpotBuyPrice);
            PriceType spotSellPrice = PriceTypeHelper.GetPriceType(this.cbSpotSellPrice);
            PriceType futureBuyPrice = PriceTypeHelper.GetPriceType(this.cbFuturesBuyPrice);
            PriceType futureSellPrice = PriceTypeHelper.GetPriceType(this.cbFuturesSellPrice);

            //var commandIds = selectItems.Select(p => p.CommandId).Distinct().ToList();
            foreach (var commandId in commandIds)
            {
                var oneCancelRedoItem = successCancelItems.Where(p => p.CommandId == commandId).ToList();
                if (oneCancelRedoItem.Count > 0)
                {
                    Submit(commandId, oneCancelRedoItem);
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region

        private bool ValidateEntrustSecurities(List<EntrustCommandItem> cmdItems, out string msg)
        {
            msg = string.Empty;
            List<EntrustCommandItem> invalidItems = new List<EntrustCommandItem>();
            List<CancelRedoItem> cancelItems = new List<CancelRedoItem>();

            foreach (var cmdItem in cmdItems)
            {
                var selectedItems = _secuDataSource.Where(p => p.Selection && p.CommandId == cmdItem.CommandId && p.SubmitId == cmdItem.SubmitId).ToList();
                if (selectedItems.Count > 0)
                {
                    //选中的委托数量不能为0
                    var zeroAmountItems = selectedItems.Where(p => p.EntrustAmount == 0).ToList();
                    if (zeroAmountItems.Count > 0)
                    {
                        invalidItems.Add(cmdItem);
                    }
                    else
                    {
                        cancelItems.AddRange(selectedItems);
                    }
                }
                else
                {
                    invalidItems.Add(cmdItem);
                }
            }

            if (cancelItems.Count == 0)
            {
                StringBuilder sb = new StringBuilder();
                invalidItems.ForEach(p =>
                {
                    sb.Append("|");
                    sb.Append(p.CommandId);
                    sb.Append(";");
                    sb.Append(p.SubmitId);
                });
                sb.Append("|");

                msg = sb.ToString();

                return false;
            }
            else
            {
                return true;
            }
        }

        //private void CancelRedo(int submitId, List<CancelRedoItem> cancelRedoItems)
        //{
        //    //var submitId = cancelRedoItems.Select(p => p.SubmitId).Single();
        //    //if (submitId <= 0)
        //    //{
        //    //    //TODO: fail to get the submitId
        //    //    return;
        //    //}
        //    //set the cancel status
        //    //int ret = _entrustBLL.Cancel(cancelRedoItems);
            
        //    //Call the UFX to cancel

        //    //Update the EntrustCommand
        //    //_entrustBLL.Submit(

        //    //ret = _entrustBLL.CancelSuccess(cancelRedoItems);
        //}


        //TODO: validate before submit
        private void Submit(int commandId, List<CancelRedoItem> cancelRedoItems)
        {
            EntrustCommandItem cmdItem = new EntrustCommandItem 
            {
                CommandId = commandId,
                Copies = 0,
            };
            var response = _entrustBLL.SubmitOne(cmdItem, cancelRedoItems);
            if (!BLLResponse.Success(response))
            { 
                int submitId = cancelRedoItems.Select(p => p.SubmitId).Distinct().Single();
                string msg = string.Format("委托失败, 提交号：[{0}], 失败原因：{1}!", submitId, response.Message);
                MessageBox.Show(this, msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
