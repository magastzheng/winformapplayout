﻿using Config;
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
using Model.EnumType;
using Model.Binding.BindingUtil;
using Quote;
using System.Text;
using Model.BLL;
using BLL.Frontend;
using Model.Database;
using BLL.Manager;
using UFX.impl;

namespace TradingSystem.Dialog
{
    public partial class CancelRedoDialog : Forms.BaseDialog
    {
        private const string GridCancelRedoId = "entrustcancelredo";

        private const string msgNoSecuritySelected = "nosecurityselected";
        private const string msgEntrustCancelNoAmount = "entrustcancelnoamount";
        private const string msgEntrustCancelRedoConfirm = "entrustcancelredoconfirm";
        private const string msgEntrustCancelPartialFail = "entrustcancelpartialfail";
        private const string msgEntrustCancelFailSecurity = "entrustcancelfailsecurity";
        private const string msgEntrustCancelResubmitFail = "entrustcancelresubmitfail";


        private EntrustBLL _entrustBLL = new EntrustBLL();
        private WithdrawBLL _withdrawBLL = new WithdrawBLL();

        private SortableBindingList<CancelRedoItem> _secuDataSource = new SortableBindingList<CancelRedoItem>(new List<CancelRedoItem>());
        
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
            EntrustPriceType shPriceType = EntrustPriceTypeHelper.GetPriceType(this.cbSHExchangePrice);
            EntrustPriceType szPriceType = EntrustPriceTypeHelper.GetPriceType(this.cbSZExchangePrice);

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
                stockItems.ForEach(p => 
                { 
                    p.EPriceSetting = priceType;
                    SetEntrustPriceType(p, shPriceType, szPriceType);
                });
            }

            //update the quote
            QueryQuote();

