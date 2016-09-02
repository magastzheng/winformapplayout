using BLL.TradeCommand;
using Config;
using Controls.Entity;
using Controls.GridView;
using Model.Binding.BindingUtil;
using Model.UI;
using System;
using System.Collections.Generic;

namespace TradingSystem.View
{
    public partial class InstanceManagementForm : Forms.DefaultForm
    {
        private const string GridId = "instancemanagement";
        private GridConfig _gridConfig = null;

        private SortableBindingList<InstanceItem> _dataSource = new SortableBindingList<InstanceItem>(new List<InstanceItem>());

        private TradeInstanceBLL _tradeInstanceBLL = new TradeInstanceBLL();

        public InstanceManagementForm() :
            base()
        {
            InitializeComponent();
        }

        public InstanceManagementForm(GridConfig gridConfig)
            : this()
        {
            _gridConfig = gridConfig;

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);
        }

        private bool Form_LoadControl(object sender, object data)
        {
            TSDataGridViewHelper.AddColumns(this.gridView, _gridConfig.GetGid(GridId));
            Dictionary<string, string> colDataMap = GridViewBindingHelper.GetPropertyBinding(typeof(InstanceItem));
            TSDataGridViewHelper.SetDataBinding(this.gridView, colDataMap);

            this.gridView.DataSource = _dataSource;

            return true;
        }

        private bool Form_LoadData(object sender, object data)
        {
            //TODO:
            var instances = _tradeInstanceBLL.GetAllInstance();
            foreach (var instance in instances)
            {
                InstanceItem instItem = new InstanceItem 
                {
                    InstanceId = instance.InstanceId,
                    InstanceCode = instance.InstanceCode,
                    PortfolioId = instance.PortfolioId,
                    PortfolioCode = instance.PortfolioCode,
                    PortfolioName = instance.PortfolioName,
                    TemplateId = instance.TemplateId,
                    TemplateName = instance.TemplateName,
                    MonitorUnitName = instance.MonitorUnitName,
                    DCreatedDate = instance.CreatedDate,
                    Owner = instance.Owner
                };

                _dataSource.Add(instItem);
            }

            return true;
        }
    }
}
