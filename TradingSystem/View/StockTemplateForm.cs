using Config;
using Controls;
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
                StockTemplate stockTemplate = JsonUtil.DeserializeObject<StockTemplate>(json);

                Model.Data.DataTable dataTable = new Model.Data.DataTable 
                {
                    ColumnIndex = new Dictionary<string,int>(),
                    Rows = new List<Model.Data.DataRow>()
                };

                Model.Data.DataRow dataRow = new Model.Data.DataRow 
                {
                    Columns = new List<DataValue>()
                };
                var columns = _tempGridView.GridColumns;
                for(int i = 0, count = columns.Count; i < count; i++)
                {
                    var column = columns[i];
                    if (!dataTable.ColumnIndex.ContainsKey(column.Name))
                    {
                        dataTable.ColumnIndex.Add(column.Name, i);
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
                dataTable.Rows.Add(dataRow);
                _tempGridView.FillData(dataTable);
            }
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
            dialog.Owner = this;
            dialog.StartPosition = FormStartPosition.CenterParent;
            //
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
