using BLL.Template;
using Config;
using Controls;
using DBAccess;
using Model.Data;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TradingSystem.Dialog;
using Util;

namespace TradingSystem.View
{
    public partial class StockTemplateForm : Forms.BaseForm
    {
        private GridConfig _gridConfig = null;
        private const string GridTemplate = "stocktemplate";
        private const string GridStock = "templatestock";
        private HSGridView _tempGridView;
        private HSGridView _stockGridView;
        //private StockTemplateDAO _tempdbdao = new StockTemplateDAO();
        //private TemplateStockDAO _stockdbdao = new TemplateStockDAO();
        //private SecurityInfoDAO _secudbdao = new SecurityInfoDAO();
        private TemplateBLL _templateBLL = new TemplateBLL();
        private bool _isStockChange = false;

        public StockTemplateForm()
            :base()
        {
            InitializeComponent();
        }

        public StockTemplateForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            _tempGridView = new HSGridView(_gridConfig.GetGid(GridTemplate));
            _tempGridView.Dock = DockStyle.Fill;
            _tempGridView.ClickRow += new ClickRowHandler(GridView_Template_ClickRow);

            _stockGridView = new HSGridView(_gridConfig.GetGid(GridStock));
            _stockGridView.Dock = DockStyle.Fill;

            splitContainerChild.Panel1.Controls.Add(_tempGridView);
            splitContainerChild.Panel2.Controls.Add(_stockGridView);
        }

        private void Form_Load(object sender, System.EventArgs e)
        {
            LoadData();
        }

        private void GridView_Template_ClickRow(object sender, Model.Data.DataRow dataRow, Dictionary<string, int> ColumnIndex)
        {
            //
            StockTemplate template = GetDialogData(dataRow, ColumnIndex);
            if (template != null && template.TemplateId > 0)
            {
                LoadTemplateStock(template.TemplateId);
            }
        }

        private void Form_LoadActived(string json)
        {
            //StockTemplateDAO _dbdao = new StockTemplateDAO();
            var items = _templateBLL.GetTemplateByUser(-1);
            json = JsonUtil.SerializeObject(items);

            if(!string.IsNullOrEmpty(json))
            {
                List<StockTemplate> stockTemplateList = JsonUtil.DeserializeObject<List<StockTemplate>>(json);

                Model.Data.DataTable dataTable = new Model.Data.DataTable 
                {
                    ColumnIndex = new Dictionary<string,int>(),
                    Rows = new List<Model.Data.DataRow>()
                };

                Dictionary<string, int> columnIndex = new Dictionary<string, int>();
                foreach (var stockTemplate in stockTemplateList)
                {
                    Model.Data.DataRow dataRow = GetDataRow(stockTemplate, ref columnIndex);
                    if (dataRow != null)
                    {
                        dataTable.Rows.Add(dataRow);
                    }
                }
                if (columnIndex.Count > 0)
                {
                    dataTable.ColumnIndex = columnIndex;
                }

                _tempGridView.Clear();
                _tempGridView.FillData(dataTable);
            }

            if (items.Count > 0)
            {
                LoadTemplateStock(items[0].TemplateId);
            }
        }

        private void LoadTemplateStock(int templateNo)
        {
            if (templateNo < 0)
                return;

            var stocks = _templateBLL.GetStocks(templateNo);
            Model.Data.DataTable dataTable = new Model.Data.DataTable
            {
                ColumnIndex = new Dictionary<string, int>(),
                Rows = new List<Model.Data.DataRow>()
            };

            Dictionary<string, int> columnIndex = new Dictionary<string, int>();
            foreach (var stock in stocks)
            {
                Model.Data.DataRow dataRow = GetDataRow(stock, ref columnIndex);
                if (dataRow != null)
                {
                    dataTable.Rows.Add(dataRow);
                }
            }
            if (columnIndex.Count > 0)
            {
                dataTable.ColumnIndex = columnIndex;
            }

            _stockGridView.Clear();
            _stockGridView.FillData(dataTable);
        }