            this.secuGridView.Invalidate();
        }

        #endregion

        #region market price type

        private void ComboBox_SHExchangePrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntrustPriceType priceType = EntrustPriceTypeHelper.GetPriceType(this.cbSHExchangePrice);
            _secuDataSource.Where(p => p.SecuType == SecurityType.Stock
                && Exchange.SHSE.Equals(p.ExchangeCode)
                && p.EPriceSetting == PriceType.Market
                )
                .ToList()
                .ForEach(o => o.EEntrustPriceType = priceType);

            this.secuGridView.Invalidate();
        }

        private void ComboBox_SZExchangePrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntrustPriceType priceType = EntrustPriceTypeHelper.GetPriceType(this.cbSZExchangePrice);

            _secuDataSource.Where(p => p.SecuType == SecurityType.Stock 
                && Exchange.SZSE.Equals(p.ExchangeCode)
                && p.EPriceSetting == PriceType.Market
                )
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
            var setting = SettingManager.Instance.Get();

            var shPrice = ConfigManager.Instance.GetComboConfig().GetComboOption("shanghaiexchangepricetype");
            ComboBoxUtil.SetComboBox(this.cbSHExchangePrice, shPrice, setting.EntrustSetting.SseEntrustPriceType.ToString());

            var szPrice = ConfigManager.Instance.GetComboConfig().GetComboOption("shenzhenexchangepricetype");
            ComboBoxUtil.SetComboBox(this.cbSZExchangePrice, szPrice, setting.EntrustSetting.SzseEntrustPriceType.ToString());

            var spotPrices = ConfigManager.Instance.GetComboConfig().GetComboOption("spotprice");
            ComboBoxUtil.SetComboBox(this.cbSpotBuyPrice, spotPrices, setting.EntrustSetting.BuySpotPrice.ToString());

            var spotSellPrices = new ComboOption
            {
                Name = spotPrices.Name,
                Selected = spotPrices.Selected,
                Items = spotPrices.Items.OrderBy(p => p.Order2).ToList()
            };
            ComboBoxUtil.SetComboBox(this.cbSpotSellPrice, spotSellPrices, setting.EntrustSetting.SellSpotPrice.ToString());

            var futurePrice = ConfigManager.Instance.GetComboConfig().GetComboOption("futureprice");
            ComboBoxUtil.SetComboBox(this.cbFuturesBuyPrice, futurePrice, setting.EntrustSetting.BuyFutuPrice.ToString());

            var futureSellPrices = new ComboOption
            {
                Name = futurePrice.Name,
                Selected = futurePrice.Selected,
                Items = futurePrice.Items.OrderBy(p => p.Order2).ToList()
            };

            ComboBoxUtil.SetComboBox(this.cbFuturesSellPrice, futureSellPrices, setting.EntrustSetting.SellFutuPrice.ToString());
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

                SetEntrustPriceType(cancelRedoItem, shPriceType, szPriceType);

                _secuDataSource.Add(cancelRedoItem);
            }

            //update the quote
            QueryQuote();
            
            return true;
        }

        private void SetEntrustPriceType(CancelRedoItem cancelRedoItem, EntrustPriceType shPriceType, EntrustPriceType szPriceType)
        {
            //只有选择市价时设置市价委托方式
            if (cancelRedoItem.SecuType == SecurityType.Stock && cancelRedoItem.EPriceSetting == PriceType.Market)
            {
                if (Exchange.SHSE.Equals(cancelRedoItem.ExchangeCode))
                {
                    cancelRedoItem.EEntrustPriceType = shPriceType;
                }
                else if (Exchange.SZSE.Equals(cancelRedoItem.ExchangeCode))
                {
                    cancelRedoItem.EEntrustPriceType = szPriceType;
                }
                else
                {
                    //do nothing
                }
            }
            else
            {
                cancelRedoItem.EEntrustPriceType = EntrustPriceType.FixedPrice;
            }
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
            var selectItems = _secuDataSource.Where(p => p.Selection).ToList();
            if (selectItems.Count == 0)
            {
                MessageDialog.Warn(this, msgNoSecuritySelected);
                return;
            }

            if (!ValidateEntrustSecurities(selectItems, out outMsg))
            {
                MessageDialog.Warn(this, outMsg);
                return;
            }

            if (MessageDialog.Info(this, msgEntrustCancelRedoConfirm, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

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
                        var failItems = secuItems.Except(cancelItems);
                        failedCancelItems.AddRange(failItems);
                    }
                }
            }

            //Get the price type
            PriceType spotBuyPrice = PriceTypeHelper.GetPriceType(this.cbSpotBuyPrice);
            PriceType spotSellPrice = PriceTypeHelper.GetPriceType(this.cbSpotSellPrice);
            PriceType futureBuyPrice = PriceTypeHelper.GetPriceType(this.cbFuturesBuyPrice);
            PriceType futureSellPrice = PriceTypeHelper.GetPriceType(this.cbFuturesSellPrice);

            string resultMsg = string.Empty;
            foreach (var commandId in commandIds)
            {
                var oneCancelRedoItem = successCancelItems.Where(p => p.CommandId == commandId).ToList();
                if (oneCancelRedoItem.Count > 0)
                {
                    resultMsg += Submit(commandId, oneCancelRedoItem, null);
                }
            }

            StringBuilder sb = new StringBuilder();
            if (failedCancelItems.Count > 0)
            {
                string format1 = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgEntrustCancelPartialFail);
                string format2 = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgEntrustCancelFailSecurity);
                sb.Append(format1);
                failedCancelItems.ForEach(p => {
                    sb.AppendFormat(format2, p.SubmitId, p.CommandId, p.SecuCode);
                });
            }

            if (!string.IsNullOrEmpty(resultMsg))
            {
                sb.Append(resultMsg);
            }

            if (sb.Length > 0)
            {
                MessageDialog.Warn(this, sb.ToString());
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region

        private bool ValidateEntrustSecurities(List<CancelRedoItem> cancelRedoItems, out string msg)
        {
            msg = string.Empty;
            List<CancelRedoItem> InvalidCancelItems = new List<CancelRedoItem>();

            //TODO: 未成交数量需要大于0
            //只能撤销存在未成交的证券(委托数量减去成交数量大于0)
            foreach (var cancelRedoItem in cancelRedoItems)
            {
                //选中的委托数量不能为0
                if(cancelRedoItem.EntrustAmount == 0 || (cancelRedoItem.EntrustAmount - cancelRedoItem.DealAmount) == 0)
                {
                    InvalidCancelItems.Add(cancelRedoItem);
                }

                //if ((cancelRedoItem.EntrustAmount - cancelRedoItem.DealAmount) > 0)
                //{
                //    InvalidCancelItems.Add(cancelRedoItem);
                //}
            }

            if (InvalidCancelItems.Count > 0)
            {
                string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgEntrustCancelNoAmount);
                StringBuilder sb = new StringBuilder();
                sb.Append(format);
                InvalidCancelItems.ForEach(p =>
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
        private string Submit(int commandId, List<CancelRedoItem> cancelRedoItems, CallerCallback callback)
        {
            EntrustCommand cmdItem = new EntrustCommand 
            {
                CommandId = commandId,
                Copies = 0,
            };

            string msg = string.Empty;
            var response = _entrustBLL.SubmitOne(cmdItem, cancelRedoItems, callback);
            if (!BLLResponse.Success(response))
            {
                var submitIds = cancelRedoItems.Select(p => p.SubmitId).Distinct().ToList();
                var submitIdStr = string.Join(",", submitIds.ConvertAll(p => p.ToString()));

                string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgEntrustCancelResubmitFail);
                msg = string.Format(format, submitIdStr, response.Message);
            }

            return msg;
        }

        #endregion

        #region query quote 

        private void QueryQuote()
        {
            //query the price and set it
            List<SecurityItem> secuList = new List<SecurityItem>();
            var uniqueSecuItems = _secuDataSource.GroupBy(p => p.SecuCode).Select(p => p.First());
            foreach (var secuItem in uniqueSecuItems)
            {
                var findItem = SecurityInfoManager.Instance.Get(secuItem.SecuCode, secuItem.SecuType);
                secuList.Add(findItem);
            }

            foreach (var cancelRedoItem in _secuDataSource)
            {
                var findItem = secuList.Find(p => p.SecuCode.Equals(cancelRedoItem.SecuCode) && p.SecuType == cancelRedoItem.SecuType);
                if (findItem != null)
                {
                    SetItemPrice(cancelRedoItem, findItem, cancelRedoItem.EPriceSetting);
                }
            }

        }

        private void SetItemPrice(CancelRedoItem item, SecurityItem secuItem, PriceType priceType)
        {
            var marketData = QuoteCenter.Instance.GetMarketData(secuItem);
            item.EntrustPrice = QuotePriceHelper.GetPrice(priceType, marketData);
            item.LastPrice = marketData.CurrentPrice;
            item.LimitDownPrice = marketData.LowLimitPrice;
            item.LimitUpPrice = marketData.HighLimitPrice;
        }

        #endregion
    }
}
