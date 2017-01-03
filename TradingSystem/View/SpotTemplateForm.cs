using Calculation;
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

namespace TradingSystem.View
{
    public partial class SpotTemplateForm : Forms.DefaultForm
    {
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
        private const string msgDeleteSecuritySelect = "tempdeletesecurityselect";
        private const string msgAddTempSelect = "tempaddselect";
        private const string msgInvalidSelect = "tempinvalidselect";
        private const string msgSecurityModifySelect = "tempsecuritymodifyselect";
        private const string msgSecurityModifyOnlyOnce = "tempsecuritymodifyonlyonce";
        private const string msgCannotAddSameSecurity = "tempcannotaddsamesecurity";
        private const string msgSecurityZeroWeight = "tempsecurityzeroweight";
        private const string msgImportInvalidFileType = "tempimportinvalidfiletype";
        private const string msgImportLoadFail = "tempimportloadfail";

        private GridConfig _gridConfig = null;
        private const string GridTemplate = "stocktemplate";
        private const string GridStock = "templatestock";

        private TemplateBLL _templateBLL = new TemplateBLL();
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

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

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
            _tempDataSource.Clear();
            _spotDataSource.Clear();

            var items = _templateBLL.GetTemplates();
            if (items != null)
            { 
                foreach(var item in items)
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

            return true;
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
            }
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

        #region

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


        #endregion

        #region button click

        private void ToolStripButton_AddTemplate_Click(object sender, System.EventArgs e)
        {
            TemplateDialog dialog = new TemplateDialog();
            dialog.SaveData += new FormLoadHandler(Dialog_NewTemplate);
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

            TemplateDialog dialog = new TemplateDialog();
            dialog.SaveData += new FormLoadHandler(Dialog_ModifyTemplate);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, stockTemplate);
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
                CreatedUserId = template.CreatedUserId,
                EStatus = template.EStatus,
                DCreatedDate = DateTime.Now,
                CanEditUsers = template.CanEditUsers,
                CanViewUsers = template.CanViewUsers,
                Permissions = template.Permissions,
            };

            StockTemplate newtemp = _templateBLL.CreateTemplate(temp);
            if (newtemp.TemplateId > 0)
            {
                _tempDataSource.Add(newtemp);
            }

