using BLL.Template;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TradingSystem.View
{
    public partial class HistSpotTemplateForm : Forms.DefaultForm
    {
        private GridConfig _gridConfig = null;
        private const string GridTemplate = "histstocktemplate";
        private const string GridStock = "histtemplatestock";

        private HistTemplateBLL _templateBLL = new HistTemplateBLL();

        private SortableBindingList<HistStockTemplate> _tempDataSource = new SortableBindingList<HistStockTemplate>(new List<HistStockTemplate>());
        private SortableBindingList<HistTemplateStock> _secuDataSource = new SortableBindingList<HistTemplateStock>(new List<HistTemplateStock>());

        public HistSpotTemplateForm()
            :base()
        {
            InitializeComponent();
        }

        public HistSpotTemplateForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        #region load control

        private bool Form_LoadControl(object sender, object data)
        {
            //set the monitorGridView
            TSDataGridViewHelper.AddColumns(this.tempGridView, _gridConfig.GetGid(GridTemplate));
            Dictionary<string, string> tempColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(HistStockTemplate));
            TSDataGridViewHelper.SetDataBinding(this.tempGridView, tempColDataMap);

            //set the securityGridView
            TSDataGridViewHelper.AddColumns(this.secuGridView, _gridConfig.GetGid(GridStock));
            Dictionary<string, string> securityColDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(HistTemplateStock));
            TSDataGridViewHelper.SetDataBinding(this.secuGridView, securityColDataMap);

            this.tempGridView.DataSource = _tempDataSource;
            this.secuGridView.DataSource = _secuDataSource;

            return true;
        }

        #endregion

        #region load data

        private bool Form_LoadData(object sender, object data)
        {
            _tempDataSource.Clear();
            _secuDataSource.Clear();

            var items = _templateBLL.GetTemplates();
            if (items != null)
            {
                foreach (var item in items)
                {
                    _tempDataSource.Add(item);
                }

                if (items.Count > 0)
                {
                    LoadTemplateStock(items[0].ArchiveId);
                }
            }

            //TODO: load data from database
            return true;
        }

        private void LoadTemplateStock(int archiveId)
        {
            if (archiveId < 0)
                return;

            _secuDataSource.Clear();
            var stocks = _templateBLL.GetStocks(archiveId);
            if (stocks != null)
            {
                foreach (var stock in stocks)
                {
                    _secuDataSource.Add(stock);
                }
            }
        }

        #endregion
    }
}
