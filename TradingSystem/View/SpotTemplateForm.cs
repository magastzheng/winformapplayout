﻿using Calculation;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.config;
using Model.Data;
using Model.SecurityInfo;
using Model.UI;
using Quote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Util;
using System.Linq;
using BLL.Template;
using Model.Binding.BindingUtil;
using BLL.SecurityInfo;
using TradingSystem.Dialog;
using BLL.Permission;
using BLL.Manager;
using log4net;
using BLL.Archive.Template;

namespace TradingSystem.View
{
    public partial class SpotTemplateForm : Forms.DefaultForm
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public enum TempChangeType
        {
            New = 0,
            Update = 1,
        }

        private const string msgDeleteSecurity = "tempdeletesecurity";
        private const string msgSaveSecurity = "tempsavesecurity";
        private const string msgDeleteTemp = "tempdelete";
        private const string msgDeleteTempSuccess = "tempdeletesuccess";
        private const string msgModifySuccess = "tempmodifysuccess";
        private const string msgModifyFail = "tempmodifyfail";
        private const string msgCopyFail = "tempcopyfail";
        private const string msgSecurityCopyFail = "tempsecuritycopyfail";
        private const string msgDeleteSecuritySelect = "tempdeletesecurityselect";
        private const string msgAddTempSelect = "tempaddselect";
        private const string msgInvalidSelect = "tempinvalidselect";
        private const string msgSecurityModifySelect = "tempsecuritymodifyselect";
        private const string msgSecurityDeleteSuccess = "tempsecuritydeletesuccess";
        private const string msgSecurityDeleteFail = "tempsecuritydeletefail";
        private const string msgSecurityModifyOnlyOnce = "tempsecuritymodifyonlyonce";
        private const string msgCannotAddSameSecurity = "tempcannotaddsamesecurity";
        private const string msgSecurityZeroWeight = "tempsecurityzeroweight";
        private const string msgImportInvalidFileType = "tempimportinvalidfiletype";
        private const string msgImportLoadFail = "tempimportloadfail";

        private GridConfig _gridConfig = null;
        private const string GridTemplate = "stocktemplate";
        private const string GridStock = "templatestock";

        private TemplateBLL _templateBLL = new TemplateBLL();
        private HistTemplateBLL _histTemplateBLL = new HistTemplateBLL();
        private BenchmarkBLL _benchmarkBLL = new BenchmarkBLL();
        private PermissionManager _permissionManager = new PermissionManager();
        
        private SortableBindingList<StockTemplate> _tempDataSource = new SortableBindingList<StockTemplate>(new List<StockTemplate>());
        private SortableBindingList<TemplateStock> _spotDataSource = new SortableBindingList<TemplateStock>(new List<TemplateStock>());
        private List<SecurityItem> _securityInfoList = new List<SecurityItem>();
        private List<Benchmark> _benchmarkList = new List<Benchmark>();

        private bool _isTempStockChanged = false;
        private int _currentTemplateIndex = -1;

        public SpotTemplateForm()
            :base()
        {
            InitializeComponent();
        }

        public SpotTemplateForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.tsbSave.Enabled = false;

            this.tsbAdd.Click += new System.EventHandler(this.ToolStripButton_AddTemplate_Click);
            this.tsbModify.Click += new System.EventHandler(this.ToolStripButton_ModifyTemplate_Click);
            this.tsbCopy.Click += new EventHandler(this.ToolStripButton_CopyTemplate_Click);
            this.tsbDelete.Click += new EventHandler(this.ToolStripButton_DeleteTemplate_Click);
            this.tsbImport.Click += new System.EventHandler(this.ToolStripButton_Import_Click);
            this.tsbAddStock.Click += new System.EventHandler(ToolStripButton_AddStock_Click);
            this.tsbModifyStock.Click += new System.EventHandler(ToolStripButton_ModifyStock_Click);
            this.tsbDeleteStock.Click += new System.EventHandler(ToolStripButton_DeleteStock_Click);
            this.tsbSave.Click += new System.EventHandler(ToolStripButton_Save_Click);
            this.tsbCalcAmount.Click += new EventHandler(ToolStripButton_CalcAmount_Click);
            this.tsbExport.Click += new EventHandler(ToolStripButton_Export_Click);

            tempGridView.MouseClickRow += new ClickRowHandler(GridView_Template_MouseClickRow);
            tempGridView.MouseDoubleClick += new MouseDoubleClickHandler(GridView_Template_MouseDoubleClick);

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            //添加右键点击事件
            secuGridView.MouseClick += new MouseEventHandler(SecurityGridView_MouseClick);