            this.tempGridView.Invalidate();
        }

        private void ToolStripButton_DeleteTemplate_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            if (template == null)
                return;

            int ret = _templateBLL.DeleteTemplate(template);
            if (ret == 1)
            {
                MessageDialog.Info(this, msgDeleteTempSuccess);
            }
            else
            {
                MessageDialog.Warn(this, msgDeleteTemp);
            }
        }

        #endregion

        #region Template change method

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
            }
        }

        private void ToolStripButton_DeleteStock_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            if(template == null)
                return;

            List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(this.secuGridView);
            if (selectIndex.Count == 0)
            {
                MessageDialog.Warn(this, msgDeleteSecuritySelect);
                return;
            }

            string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgDeleteSecurity);
            string msg = string.Format(format, template.TemplateId, template.TemplateName, selectIndex.Count);
            if(MessageDialog.Warn(this, msg, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            for (int i = selectIndex.Count - 1; i >= 0; i--)
            { 
                int rowIndex = selectIndex[i];
                if(rowIndex >= 0 && rowIndex < _spotDataSource.Count)
                {
                    _spotDataSource.RemoveAt(rowIndex);
                }
            }

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
            psDialog.SaveData += new FormLoadHandler(Dialog_SpotSecu_SaveData);
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
            List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(secuGridView);
            if (selectIndex.Count == 0)
            {
                MessageDialog.Warn(this, msgSecurityModifySelect);
                return;
            }

            if (selectIndex.Count > 1)
            {
                MessageDialog.Warn(this, msgSecurityModifyOnlyOnce);
                return;
            }

            int rowIndex = selectIndex[0];
            if (rowIndex < 0 || rowIndex > _spotDataSource.Count)
            {
                MessageDialog.Warn(this, msgInvalidSelect);
                return;
            }

            TemplateStock stock = _spotDataSource[rowIndex];

            PortfolioSecurityDialog psDialog = new PortfolioSecurityDialog();
            psDialog.OnLoadControl(psDialog, DialogType.Modify);
            psDialog.OnLoadData(psDialog, stock);
            psDialog.SaveData += new FormLoadHandler(Dialog_SpotSecu_SaveData);
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

            var invalidSecuItem = _spotDataSource.Where(p => p.SettingWeight <= 0.0001 || p.Amount == 0).ToList();
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
            SwitchTemplateStockSave(true);

            this.secuGridView.Invalidate();
        }

        private void ReCalculateAmount(StockTemplate template)
        {
            double[] weights = _spotDataSource.Select(p => p.SettingWeight / 100).ToArray();
            double totalWeight = weights.Sum();
            double minusResult = 1.0 - totalWeight;

            //如果不为100%，则根据数量调整比例;这种调整方式是否可以改进, 通过市值调整？
            if ( minusResult > 0.001 )
            {
                int[] origAmounts = new int[_spotDataSource.Count];
                for (int i = 0, count = _spotDataSource.Count; i < count; i++)
                {
                    var stock = _spotDataSource[i];
                    origAmounts[i] = stock.Amount;
                }

                weights = CalcUtil.CalcStockWeightByAmount(origAmounts);
                for (int i = 0, count = weights.Length; i < count; i++)
                {
                    double weight = weights[i];
                    var stock = _spotDataSource[i];
                    stock.SettingWeight = 100 * weight;
                }
            }

            List<SecurityItem> secuList = GetSecurityItems(template);

            var benchmarkItem = _securityInfoList.Find(p => p.SecuCode.Equals(template.Benchmark) && p.SecuType == SecurityType.Index);
            var benchmarkData = QuoteCenter2.Instance.GetMarketData(benchmarkItem);
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

            var prices = GetPrices(secuList);
            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalValue, weights, prices);
            var mktCaps = GetMarketCap(prices, amounts);
            
            double totalCap = mktCaps.Sum();
            for (int i = 0, count = _spotDataSource.Count; i < count; i++)
            {
                var stock = _spotDataSource[i];
                stock.Amount = amounts[i];
                stock.MarketCap = mktCaps[i];
                stock.MarketCapWeight = 100 * stock.MarketCap / totalCap;
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
            var benchmarkItem = _securityInfoList.Find(p => p.SecuCode.Equals(template.Benchmark) && p.SecuType == SecurityType.Index);
            if (benchmarkItem != null)
            {
                secuList.Add(benchmarkItem);
            }

            foreach (var stock in _spotDataSource)
            {
                var findItem = _securityInfoList.Find(p => p.SecuCode.Equals(stock.SecuCode) && p.SecuType == SecurityType.Stock);
                if (findItem != null)
                {
                    secuList.Add(findItem);
                }
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
                    var secuData = QuoteCenter2.Instance.GetMarketData(secuItem);

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

                var stockList = ExcelToGrid(table);
                if (stockList == null || stockList.Count == 0)
                {
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
            HSGrid hsGrid = _gridConfig.GetGid(GridStock);
            var gridColumns = hsGrid.Columns;

            //标签与属性名映射表
            var attFieldMap = GridViewBindingHelper.GetPropertyBinding(typeof(TemplateStock));
            
            //实体类标签名与Excel DataTable DataRow.Columns中列索引映射表
            Dictionary<string, int> fieldNameColumnIndexMap = new Dictionary<string, int>();
            for (int i = 0, count = gridColumns.Count; i < count; i++)
            {
                var column = gridColumns[i];
                if (excelData.ColumnIndex.ContainsKey(column.Text))
                {
                    if (!fieldNameColumnIndexMap.ContainsKey(column.Name))
                    {
                        fieldNameColumnIndexMap.Add(column.Name, excelData.ColumnIndex[column.Text]);
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
                    if (field == null)
                        continue;

                    DataValueType valType = DataValueType.String;
                    var gridColomn = gridColumns.Find(p => p.Name.Equals(kv.Key));
                    if (gridColomn != null)
                    {
                        valType = gridColomn.ValueType;
                    }

                    var val = row.Columns[valIndex];
                    switch(val.Type)
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
            HSGrid hsGrid = _gridConfig.GetGid(GridStock);
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
                    Columns = new List<DataValue>(table.ColumnIndex.Count)
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

                    //dataRow.Columns[valIndex] = dataValue;
                    dataRow.Columns.Insert(valIndex, dataValue);
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }
    }
}
