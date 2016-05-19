using Config;
using Controls.Entity;
using Controls.GridView;
using DBAccess;
using Model.Data;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Util;

namespace TradingSystem.View
{
    public partial class SpotTemplateForm : Forms.DefaultForm
    {
        public enum TempChangeType
        {
            New = 0,
            Update = 1,
        }

        private GridConfig _gridConfig = null;
        private const string GridTemplate = "stocktemplate";
        private const string GridStock = "templatestock";

        private StockTemplateDAO _tempdbdao = new StockTemplateDAO();
        private TemplateStockDAO _stockdbdao = new TemplateStockDAO();

        private SortableBindingList<StockTemplate> _tempDataSource = new SortableBindingList<StockTemplate>(new List<StockTemplate>());
        private SortableBindingList<TemplateStock> _spotDataSource = new SortableBindingList<TemplateStock>(new List<TemplateStock>());

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

            tempGridView.ClickRow += new ClickRowHandler(GridView_Template_ClickRow);

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        #region load control
        private void Form_LoadControl(object sender, object data)
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
        }

        #endregion

        #region load data

        private void Form_LoadData(object sender, object data)
        {
            _tempDataSource.Clear();
            _spotDataSource.Clear();

            var items = _tempdbdao.Get(-1);
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

            string json = JsonUtil.SerializeObject(stockTemplate);

            TemplateDialog dialog = new TemplateDialog();
            dialog.SaveData += new FormLoadHandler(Dialog_ModifyTemplate);
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnFormActived(json);
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

        private void Dialog_NewTemplate(object sender, object data)
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
        }

        private void Dialog_ModifyTemplate(object sender, object data)
        {
            if (!(data is StockTemplate))
            {
                return;
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

            foreach (var stock in _spotDataSource)
            {
                stock.TemplateNo = template.TemplateId;
                string newid = _stockdbdao.Create(stock);
                if (string.IsNullOrEmpty(newid))
                {
                    //TODO: popup the error message
                }
            }
        }

        private void ToolStripButton_DeleteStock_Click(object sender, EventArgs e)
        {
            StockTemplate template = GetSelectTemplate();

            List<int> selectIndex = TSDataGridViewHelper.GetSelectRowIndex(this.secuGridView);
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
                }
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
                    var stockList = ExcelToGrid(table);
                    _spotDataSource.Clear();
                    foreach (var stock in stockList)
                    {
                        _spotDataSource.Add(stock);
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

            HSGrid hsGrid = _gridConfig.GetGid(GridStock);
            var columns = hsGrid.Columns;
            var attFieldMap = TSDGVColumnBindingHelper.GetPropertyBinding(typeof(TemplateStock));
            
            Dictionary<string, int> fieldColumnIndex = new Dictionary<string, int>();
            
            for (int i = 0, count = columns.Count; i < count; i++)
            {
                var column = columns[i];
                if (excelData.ColumnIndex.ContainsKey(column.Text))
                {
                    if (!fieldColumnIndex.ContainsKey(column.Name))
                    {
                        fieldColumnIndex.Add(column.Name, excelData.ColumnIndex[column.Text]);
                    }
                }
            }

            foreach (var row in excelData.Rows)
            {
                TemplateStock stock = new TemplateStock();
                stock.TemplateNo = template.TemplateId;

                foreach (var kv in fieldColumnIndex)
                {
                    if (!attFieldMap.ContainsKey(kv.Key) || !fieldColumnIndex.ContainsKey(kv.Key))
                        continue;

                    string fieldName = attFieldMap[kv.Key];
                    int valIndex = fieldColumnIndex[kv.Key];

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

                stockList.Add(stock);
            }

            return stockList;
        }

        #endregion
    }
}
