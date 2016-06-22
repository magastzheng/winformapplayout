using Calculation;
using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
using Model.config;
using Model.Data;
using Model.SecurityInfo;
using Model.UI;
using Quote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Util;
using System.Linq;

namespace TradingSystem.View
{
    public partial class SpotTemplateForm : Forms.DefaultForm
    {
        public enum TempChangeType
        {
            New = 0,
            Update = 1,
        }

        private const string MsgDeleteStock = "确定要从模板[{0}-{1}]中删除选择的[{2}]支证券吗?";

        private GridConfig _gridConfig = null;
        private const string GridTemplate = "stocktemplate";
        private const string GridStock = "templatestock";

        private StockTemplateDAO _tempdbdao = new StockTemplateDAO();
        private TemplateStockDAO _stockdbdao = new TemplateStockDAO();
        private SecurityInfoDAO _secudbdao = new SecurityInfoDAO();
        
        private SortableBindingList<StockTemplate> _tempDataSource = new SortableBindingList<StockTemplate>(new List<StockTemplate>());
        private SortableBindingList<TemplateStock> _spotDataSource = new SortableBindingList<TemplateStock>(new List<TemplateStock>());
        private List<SecurityItem> _securityInfoList = new List<SecurityItem>();
        private List<Benchmark> _benchmarkList = new List<Benchmark>();

        public SpotTemplateForm()
            :base()
        {
            InitializeComponent();
        }

        public SpotTemplateForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.tsbAdd.Click += new System.EventHandler(this.Button_Add_Click);
            this.tsbModify.Click += new System.EventHandler(this.Button_Modify_Click);
            this.tsbImport.Click += new System.EventHandler(this.ToolStripButton_Import_Click);
            this.tsbAddStock.Click += new System.EventHandler(ToolStripButton_AddStock_Click);
            this.tsbModifyStock.Click += new System.EventHandler(ToolStripButton_ModifyStock_Click);
            this.tsbDeleteStock.Click += new System.EventHandler(ToolStripButton_DeleteStock_Click);
            this.tsbSave.Click += new System.EventHandler(ToolStripButton_Save_Click);
            this.tsbCalcAmount.Click += new EventHandler(ToolStripButton_CalcAmount_Click);
            this.tsbExport.Click += new EventHandler(ToolStripButton_Export_Click);

