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
            HSGridView tempGridView = new HSGridView(_gridConfig.GetGid(GridTemplate));
            tempGridView.Dock = DockStyle.Fill;

            HSGridView stockGridView = new HSGridView(_gridConfig.GetGid(GridStock));
            stockGridView.Dock = DockStyle.Fill;

            splitContainerChild.Panel1.Controls.Add(tempGridView);
            splitContainerChild.Panel2.Controls.Add(stockGridView);
        }
    }
}
