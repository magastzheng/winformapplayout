using Config;
using Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