            secuContextMenu.ItemClicked += new ToolStripItemClickedEventHandler(SecurityContextMenu_ItemClicked);
        }

        #region 右键菜单处理

        //right-click popup menu
        private void SecurityGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.secuContextMenu.Show(this.secuGridView, e.Location);
            }
        }

        private void SecurityContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "selectAllToolStripMenuItem":
                    {
                        _spotDataSource.ToList().ForEach(p => p.Selection = true);
                    }
                    break;
                case "unSelectToolStripMenuItem":
                    {
                        _spotDataSource.ToList().ForEach(p => p.Selection = false);
                    }
                    break;
                case "cancelSelectToolStripMenuItem":
                    {
                        _spotDataSource.Where(p => p.Selection).ToList().ForEach(p => p.Selection = false);
                    }
                    break;
                case "removeZeroToolStripMenuItem":
                    {
                        var selectedStocks = _spotDataSource.Where(p => p.Amount == 0).ToList();
                        foreach (var stock in selectedStocks)
                        {
                            _spotDataSource.Remove(stock);
                        }

                        this._isTempStockChanged = true;
                    }
                    break;
                default:
                    break;
            }

            this.secuGridView.Invalidate();
        }

        #endregion

        private void SwitchTemplateStockSave(bool isEnable)
        {
            this._isTempStockChanged = isEnable;
            this.tsbSave.Enabled = this._isTempStockChanged;
        }

        #region load control
        
        private bool Form_LoadControl(object sender, object data)
        {
            //set the tempGridView
            TSDataGridViewHelper.AddColumns(this.tempGridView, _gridConfig.GetGid(GridTemplate));
            Dictionary<string, string> tempColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(StockTemplate));
            TSDataGridViewHelper.SetDataBinding(this.tempGridView, tempColDataMap);

            //set the secuGridView
            TSDataGridViewHelper.AddColumns(this.secuGridView, _gridConfig.GetGid(GridStock));
            Dictionary<string, string> securityColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(TemplateStock));
            TSDataGridViewHelper.SetDataBinding(this.secuGridView, securityColDataMap);

            this.tempGridView.DataSource = _tempDataSource;
            this.secuGridView.DataSource = _spotDataSource;

            return true;
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            LoadAllData();

            return true;
        }

        private void LoadAllData()
        {
            _tempDataSource.Clear();
            _spotDataSource.Clear();

            var items = _templateBLL.GetTemplates();
            if (items != null)
            {
                foreach (var item in items)
                {
                    _tempDataSource.Add(item);
                }

                if (items.Count > 0)
                {
                    LoadTemplateStock(items[0].TemplateId);
                }
            }

            _securityInfoList = SecurityInfoManager.Instance.Get();
            _benchmarkList = _benchmarkBLL.GetAll();

            SetCurrentTemplate();
        }

        private void LoadTemplateStock(int templateNo)
        {
            if (templateNo < 0)
                return;

            _spotDataSource.Clear();
            var stocks = _templateBLL.GetStocks(templateNo);
            if (stocks != null)
            {
                foreach (var stock in stocks)
                {
                    _spotDataSource.Add(stock);
                }

                SummaryTemplateStock();
            }
        }

        private void SummaryTemplateStock()
        {
            int number = _spotDataSource.Count;
            double totalCap = _spotDataSource.Sum(p => p.MarketCap);
            double totalWeight = _spotDataSource.Sum(p => p.SettingWeight);

            tbNumber.Text = string.Format("{0}", number);
            tbTotalCap.Text = string.Format("{0}", totalCap);
            tbSettingWeight.Text = string.Format("{0}", totalWeight);
        }

        #endregion

        #region switch the top toolbar

        private bool SwitchToolBar(int templateId)
        {
            int userId = LoginManager.Instance.GetUserId();
            //var template = GetSelectTemplate();

            bool hasPerm = _permissionManager.HasPermission(userId, templateId, Model.Permission.ResourceType.SpotTemplate, Model.Permission.PermissionMask.Edit);

            this.tsbModify.Enabled = hasPerm;
            this.tsbDelete.Enabled = hasPerm;

            return true;
        }

        #endregion

        #region GridView event handler

        private void GridView_Template_MouseClickRow(object sender, int rowIndex)
        {
            //检查点击事件是否触发在有效的表各项中
            if (rowIndex < 0 || rowIndex >= _tempDataSource.Count)
                return;

            //如果之前的现货模板有改动，提示保存
            if (this._isTempStockChanged)
            {
                if (MessageDialog.Warn(this, msgSaveSecurity, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    SwitchTemplateStockSave(false);
                }
            }

            var template = _tempDataSource[rowIndex];

            if (template != null && template.TemplateId > 0)
            {
                SwitchToolBar(template.TemplateId);
                LoadTemplateStock(template.TemplateId);
            }

            SetCurrentTemplate();
        }

        private void GridView_Template_MouseDoubleClick(object sender, int rowIndex, int columnIndex)
        {
            //检查点击事件是否触发在有效的表各项中
            if (rowIndex < 0 || rowIndex >= _tempDataSource.Count)
                return;

            StockTemplate template = _tempDataSource[rowIndex];
            ModifyTemplate(template);
        }

        #endregion

        #region button click

        private void ToolStripButton_AddTemplate_Click(object sender, System.EventArgs e)
        {
            TemplateDialog dialog = new TemplateDialog();
            dialog.SaveData += new FormSaveHandler(Dialog_NewTemplate);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, null);
            dialog.ShowDialog();

            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                dialog.Dispose();
            }
            else
            {
                dialog.Dispose();
            }
        }

        private void ToolStripButton_ModifyTemplate_Click(object sender, System.EventArgs e)
        {
            StockTemplate stockTemplate = GetSelectTemplate();
            if (stockTemplate == null)
                return;

            ModifyTemplate(stockTemplate);
        }

        private void ToolStripButton_CopyTemplate_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            if (template == null)
                return;

            string templateName = string.Format("copy_{0}", template.TemplateName);
            StockTemplate temp = new StockTemplate
            {
                TemplateName = templateName,
                FutureCopies = template.FutureCopies,
                MarketCapOpt = template.MarketCapOpt,
                EReplaceType = template.EReplaceType,
                EWeightType = template.EWeightType,
                Benchmark = template.Benchmark,
                CreatedUserId = LoginManager.Instance.GetUserId(),
                EStatus = template.EStatus,
                DCreatedDate = DateTime.Now,
                CanEditUsers = template.CanEditUsers,
                CanViewUsers = template.CanViewUsers,
                Permissions = template.Permissions,
            };

            int newTemplateId = _templateBLL.Copy(template.TemplateId, temp);
            if (newTemplateId > 0)
            {
                temp.TemplateId = newTemplateId;
                _tempDataSource.Add(temp);
                //如果需要切换到新的模板，可以在此处理
            }
            else
            {
                MessageDialog.Fail(this, msgCopyFail);
            }

            this.tempGridView.Invalidate();
        }

        private void ToolStripButton_DeleteTemplate_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            if (template == null)
                return;

            //archive before deleting
            ArchiveTemplate(template);

            //delete
            int ret = _templateBLL.DeleteTemplate(template);
            if (ret == 1)
            {
                _tempDataSource.Remove(template);
                tempGridView.Invalidate();

                MessageDialog.Info(this, msgDeleteTempSuccess);
            }
            else
            {
                MessageDialog.Warn(this, msgDeleteTemp);
            }
        }

        #endregion

        #region Template change method

        private void ModifyTemplate(StockTemplate template)
        {
            TemplateDialog dialog = new TemplateDialog();
            dialog.SaveData += new FormSaveHandler(Dialog_ModifyTemplate);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, template);
            dialog.ShowDialog();
            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                MessageDialog.Info(this, msgModifySuccess);
                dialog.Dispose();
            }
            else
            {
                dialog.Close();
                dialog.Dispose();
            }
        }

        private void SetCurrentTemplate()
        { 
            if (tempGridView.CurrentRow == null)
                return;
            int rowIndex = tempGridView.CurrentRow.Index;
            if (rowIndex < 0 && rowIndex >= _tempDataSource.Count)
            {
                return;
            }

            this._currentTemplateIndex = rowIndex;
        }

        private StockTemplate GetSelectTemplate()
        {
            if (this._currentTemplateIndex < 0)
                return null;
            int rowIndex = this._currentTemplateIndex;
            if (rowIndex < 0 && rowIndex >= _tempDataSource.Count)
            {
                return null;
            }

            return _tempDataSource[rowIndex];
        }

        private StockTemplate SaveTemplateToDB(StockTemplate stockTemplate, TempChangeType type)
        {
            switch (type)
            {
                case TempChangeType.New:
                    {
                        var template = _templateBLL.CreateTemplate(stockTemplate);
                        stockTemplate.TemplateId = template.TemplateId;
                    }
                    break;
                case TempChangeType.Update:
                    {
                        int tempid = _templateBLL.UpdateTemplate(stockTemplate);
                        if (tempid > 0)
                        {
                            stockTemplate.TemplateId = tempid;
                        }
                        else
                        {
                            MessageDialog.Error(this, msgModifyFail);
                        }
                    }
                    break;
                default:
                    break;
            }
            return stockTemplate;
        }

        private bool Dialog_NewTemplate(object sender, object data)
        {
            if (data is StockTemplate)
            {
                StockTemplate stockTemplate = data as StockTemplate;
                stockTemplate = SaveTemplateToDB(stockTemplate, TempChangeType.New);

                //add into the view
                if (stockTemplate.TemplateId > 0)
                {
                    _tempDataSource.Add(stockTemplate);
                }
            }

            return true;
        }

        private bool Dialog_ModifyTemplate(object sender, object data)
        {
            if (!(data is StockTemplate))
            {
                return false;
            }

            StockTemplate stockTemplate = data as StockTemplate;
            stockTemplate = SaveTemplateToDB(stockTemplate, TempChangeType.Update);

            //update the row in the view
            if (stockTemplate.TemplateId > 0)
            {
                int targetIndex = _tempDataSource.ToList().FindIndex(p => p.TemplateId == stockTemplate.TemplateId);
                if (targetIndex >= 0 && targetIndex < _tempDataSource.Count)
                {
                    //archive the old template
                    ArchiveTemplate(_tempDataSource[targetIndex]);

                    _tempDataSource[targetIndex] = stockTemplate;
                }
            }

            return true;
        }

        #endregion

        #region TemplateStock button event handler

        private void ToolStripButton_Import_Click(object sender, EventArgs e)
        {
            ImportOptionDialog importDialog = new ImportOptionDialog();
            //ioDialog.SaveFormData += new FormDataSaveHandler(ImportOptionDialog_SaveFormData);
            importDialog.ShowDialog();
            if (importDialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                var importType = importDialog.ImportType;
                importDialog.Close();
                importDialog.Dispose();

                bool ret = ImportFromExcel(importType);
                if (ret)
                {
                    SwitchTemplateStockSave(true);
                }
                else
                { 
                    //Fail to import
                }
            }
            else
            {
                importDialog.Close();
                importDialog.Dispose();
            }
        }

        private void ToolStripButton_Save_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            if (template == null)
            {
                return;
            }

            if (_spotDataSource.Count <= 0)
            {
                return;
            }

            int ret = _templateBLL.Replace(template.TemplateId, _spotDataSource.ToList());
            if (ret > 0)
            {
                SwitchTemplateStockSave(false);

                //更新时间
                template.DModifiedDate = DateTime.Now;

                this.tempGridView.Invalidate();
            }
        }

        private void ToolStripButton_DeleteStock_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            if(template == null)
                return;

            var deleteItems = _spotDataSource.Where(p => p.Selection).ToList();

            //List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(this.secuGridView);
            if (deleteItems == null || deleteItems.Count == 0)
            {
                MessageDialog.Warn(this, msgDeleteSecuritySelect);
                return;
            }

            string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgDeleteSecurity);
            string msg = string.Format(format, template.TemplateId, template.TemplateName, deleteItems.Count);
            if(MessageDialog.Warn(this, msg, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //List<TemplateStock> deleteItems = new List<TemplateStock>();
            //for (int i = 0, count = selectIndex.Count; i < count; i++)
            //{
            //    int rowIndex = selectIndex[i];
            //    if (rowIndex >= 0 && rowIndex < _spotDataSource.Count)
            //    {
            //        deleteItems.Add(_spotDataSource[rowIndex]);
            //    }
            //}

            //archive template
            ArchiveTemplate(template);

            foreach (var deleteItem in deleteItems)
            {
                _spotDataSource.Remove(deleteItem);
            }

            //int success = _templateBLL.DeleteStock(deleteItems);
            //if (success > 0)
            //{


            //    MessageDialog.Info(this, msgSecurityDeleteSuccess);
            //}
            //else
            //{
            //    MessageDialog.Info(this, msgSecurityDeleteFail);
            //}

            SwitchTemplateStockSave(true);
        }

        private void ToolStripButton_AddStock_Click(object sender, EventArgs e)
        {
            if (tempGridView.CurrentRow == null)
            {
                MessageDialog.Warn(this, msgAddTempSelect);
                return;
            }

            int rowIndex = tempGridView.CurrentRow.Index;
            if (rowIndex < 0 || rowIndex > _tempDataSource.Count)
            {
                MessageDialog.Warn(this, msgInvalidSelect);
                return;
            }

            TemplateStock stock = new TemplateStock
            {
                TemplateNo = _tempDataSource[rowIndex].TemplateId
            };

            PortfolioSecurityDialog psDialog = new PortfolioSecurityDialog();
            psDialog.OnLoadControl(psDialog, DialogType.New);
            psDialog.OnLoadData(psDialog, stock);
            psDialog.SaveData += new FormSaveHandler(Dialog_SpotSecu_SaveData);
            psDialog.ShowDialog();
            if (psDialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                psDialog.Close();
                psDialog.Dispose();
            }
            else
            {
                psDialog.Close();
                psDialog.Dispose();
            }
        }

        private void ToolStripButton_ModifyStock_Click(object sender, EventArgs e)
        {
            var selectedStocks = _spotDataSource.Where(p => p.Selection).ToList();
            //List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(secuGridView);
            if (selectedStocks == null || selectedStocks.Count == 0)
            {
                MessageDialog.Warn(this, msgSecurityModifySelect);
                return;
            }

            if (selectedStocks.Count > 1)
            {
                MessageDialog.Warn(this, msgSecurityModifyOnlyOnce);
                return;
            }

            TemplateStock stock = selectedStocks[0];

            PortfolioSecurityDialog psDialog = new PortfolioSecurityDialog();
            psDialog.OnLoadControl(psDialog, DialogType.Modify);
            psDialog.OnLoadData(psDialog, stock);
            psDialog.SaveData += new FormSaveHandler(Dialog_SpotSecu_SaveData);
            psDialog.ShowDialog();
            if (psDialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                psDialog.Close();
                psDialog.Dispose();
            }
            else
            {
                psDialog.Close();
                psDialog.Dispose();
            }
        }

        private bool Dialog_SpotSecu_SaveData(object sender, object data)
        {
            bool ret = false;
            if (sender == null || data == null)
            {
                throw new Exception("Fail to get the setting from dialog");
            }

            PortfolioSecurityDialog dialog = sender as PortfolioSecurityDialog;
            if (dialog == null)
                return ret;

            if (data is TemplateStock)
            {
                TemplateStock stock = data as TemplateStock;
                switch (dialog.DialogType)
                {
                    case DialogType.New:
                        {
                            TemplateStock findStock = _spotDataSource.SingleOrDefault(p => p.SecuCode.Equals(stock.SecuCode));
                            if (findStock != null)
                            {
                                ret = false;
                                MessageDialog.Warn(this, msgCannotAddSameSecurity);
                            }
                            else
                            {
                                _spotDataSource.Add(stock);
                                ret = true;
                            }
                        }
                        break;
                    case DialogType.Modify:
                        {
                            int findIndex = _spotDataSource.ToList().FindIndex(p => p.SecuCode.Equals(stock.SecuCode));
                            if(findIndex >= 0 && findIndex < _spotDataSource.Count)
                            {                                    
                                ret = true;
                                _spotDataSource[findIndex] = stock;
                            }
                        }
                        break;
                    default:
                        break;
                }

                if (ret)
                {
                    var template = _tempDataSource.Single(p => p.TemplateId == stock.TemplateNo);
                    CalculateAmount(template);

                    SwitchTemplateStockSave(true);

                    this.secuGridView.Invalidate();
                }
            }

            return ret;
        }

        private void ToolStripButton_CalcAmount_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            if (template == null)
            {
                return;
            }

            if (_spotDataSource == null || _spotDataSource.Count == 0)
            {
                return;
            }

            //var invalidSecuItem = _spotDataSource.Where(p => FloatUtil.IsZero(p.SettingWeight) || p.Amount == 0).ToList();
            var invalidSecuItem = _spotDataSource.Where(p => FloatUtil.IsZero(p.SettingWeight)).ToList();
            if (invalidSecuItem != null && invalidSecuItem.Count() > 0)
            {
                if (MessageDialog.Warn(this, msgSecurityZeroWeight, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    foreach (var item in invalidSecuItem)
                    {
                        _spotDataSource.Remove(item);
                    }
                }
            }

            ReCalculateAmount(template);
            SummaryTemplateStock();
            SwitchTemplateStockSave(true);

            this.secuGridView.Invalidate();
        }

        private void ReCalculateAmount(StockTemplate template)
        {
            double[] weights = _spotDataSource.Select(p => p.SettingWeight / 100).ToArray();
            double totalWeight = weights.Sum();
            double minusResult = 1.0 - totalWeight;

            double[] mweights = _spotDataSource.Select(p => p.MarketCapWeight / 100).ToArray();
            double mtotal = mweights.Sum();
            double mDiff = 1.0 - mtotal;

            //如果不为100%，则需要调整比例; 依据市值调整
            if (Math.Abs(minusResult) > 0.001 && mDiff > 0.001)
            {
                for (int i = 0, count = _spotDataSource.Count; i < count; i++)
                {
                    var stock = _spotDataSource[i];
                    weights[i] = stock.MarketCapWeight / mtotal;
                 }
            }

            List<SecurityItem> secuList = GetSecurityItems(template);

            var benchmarkItem = _securityInfoList.Find(p => p.SecuCode.Equals(template.Benchmark) && p.SecuType == SecurityType.Index);
            var benchmarkData = QuoteCenter.Instance.GetMarketData(benchmarkItem);
            var benchmark = _benchmarkList.Find(p => p.BenchmarkId.Equals(benchmarkItem.SecuCode));
            double bmkPrice = 0f;
            double totalValue = 0f;
            if(!FloatUtil.IsZero(benchmarkData.CurrentPrice))
            {
                bmkPrice = benchmarkData.CurrentPrice;
            }
            else if (!FloatUtil.IsZero(benchmarkData.PreClose))
            {
                bmkPrice = benchmarkData.PreClose;
            }

            //指数基准当期总市值
            //上证50、沪深300、中证500每一个点数对应不同的价格
            totalValue = bmkPrice * benchmark.ContractMultiple;
            totalValue = totalValue * template.MarketCapOpt / 100;

            var prices = GetPrices(secuList);
            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalValue, weights, prices, 0);
            var mktCaps = GetMarketCap(prices, amounts);
            switch (template.EWeightType)
            {
                case Model.EnumType.WeightType.ProportionalWeight:
                    {
                        double totalCap = mktCaps.Sum();
                        for (int i = 0, count = _spotDataSource.Count; i < count; i++)
                        {
                            var stock = _spotDataSource[i];
                            stock.Amount = amounts[i];
                            stock.MarketCap = mktCaps[i];
                            stock.MarketCapWeight = 100 * stock.MarketCap / totalCap;
                        }
                    }
                    break;
                case Model.EnumType.WeightType.AmountWeight:
                    {
                        var totalAmount = amounts.Sum();
                        double totalCap = mktCaps.Sum();
                        for (int i = 0, count = _spotDataSource.Count; i < count; i++)
                        {
                            var stock = _spotDataSource[i];
                            stock.Amount = amounts[i];
                            stock.MarketCap = mktCaps[i];
                            stock.MarketCapWeight = 100 * stock.MarketCap / totalCap;
                            stock.SettingWeight = 100 * (double)stock.Amount / (double)totalAmount;
                        }
                    }
                    break;
                default:
                    break;
            }
            
        }

        private void CalculateAmount(StockTemplate template)
        {
            List<SecurityItem> secuList = GetSecurityItems(template);
            //QuoteCenter.Instance.Query(secuList);

            double[] prices = GetPrices(secuList);
            int[] amounts = _spotDataSource.Select(p => p.Amount).ToArray();
            double[] mktCaps = GetMarketCap(prices, amounts);

            double totalCap = mktCaps.Sum();
            for (int i = 0, count = _spotDataSource.Count; i < count; i++)
            {
                var stock = _spotDataSource[i];
                stock.Amount = amounts[i];
                stock.MarketCap = mktCaps[i];
                stock.MarketCapWeight = 100 * stock.MarketCap / totalCap;
                stock.SettingWeight = stock.MarketCapWeight;
            }

        }

        private List<SecurityItem> GetSecurityItems(StockTemplate template)
        {
            List<SecurityItem> secuList = new List<SecurityItem>();
            var benchmarkItem = SecurityInfoManager.Instance.Get(template.Benchmark, SecurityType.Index);
            if (benchmarkItem != null)
            {
                secuList.Add(benchmarkItem);
            }

            foreach (var stock in _spotDataSource)
            {
                var secuItem = SecurityInfoManager.Instance.Get(stock.SecuCode, SecurityType.Stock);
                secuList.Add(secuItem);
            }

            return secuList;
        }

        private double[] GetPrices(List<SecurityItem> secuList)
        {
            double[] prices = new double[_spotDataSource.Count];
            for (int i = 0, count = _spotDataSource.Count; i < count; i++)
            {
                var stock = _spotDataSource[i];
                var secuItem = secuList.Find(p => p.SecuCode.Equals(stock.SecuCode) && p.SecuType == SecurityType.Stock);
                if (secuItem != null)
                {
                    var secuData = QuoteCenter.Instance.GetMarketData(secuItem);
                    stock.ELimitUpDownFlag = secuData.LimitUpDownFlag;
                    stock.ESuspendFlag = secuData.SuspendFlag;

                    if (!FloatUtil.IsZero(secuData.CurrentPrice))
                    {
                        prices[i] = secuData.CurrentPrice;
                    }
                    else if (!FloatUtil.IsZero(secuData.PreClose))
                    {
                        prices[i] = secuData.PreClose;
                    }
                    else
                    {
                        prices[i] = secuData.LowLimitPrice;
                    }
                }
                else
                {
                    prices[i] = 0f;
                }
            }

            return prices;
        }

        /// <summary>
        /// The formulas to calculate the market cap
        /// MarketCap(i) = Price[i] * Amounts[i]
        /// </summary>
        /// <param name="prices">An array with each element as a security current price.</param>
        /// <param name="amounts">An array with each element as a security amount.</param>
        /// <returns>An array with each element as a security market capital.</returns>
        private double[] GetMarketCap(double[] prices, int[] amounts)
        {
            double[] mktCaps = new double[prices.Length];
            for (int i = 0, count = mktCaps.Length; i < count; i++)
            {
                mktCaps[i] = prices[i] * amounts[i];
            }

            return mktCaps;
        }

        #endregion

        #region archive

        private void ArchiveTemplate(StockTemplate template)
        {
            //archive before deleting
            _histTemplateBLL.ArchiveTemplate(template);
        }

        #endregion

        #region Import method

        private bool ImportFromExcel(ImportType importType)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx";
            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return false;
            }
            else
            {
                string extension = Path.GetExtension(fileDialog.FileName);
                List<string> list = new List<string>() { ".xls", ".xlsx" };

                if (!list.Contains(extension))
                {
                    MessageDialog.Warn(this, msgImportInvalidFileType);
                    return false;
                }

                var sheetConfig = ConfigManager.Instance.GetImportConfig().GetSheet("stocktemplate");
                var cellRanges = ConfigManager.Instance.GetImportConfig().GetColumnHeader(sheetConfig.Columns);
                Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string, DataColumnHeader>();
                foreach (var column in cellRanges)
                {
                    colHeadMap.Add(column.Name, column);
                }

                int sheetIndex = 0;
                var table = ExcelUtil.GetSheetData(fileDialog.FileName, sheetIndex, colHeadMap);
                if (table == null || table.ColumnIndex == null || table.Rows == null)
                {
                    MessageDialog.Error(this, msgImportLoadFail);
                    return false;
                }

                List<TemplateStock> stockList = null;

                try
                {
                    stockList = ExcelToGrid(table);
                }
                catch (Exception e)
                {
                    string msg = string.Format("Fail to convert excel data to table, message: {0}", e.Message);
                    logger.Error(msg);  
                }
                if (stockList == null || stockList.Count == 0)
                {
                    MessageDialog.Error(this, msgImportLoadFail);
                    return false;
                }

                switch (importType)
                {
                    case ImportType.Replace:
                        {
                            _spotDataSource.Clear();
                            foreach (var stock in stockList)
                            {
                                _spotDataSource.Add(stock);
                            }
                        }
                        break;
                    case ImportType.Append:
                        {
                            //List<TemplateStock> appendList = new List<TemplateStock>();
                            List<TemplateStock> duplicatedList = new List<TemplateStock>();
                            foreach (var sp in _spotDataSource)
                            {
                                int index = stockList.FindIndex(p => p.SecuCode.Equals(sp.SecuCode));
                                if (index >= 0)
                                {
                                    duplicatedList.Add(stockList[index]);
                                    stockList.RemoveAt(index);
                                }
                            }

                            foreach (var nitem in stockList)
                            {
                                _spotDataSource.Add(nitem);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        private List<TemplateStock> ExcelToGrid(Model.Data.DataTable excelData)
        {
            List<TemplateStock> stockList = new List<TemplateStock>();
            var template = GetSelectTemplate();
            if(template == null)
                return stockList;

            //用于获取二维表表头名字与Excel中表头进行匹配，获取Excel DataTable中列的位置
            TSGrid hsGrid = _gridConfig.GetGid(GridStock);
            var gridColumns = hsGrid.Columns;

            //标签与属性名映射表
            var attFieldMap = GridViewBindingHelper.GetPropertyBinding(typeof(TemplateStock));
            
            //实体类标签名与Excel DataTable DataRow.Columns中列索引映射表
            Dictionary<string, int> fieldNameColumnIndexMap = new Dictionary<string, int>();
            for (int i = 0, count = gridColumns.Count; i < count; i++)
            {
                var column = gridColumns[i];
                string origtext = column.Text;
                string text = origtext;
                if (text.Contains("(%)"))
                {
                    text = text.Replace("(%)", string.Empty);
                }

                if (excelData.ColumnIndex.ContainsKey(origtext))
                {
                    if (!fieldNameColumnIndexMap.ContainsKey(column.Name))
                    {
                        fieldNameColumnIndexMap.Add(column.Name, excelData.ColumnIndex[origtext]);
                    }
                }
                else if (excelData.ColumnIndex.ContainsKey(text))
                {
                    if (!fieldNameColumnIndexMap.ContainsKey(column.Name))
                    {
                        fieldNameColumnIndexMap.Add(column.Name, excelData.ColumnIndex[text]);
                    }
                }
            }

            foreach (var row in excelData.Rows)
            {
                TemplateStock stock = new TemplateStock();
                stock.TemplateNo = template.TemplateId;

                foreach (var kv in fieldNameColumnIndexMap)
                {
                    if (!attFieldMap.ContainsKey(kv.Key) || !fieldNameColumnIndexMap.ContainsKey(kv.Key))
                        continue;

                    string fieldName = attFieldMap[kv.Key];
                    int valIndex = fieldNameColumnIndexMap[kv.Key];

                    var field = stock.GetType().GetProperty(fieldName);
                    if (field == null || !field.CanWrite)
                        continue;

                    DataValueType valType = DataValueType.String;
                    var gridColomn = gridColumns.Find(p => p.Name.Equals(kv.Key));
                    if (gridColomn != null)
                    {
                        valType = gridColomn.ValueType;
                    }

                    var val = row.Columns[valIndex];
                    if (val != null)
                    {
                        switch (val.Type)
                        {
                            case DataValueType.Int:
                                field.SetValue(stock, val.GetInt());
                                break;
                            case DataValueType.Float:
                                field.SetValue(stock, val.GetDouble());
                                break;
                            case DataValueType.String:
                                if (valType == DataValueType.Int)
                                {
                                    field.SetValue(stock, val.GetInt());
                                }
                                else if (valType == DataValueType.Float)
                                {
                                    field.SetValue(stock, val.GetDouble());
                                }
                                else
                                {
                                    field.SetValue(stock, val.GetStr());
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    { 
                        
                    }
                }

                if (!string.IsNullOrEmpty(stock.SecuCode))
                {
                    var secuInfo = _securityInfoList.Find(p => p.SecuCode.Equals(stock.SecuCode) && p.SecuType == SecurityType.Stock);
                    if (secuInfo != null)
                    {
                        stock.SecuName = secuInfo.SecuName;
                        stock.Exchange = secuInfo.ExchangeCode;
                    }
                }

                stockList.Add(stock);
            }

            return stockList;
        }

        #endregion

        private void ToolStripButton_Export_Click(object sender, EventArgs e)
        {
            var template = GetSelectTemplate();
            if (template == null)
            {
                return;
            }

            SaveFileDialog fileDialog = new SaveFileDialog();
            //设置文件类型
            fileDialog.Filter = "Excel文档(*.xls)|*.xls";
            //设置默认文件类型显示顺序
            fileDialog.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录
            fileDialog.RestoreDirectory = true;
            //设置默认文件名
            fileDialog.FileName = template.TemplateName;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = Path.GetDirectoryName(fileDialog.FileName);
                string extension = Path.GetExtension(fileDialog.FileName);

                var tempStocks = _spotDataSource.Where(p => p.TemplateNo == template.TemplateId).ToList();
                List<DataHeader> cellRanges = null;
                var table = GridToExecl(tempStocks, out cellRanges);
                ExcelUtil.CreateExcel(fileDialog.FileName, table, cellRanges);
            }
        }

        private Model.Data.DataTable GridToExecl(List<TemplateStock> tempStocks, out List<DataHeader> cellRanges)
        {
            TSGrid hsGrid = _gridConfig.GetGid(GridStock);
            var gridColumns = hsGrid.Columns;

            var sheetConfig = ConfigManager.Instance.GetImportConfig().GetSheet("stocktemplate");
            cellRanges = sheetConfig.Columns;
            var sheetColumns = ConfigManager.Instance.GetImportConfig().GetColumnHeader(cellRanges);
            Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string, DataColumnHeader>();
            foreach (var column in sheetColumns)
            {
                //colHeadMap.Add(column.Name, column);
                var gridColumn = gridColumns.Find(p => p.Text.Equals(column.Name));
                if (gridColumn != null)
                {
                    if (!colHeadMap.ContainsKey(gridColumn.Name))
                    {
                        colHeadMap.Add(gridColumn.Name, column);
                    }
                }
            }

            //实体类标签名与Excel DataTable DataRow.Columns中列索引映射表
            Dictionary<string, int> excelNameColumnIndexMap = new Dictionary<string, int>();
            for (int i = 0, count = sheetColumns.Count; i < count; i++)
            {
                if (!excelNameColumnIndexMap.ContainsKey(sheetColumns[i].Name))
                {
                    excelNameColumnIndexMap.Add(sheetColumns[i].Name, i);
                }
            }

            Dictionary<string, int> fieldNameColumnIndexMap = new Dictionary<string, int>();
            for (int i = 0, count = gridColumns.Count; i < count; i++)
            {
                var column = gridColumns[i];
                int index = sheetColumns.FindIndex(p => p.Name.Equals(column.Text));
                if (index >= 0)
                {
                    if (!fieldNameColumnIndexMap.ContainsKey(column.Name))
                    {
                        fieldNameColumnIndexMap.Add(column.Name, index);
                    }
                }
            }

            Model.Data.DataTable table = new Model.Data.DataTable 
            {
                ColumnIndex = new Dictionary<string,int>(),
                Rows = new List<Model.Data.DataRow>()
            };

            table.ColumnIndex = excelNameColumnIndexMap;

            var attFieldMap = GridViewBindingHelper.GetPropertyBinding(typeof(TemplateStock));
            foreach (var tempStock in tempStocks)
            {
                Model.Data.DataRow dataRow = new Model.Data.DataRow 
                {
                    Columns = new List<DataValue>(new DataValue[table.ColumnIndex.Count])
                };

                foreach (var kv in attFieldMap)
                {
                    if (!attFieldMap.ContainsKey(kv.Key) || !fieldNameColumnIndexMap.ContainsKey(kv.Key))
                        continue;

                    string fieldName = attFieldMap[kv.Key];
                    int valIndex = fieldNameColumnIndexMap[kv.Key];

                    var field = tempStock.GetType().GetProperty(fieldName);
                    if (field == null)
                        continue;

                    Model.Data.DataValue dataValue = new DataValue();
                    if(colHeadMap.ContainsKey(kv.Key))
                    {
                        dataValue.Type = colHeadMap[kv.Key].Type;
                    }

                    dataValue.Value = field.GetValue(tempStock, null);

                    dataRow.Columns[valIndex] = dataValue;
                    if (valIndex >= 0 && valIndex < dataRow.Columns.Count)
                    {
                        dataRow.Columns[valIndex] = dataValue;
                    }
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }
    }
}