            tempGridView.ClickRow += new ClickRowHandler(GridView_Template_ClickRow);

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        #region load control
        private bool Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.tempGridView, _gridConfig.GetGid(GridTemplate));
            Dictionary<string, string> tempColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(StockTemplate));
            TSDataGridViewHelper.SetDataBinding(this.tempGridView, tempColDataMap);

            //set the securityGridView
            TSDataGridViewHelper.AddColumns(this.secuGridView, _gridConfig.GetGid(GridStock));
            Dictionary<string, string> securityColDataMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(TemplateStock));
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

            var items = _tempdbdao.GetByUser(-1);
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

            _securityInfoList = _secudbdao.Get(SecurityType.All);
            _benchmarkList = _tempdbdao.GetBenchmark();

            return true;
        }

        private void LoadTemplateStock(int templateNo)
        {
            if (templateNo < 0)
                return;

            _spotDataSource.Clear();
            var stocks = _stockdbdao.Get(templateNo);
            if (stocks != null)
            {
                foreach (var stock in stocks)
                {
                    _spotDataSource.Add(stock);
                }
            }
        }

        #endregion

        #region

        private void GridView_Template_ClickRow(object sender, int rowIndex)
        {
            //
            if (rowIndex < 0 || rowIndex >= _tempDataSource.Count)
                return;
            var template = _tempDataSource[rowIndex];

            if (template != null && template.TemplateId > 0)
            {
                LoadTemplateStock(template.TemplateId);
            }
        }


        #endregion

        #region button click

        private void Button_Add_Click(object sender, System.EventArgs e)
        {
            TemplateDialog dialog = new TemplateDialog();
            dialog.SaveData += new FormLoadHandler(Dialog_NewTemplate);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, null);
            //dialog.OnLoadFormActived(json);
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

        private void Button_Modify_Click(object sender, System.EventArgs e)
        {
            StockTemplate stockTemplate = GetSelectTemplate();
            if (stockTemplate == null)
                return;

            //string json = JsonUtil.SerializeObject(stockTemplate);

            TemplateDialog dialog = new TemplateDialog();
            dialog.SaveData += new FormLoadHandler(Dialog_ModifyTemplate);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            //dialog.OnFormActived(json);
            dialog.OnLoadControl(dialog, null);
            dialog.OnLoadData(dialog, stockTemplate);
            dialog.ShowDialog();
            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                dialog.Dispose();
            }
            else
            {
                dialog.Close();
                dialog.Dispose();
            }
        }

        #endregion

        #region Template change method

        private StockTemplate GetSelectTemplate()
        {
            if (tempGridView.CurrentRow == null)
                return null;
            int rowIndex = tempGridView.CurrentRow.Index;
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
                        int newid = _tempdbdao.Create(stockTemplate.TemplateName, stockTemplate.WeightType, stockTemplate.ReplaceType, stockTemplate.FutureCopies, stockTemplate.MarketCapOpt, stockTemplate.Benchmark, 11111);
                        stockTemplate.TemplateId = newid;
                    }
                    break;
                case TempChangeType.Update:
                    {
                        int tempid = _tempdbdao.Update(stockTemplate.TemplateId, stockTemplate.TemplateName, stockTemplate.WeightType, stockTemplate.ReplaceType, stockTemplate.FutureCopies, stockTemplate.MarketCapOpt, stockTemplate.Benchmark, 11111);
                        stockTemplate.TemplateId = tempid;
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
                for (int i = 0, count = _tempDataSource.Count; i < count; i++)
                {
                    if (_tempDataSource[i].TemplateId == stockTemplate.TemplateId)
                    {
                        _tempDataSource[i] = stockTemplate;
                        break;
                    }
                }
            }

            return true;
        }

        #endregion

        #region TemplateStock button event handler

        private void ToolStripButton_Import_Click(object sender, EventArgs e)
        {
            ImportOptionDialog ioDialog = new ImportOptionDialog();
            //ioDialog.SaveFormData += new FormDataSaveHandler(ImportOptionDialog_SaveFormData);
            ioDialog.ShowDialog();
            if (ioDialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                var importType = ioDialog.ImportType;
                ioDialog.Close();
                ioDialog.Dispose();

                ImportFromExcel(importType);
            }
            else
            {
                ioDialog.Close();
                ioDialog.Dispose();
            }
        }

        private void ToolStripButton_Save_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            if (template == null)
                return;

            List<TemplateStock> originStocks = _stockdbdao.Get(template.TemplateId);
            if (originStocks == null)
                return;

            foreach (var stock in _spotDataSource)
            {
                var findItem = originStocks.Find(p => p.SecuCode.Equals(stock.SecuCode));

                //Update if there is existed
                if (findItem != null && !string.IsNullOrEmpty(findItem.SecuCode))
                {
                    string newid = _stockdbdao.Update(stock);
                    originStocks.Remove(findItem);
                }
                else
                {
                    string newid = _stockdbdao.Create(stock);
                    if (string.IsNullOrEmpty(newid))
                    {
                        //TODO: popup the error message
                    }
                }
            }

            if (originStocks.Count > 0)
            {
                originStocks.ForEach(p => {
                    string retid = _stockdbdao.Delete(p.TemplateNo, p.SecuCode);
                    if (string.IsNullOrEmpty(retid))
                    { 
                        //TODO: fail to remove the template stock
                    }
                });
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
                MessageBox.Show("请选择要删除的证券", "警告", MessageBoxButtons.OK);
                return;
            }

            string msg = string.Format(MsgDeleteStock, template.TemplateId, template.TemplateName, selectIndex.Count);
            if (MessageBox.Show(msg, "警告", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            for (int i = selectIndex.Count - 1; i >= 0; i--)
            { 
                int rowIndex = selectIndex[i];
                if(rowIndex >= 0 && rowIndex < _spotDataSource.Count)
                {
                    TemplateStock stock = _spotDataSource[rowIndex];
                    string newid = _stockdbdao.Delete(template.TemplateId, stock.SecuCode);
                    if (string.IsNullOrEmpty(newid))
                    {
                        //TODO: popup the error message
                    }
                    else
                    {
                        _spotDataSource.Remove(stock);
                    }
                }
            }
        }

        private void ToolStripButton_AddStock_Click(object sender, EventArgs e)
        {
            if (tempGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择需要添加的现货模板！", "警告", MessageBoxButtons.OK);
                return;
            }

            int rowIndex = tempGridView.CurrentRow.Index;
            if (rowIndex < 0 || rowIndex > _tempDataSource.Count)
            {
                MessageBox.Show("无效选择！", "警告", MessageBoxButtons.OK);
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
                MessageBox.Show("请选择需要修改的证券！", "警告", MessageBoxButtons.OK);
                return;
            }

            if (selectIndex.Count > 1)
            {
                MessageBox.Show("每次仅能对一支证券修改！", "警告", MessageBoxButtons.OK);
                return;
            }

            int rowIndex = selectIndex[0];
            if (rowIndex < 0 || rowIndex > _spotDataSource.Count)
            {
                MessageBox.Show("无效选择！", "警告", MessageBoxButtons.OK);
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
                                MessageBox.Show(this, "不能添加相同的证券", "警告", MessageBoxButtons.OK);
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
                            for (int i = 0, count = _spotDataSource.Count; i < count; i++)
                            {
                                if (stock.SecuCode.Equals(_spotDataSource[i].SecuCode))
                                {
                                    ret = true;
                                    _spotDataSource[i] = stock;
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                var template = _tempDataSource.Single(p => p.TemplateId == stock.TemplateNo);
                CalculateAmount(template);

                this.secuGridView.Invalidate();
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
                if (MessageBox.Show(this, "存在权重设置为零的证券，如果继续将删除这些证券!", "警告", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
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

            CalculateAmount(template);

            this.secuGridView.Invalidate();
        }

        private void CalculateAmount(StockTemplate template)
        {
            //_spotDataSource
            List<SecurityItem> secuList = new List<SecurityItem>();
            var benchmarkItem = _securityInfoList.Find(p => p.SecuCode.Equals(template.Benchmark) && p.SecuType == SecurityType.Index);
            if (benchmarkItem != null)
            {
                secuList.Add(benchmarkItem);
            }

            double[] weights = new double[_spotDataSource.Count];
            if (_spotDataSource != null)
            {
                for (int i = 0, count = _spotDataSource.Count; i < count; i++)
                {
                    var stock = _spotDataSource[i];
                    weights[i] = stock.SettingWeight / 100;

                    var secuItem = _securityInfoList.Find(p => p.SecuCode.Equals(stock.SecuCode) && p.SecuType == SecurityType.Stock);
                    secuList.Add(secuItem);
                }
            }

            QuoteCenter.Instance.Query(secuList);

            var benchmarkData = QuoteCenter.Instance.GetMarketData(benchmarkItem);
            var benchmark = _benchmarkList.Find(p => p.BenchmarkId.Equals(benchmarkItem.SecuCode));
            double totalValue = benchmarkData.CurrentPrice * benchmark.ContractMultiple;
            double[] prices = new double[_spotDataSource.Count];
            for (int i = 0, count = _spotDataSource.Count; i < count; i++)
            {
                var stock = _spotDataSource[i];
                var secuItem = secuList.Find(p => p.SecuCode.Equals(stock.SecuCode) && p.SecuType == SecurityType.Stock);
                var secuData = QuoteCenter.Instance.GetMarketData(secuItem);
                prices[i] = secuData.CurrentPrice;
            }

            var amounts = CalcUtil.CalcStockAmountPerCopyRound(totalValue, weights, prices);
            double[] mktCaps = new double[_spotDataSource.Count];
            for (int i = 0, count = mktCaps.Length; i < count; i++)
            {
                mktCaps[i] = prices[i] * amounts[i];
            }

            double totalCap = mktCaps.Sum();
            for (int i = 0, count = _spotDataSource.Count; i < count; i++)
            {
                var stock = _spotDataSource[i];
                stock.Amount = amounts[i];
                stock.MarketCap = mktCaps[i];
                stock.MarketCapWeight = stock.MarketCap / totalCap;
            }
        }

        #endregion

        #region Import method

        private bool ImportFromExcel(ImportType importType)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.xls)|*.xls";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string extension = Path.GetExtension(fileDialog.FileName);
                List<string> list = new List<string>() { ".xls", ".xlsx" };

                if (!list.Contains(extension))
                {
                    MessageBox.Show(this, "文件类型不符合", "请选择excel文件", MessageBoxButtons.OK);

                    return false;
                }

                var sheetConfig = ConfigManager.Instance.GetImportConfig().GetSheet("stocktemplate");
                Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string, DataColumnHeader>();
                foreach (var column in sheetConfig.Columns)
                {
                    colHeadMap.Add(column.Name, column);
                }
                //string sheetName = @"20160222";
                int sheetIndex = 0;
                var table = ExcelUtil.GetSheetData(fileDialog.FileName, sheetIndex, colHeadMap);
                //Console.WriteLine(table.ColumnIndex.Count);
                if (table != null)
                {
                    var stockList = ExcelToGrid(table);
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
            }

            return true;
        }

        private List<TemplateStock> ExcelToGrid(Model.Data.DataTable excelData)
        {
            List<TemplateStock> stockList = new List<TemplateStock>();
            var template = GetSelectTemplate();
            if(template == null)
                return stockList;

            //var secuInfoList = _secudbdao.Get(2);
            //用于获取二维表表头名字与Excel中表头进行匹配，获取Excel DataTable中列的位置
            HSGrid hsGrid = _gridConfig.GetGid(GridStock);
            var gridColumns = hsGrid.Columns;

            //标签与属性名映射表
            var attFieldMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(TemplateStock));
            
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
                            field.SetValue(stock, val.GetStr());
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
                var table = GridToExecl(tempStocks);
                ExcelUtil.CreateExcel(fileDialog.FileName, table);
            }
        }

        private Model.Data.DataTable GridToExecl(List<TemplateStock> tempStocks)
        {
            HSGrid hsGrid = _gridConfig.GetGid(GridStock);
            var gridColumns = hsGrid.Columns;

            var sheetConfig = ConfigManager.Instance.GetImportConfig().GetSheet("stocktemplate");
            Dictionary<string, DataColumnHeader> colHeadMap = new Dictionary<string, DataColumnHeader>();
            foreach (var column in sheetConfig.Columns)
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
            for (int i = 0, count = sheetConfig.Columns.Count; i < count; i++)
            {
                if (!excelNameColumnIndexMap.ContainsKey(sheetConfig.Columns[i].Name))
                {
                    excelNameColumnIndexMap.Add(sheetConfig.Columns[i].Name, i);
                }
            }

            Dictionary<string, int> fieldNameColumnIndexMap = new Dictionary<string, int>();
            for (int i = 0, count = gridColumns.Count; i < count; i++)
            {
                var column = gridColumns[i];
                int index = sheetConfig.Columns.FindIndex(p => p.Name.Equals(column.Text));
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

            var attFieldMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(TemplateStock));
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