        private Model.Data.DataRow GetDataRow(StockTemplate stockTemplate, ref Dictionary<string,int> columnIndex)
        {
            Model.Data.DataRow dataRow = new Model.Data.DataRow
            {
                Columns = new List<DataValue>()
            };
            var columns = _tempGridView.GridColumns;
            for (int i = 0, count = columns.Count; i < count; i++)
            {
                var column = columns[i];
                if (!columnIndex.ContainsKey(column.Name))
                {
                    columnIndex.Add(column.Name, i);
                }
                DataValue dataValue = new DataValue();
                dataValue.Type = column.ValueType;
                switch (column.Name)
                {
                    case "st_order":
                        {
                            dataValue.Value = stockTemplate.TemplateId;
                        }
                        break;
                    case "st_name":
                        {
                            dataValue.Value = stockTemplate.TemplateName;
                        }
                        break;
                    case "st_status":
                        {
                            dataValue.Value = "Normal";
                        }
                        break;
                    case "st_weighttype":
                        {
                            dataValue.Value = stockTemplate.EWeightType;
                        }
                        break;
                    case "st_futurescopies":
                        {
                            dataValue.Value = stockTemplate.FutureCopies;
                        }
                        break;
                    case "st_marketcapoption":
                        {
                            dataValue.Value = stockTemplate.MarketCapOpt;
                        }
                        break;
                    case "st_benchmark":
                        {
                            dataValue.Value = stockTemplate.Benchmark;
                        }
                        break;
                    case "st_addeddate":
                        {
                            dataValue.Value = DateTime.Now.ToShortDateString();
                        }
                        break;
                    case "st_addedtime":
                        {
                            dataValue.Value = DateTime.Now.ToShortTimeString();
                        }
                        break;
                    case "st_modifieddate":
                        {
                            dataValue.Value = DateTime.Now.ToShortDateString();
                        }
                        break;
                    case "st_modifiedtime":
                        {
                            dataValue.Value = DateTime.Now.ToShortTimeString();
                        }
                        break;
                    case "st_addeduser":
                        {
                            dataValue.Value = "";
                        }
                        break;
                    case "st_editableuser":
                        {
                            dataValue.Value = "";
                        }
                        break;
                    case "st_viewuser":
                        {
                            dataValue.Value = "";
                        }
                        break;
                    case "st_replacetype":
                        {
                            dataValue.Value = stockTemplate.EReplaceType;
                        }
                        break;
                    case "st_replacetemplate":
                        {
                            dataValue.Value = "";
                        }
                        break;
                    default:
                        break;
                }

                dataRow.Columns.Add(dataValue);
            }

            return dataRow;
        }

        private StockTemplate GetDialogData(Model.Data.DataRow dataRow, Dictionary<string, int> columnIndex)
        {
            StockTemplate stockTemplate = new StockTemplate();

            var columns = _tempGridView.GridColumns;
            for (int i = 0, count = columns.Count; i < count; i++)
            {
                var column = columns[i];
                int valIndex = -1;
                foreach (var kv in columnIndex)
                {
                    if (kv.Key == column.Name)
                    {
                        valIndex = kv.Value;
                        break;
                    }
                }
                if (valIndex < 0 || valIndex >= dataRow.Columns.Count)
                {
                    continue;
                }

                DataValue dataValue = dataRow.Columns[valIndex];
                switch (column.Name)
                {
                    case "st_order":
                        {
                            stockTemplate.TemplateId = dataValue.GetInt();
                        }
                        break;
                    case "st_name":
                        {
                            stockTemplate.TemplateName = dataValue.GetStr();
                        }
                        break;
                    case "st_status":
                        {
                            //dataValue.Value = "Normal";
                        }
                        break;
                    case "st_weighttype":
                        {
                            stockTemplate.EWeightType = (WeightType)dataValue.GetInt();
                        }
                        break;
                    case "st_futurescopies":
                        {
                            stockTemplate.FutureCopies = dataValue.GetInt();
                        }
                        break;
                    case "st_marketcapoption":
                        {
                            stockTemplate.MarketCapOpt = dataValue.GetDouble();
                        }
                        break;
                    case "st_benchmark":
                        {
                            stockTemplate.Benchmark = dataValue.GetStr();
                        }
                        break;
                    case "st_addeddate":
                        {
                            //dataValue.Value = DateTime.Now.ToShortDateString();
                        }
                        break;
                    case "st_addedtime":
                        {
                            //dataValue.Value = DateTime.Now.ToShortTimeString();
                        }
                        break;
                    case "st_modifieddate":
                        {
                            //dataValue.Value = DateTime.Now.ToShortDateString();
                        }
                        break;
                    case "st_modifiedtime":
                        {
                            //dataValue.Value = DateTime.Now.ToShortTimeString();
                        }
                        break;
                    case "st_addeduser":
                        {
                            //dataValue.Value = "";
                        }
                        break;
                    case "st_editableuser":
                        {
                            //dataValue.Value = "";
                        }
                        break;
                    case "st_viewuser":
                        {
                            //dataValue.Value = "";
                        }
                        break;
                    case "st_replacetype":
                        {
                            stockTemplate.EReplaceType = (ReplaceType)dataValue.GetInt();
                        }
                        break;
                    case "st_replacetemplate":
                        {
                            //dataValue.Value = "";
                        }
                        break;
                }

                dataRow.Columns.Add(dataValue);
            }

            return stockTemplate;
        }

