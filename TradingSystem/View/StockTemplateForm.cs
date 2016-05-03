using Config;
using Controls;
using DBAccess;
using Model.Data;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;

namespace TradingSystem.View
{
    public partial class StockTemplateForm : Forms.BaseForm
    {
        private GridConfig _gridConfig = null;
        private const string GridTemplate = "stocktemplate";
        private const string GridStock = "templatestock";
        HSGridView _tempGridView;
        HSGridView _stockGridView;
        StockTemplateDAO _dbdao = new StockTemplateDAO();

        public StockTemplateForm()
            :base()
        {
            InitializeComponent();
        }

        public StockTemplateForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;
        }

        private void Form_Load(object sender, System.EventArgs e)
        {
            _tempGridView = new HSGridView(_gridConfig.GetGid(GridTemplate));
            _tempGridView.Dock = DockStyle.Fill;

            _stockGridView = new HSGridView(_gridConfig.GetGid(GridStock));
            _stockGridView.Dock = DockStyle.Fill;

            splitContainerChild.Panel1.Controls.Add(_tempGridView);
            splitContainerChild.Panel2.Controls.Add(_stockGridView);

            LoadData();
        }

        private void Form_LoadActived(string json)
        {
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
                _tempGridView.FillData(dataTable);
            }
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
                            dataValue.Value = stockTemplate.TemplateNo;
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
                            dataValue.Value = stockTemplate.WeightType;
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
                            dataValue.Value = stockTemplate.ReplaceType;
                        }
                        break;
                    case "st_replacetemplate":
                        {
                            dataValue.Value = "";
                        }
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
                if (valIndex >= dataRow.Columns.Count)
                {
                    continue;
                }

                DataValue dataValue = dataRow.Columns[valIndex];
                switch (column.Name)
                {
                    case "st_order":
                        {
                            stockTemplate.TemplateNo = dataValue.GetInt();
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
                            stockTemplate.WeightType = dataValue.GetInt();
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
                            stockTemplate.ReplaceType = dataValue.GetInt();
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

        private void LoadData()
        { 
            //Load data from database

            //Fill data into gridview
        }

        #region button click

        private void Button_Add_Click(object sender, System.EventArgs e)
        {
            //Dictionary<string, int> columnIndex;
            //Model.Data.DataRow dataRow = _tempGridView.GetSelectedRow(out columnIndex);
            //StockTemplate stockTemplate = GetDialogData(dataRow, columnIndex);
            //string json = JsonUtil.SerializeObject(stockTemplate);

            TemplateDialog dialog = new TemplateDialog();
            dialog.SaveFormData += new FormDataSaveHandler(Dialog_SaveData);
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

        private StockTemplate SaveTemplateToDB(StockTemplate stockTemplate)
        {

            int newid = _dbdao.CreateTemplate(stockTemplate.TemplateName, stockTemplate.WeightType, stockTemplate.FutureCopies, stockTemplate.MarketCapOpt, stockTemplate.Benchmark, 11111);
            stockTemplate.TemplateNo = newid;

            return stockTemplate;
        }

        private void Dialog_SaveData(object sender, object data)
        {
            if (data is StockTemplate)
            {
                StockTemplate stockTemplate = data as StockTemplate;
                stockTemplate = SaveTemplateToDB(stockTemplate);

                //TODO: add into the view
            }
        }

        private void Button_Modify_Click(object sender, System.EventArgs e)
        {
            Dictionary<string, int> columnIndex;
            Model.Data.DataRow dataRow = _tempGridView.GetSelectedRow(out columnIndex);
            StockTemplate stockTemplate = GetDialogData(dataRow, columnIndex);
            string json = JsonUtil.SerializeObject(stockTemplate);

            TemplateDialog dialog = new TemplateDialog();
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.OnLoadFormActived(json);
            dialog.ShowDialog();
            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {

            }
            else
            {
                dialog.Close();
            }
        }
        #endregion
    }
}