        private Model.Data.DataRow GetDataRow(TemplateStock tempStock, ref Dictionary<string, int> columnIndex)
        {
            Model.Data.DataRow dataRow = new Model.Data.DataRow
            {
                Columns = new List<DataValue>()
            };
            var columns = _stockGridView.GridColumns;
            for (int i = 0, count = columns.Count; i < count; i++)
            {
                var column = columns[i];
                if (!columnIndex.ContainsKey(column.Name))
                {
                    columnIndex.Add(column.Name, i);
                }
                DataValue dataValue = new DataValue();
                dataValue.Type = column.ValueType;
                switch (column.Name)
                {
                    case "ts_selection":
                        {
                            dataValue.Value = 0;
                        }
                        break;
                    case "ts_secucode":
                        {
                            dataValue.Value = tempStock.SecuCode;
                        }
                        break;
                    case "ts_secuname":
                        {
                            dataValue.Value = tempStock.SecuName;
                        }
                        break;
                    case "ts_market":
                        {
                            dataValue.Value = tempStock.Exchange;
                        }
                        break;
                    case "ts_amount":
                        {
                            dataValue.Value = tempStock.Amount;
                        }
                        break;
                    case "ts_marketcap":
                        {
                            dataValue.Value = tempStock.MarketCap;
                        }
                        break;
                    case "ts_marketcapweight":
                        {
                            dataValue.Value = tempStock.MarketCapWeight;
                        }
                        break;
                    case "ts_setweight":
                        {
                            dataValue.Value = tempStock.SettingWeight;
                        }
                        break;
                    default:
                        break;
                }

                dataRow.Columns.Add(dataValue);
            }

            return dataRow;
        }

        private TemplateStock GetStockDialogData(Model.Data.DataRow dataRow, Dictionary<string, int> columnIndex)
        {
            TemplateStock tempStock = new TemplateStock();

            var columns = _stockGridView.GridColumns;
            for (int i = 0, count = columns.Count; i < count; i++)
            {
                var column = columns[i];
                int valIndex = -1;
                foreach (var kv in columnIndex)
                {
                    if (kv.Key == column.Name)
                    {
                        valIndex = kv.Value;
                        break;
                    }
                }
                if (valIndex < 0 || valIndex >= dataRow.Columns.Count)
                {
                    continue;
                }

                DataValue dataValue = dataRow.Columns[valIndex];
                switch (column.Name)
                {
                    case "ts_secucode":
                        {
                            tempStock.SecuCode = dataValue.GetStr();
                        }
                        break;
                    case "ts_secuname":
                        {
                            tempStock.SecuName = dataValue.GetStr();
                        }
                        break;
                    case "ts_market":
                        {
                            tempStock.Exchange = dataValue.GetStr();
                        }
                        break;
                    case "ts_amount":
                        {
                            tempStock.Amount = dataValue.GetInt();
                        }
                        break;
                    case "ts_marketcap":
                        {
                            tempStock.MarketCap = dataValue.GetDouble();
                        }
                        break;
                    case "ts_marketcapweight":
                        {
                            tempStock.MarketCapWeight = dataValue.GetDouble();
                        }
                        break;
                    case "ts_setweight":
                        {
                            tempStock.SettingWeight = dataValue.GetDouble();
                        }
                        break;
                    default:
                        break;
                }

                dataRow.Columns.Add(dataValue);
            }

            return tempStock;
        }

        private void LoadData()
        {
            //Load data from database

            //Fill data into gridview
        }

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
            Dictionary<string, int> columnIndex;
            Model.Data.DataRow dataRow = _tempGridView.GetSelectedRow(out columnIndex);
            return GetDialogData(dataRow, columnIndex);
        }

        private StockTemplate SaveTemplateToDB(StockTemplate stockTemplate, TempChangeType type)
        {
            switch(type)
            {
                case TempChangeType.New:
                    {
                        var newTemplate = _templateBLL.CreateTemplate(stockTemplate);
                        stockTemplate.TemplateId = newTemplate.TemplateId;
                    }
                    break;
                case TempChangeType.Update:
                    {
                        int tempid = _templateBLL.UpdateTemplate(stockTemplate);
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
                    Dictionary<string, int> columnIndex = new Dictionary<string, int>();
                    Model.Data.DataRow dataRow = GetDataRow(stockTemplate, ref columnIndex);
                    _tempGridView.FillRow(dataRow, columnIndex);
                }
            }

            return true;
        }

        private bool Dialog_ModifyTemplate(object sender, object data)
        {
            if (data is StockTemplate)
            {
                StockTemplate stockTemplate = data as StockTemplate;
                stockTemplate = SaveTemplateToDB(stockTemplate, TempChangeType.Update);

                //update the row in the view
                if (stockTemplate.TemplateId > 0)
                {
                    Dictionary<string, int> columnIndex = new Dictionary<string, int>();
                    Model.Data.DataRow dataRow = GetDataRow(stockTemplate, ref columnIndex);
                    _tempGridView.UpdateRow(_tempGridView.CurrentRow.Index, dataRow);
                }
            }

            return true;
        }

        #endregion

        public enum TempChangeType
        { 
            New = 0,
            Update = 1,
        }

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
            Model.Data.DataTable dataTable = _stockGridView.DataTable;
            if (template == null || dataTable == null)
            {
                //TODO: error handle
                return;
            }

            foreach (Model.Data.DataRow dataRow in dataTable.Rows)
            {
                TemplateStock stock = GetStockDialogData(dataRow, dataTable.ColumnIndex);
                stock.TemplateNo = template.TemplateId;
                //string newid = _stockdbdao.Create(stock);
                //if (string.IsNullOrEmpty(newid))
                //{ 
                //    //TODO: popup the error message
                //}
            }
        }

        private void ToolStripButton_DeleteStock_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();
            Model.Data.DataTable dataTable = _stockGridView.GetSeletedRows();
            if (template == null || dataTable == null)
            {
                //TODO: popup the message there is no row selected
                return;
            }

            foreach (Model.Data.DataRow dataRow in dataTable.Rows)
            {
                TemplateStock stock = GetStockDialogData(dataRow, dataTable.ColumnIndex);

                //string newid = _stockdbdao.Delete(template.TemplateId, stock.SecuCode);
                //if (string.IsNullOrEmpty(newid))
                //{
                //    //TODO: popup the error message
                //}
            }
        }

        private void ToolStripButton_AddStock_Click(object sender, EventArgs e)
        {
            PortfolioSecurityDialog psDialog = new PortfolioSecurityDialog();
            psDialog.ShowDialog();
        }

        private void ToolStripButton_ModifyStock_Click(object sender, EventArgs e)
        {
            PortfolioSecurityDialog psDialog = new PortfolioSecurityDialog();
            psDialog.ShowDialog();
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
                    var gridData = ExcelToGrid(table);
                    this._stockGridView.FillData(gridData);
                    this._isStockChange = true;
                }
            }

            return true;
        }

        private Model.Data.DataTable ExcelToGrid(Model.Data.DataTable excelData)
        {
            Model.Data.DataTable dataTable = new Model.Data.DataTable 
            {
                ColumnIndex = new Dictionary<string,int>(),
                Rows = excelData.Rows
            };

            var columns = _stockGridView.GridColumns;
            for (int i = 0, count = columns.Count; i < count; i++)
            {
                var column = columns[i];
                if (excelData.ColumnIndex.ContainsKey(column.Text))
                {
                    if (!dataTable.ColumnIndex.ContainsKey(column.Name))
                    {
                        dataTable.ColumnIndex.Add(column.Name, excelData.ColumnIndex[column.Text]);
                    }
                }
                else
                {
                    bool isExisted = false;
                    int index = -1;
                    foreach (var kv in excelData.ColumnIndex)
                    {
                        if (kv.Key.StartsWith(column.Text) || column.Text.StartsWith(kv.Key))
                        {
                            index = kv.Value;
                            isExisted = true;
                        }
                    }
                    if (isExisted && !dataTable.ColumnIndex.ContainsKey(column.Name))
                    {
                        dataTable.ColumnIndex.Add(column.Name, index);
                    }
                }
            }

            return dataTable;
        }

        #endregion
    }
}
