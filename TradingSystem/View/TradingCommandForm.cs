using Config;
using Controls;
using TradingSystem.Controller;
using Model;
using Model.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controls.Entity;

namespace TradingSystem.View
{
    public partial class TradingCommandForm : Form
    {
        private const int MAX_ENTRUST_AMOUNT = 10;
        private MainController _mainController;

        public MainController MainController 
        {
            set { _mainController = value; }
        }

        public TradingCommandForm(GridConfig gridConfig)
        {
            _gridConfig = gridConfig;

            InitializeComponent();
        }

        public void LoadData()
        {
            //var retCode = _mainController.StrategyBLL.QueryTrading();
            var retCode = _mainController.StrategyBLL.QueryTemplate();
            //retCode = _mainController.StrategyBLL.QueryTemplateStock(-1);
            //retCode = _mainController.StrategyBLL.QueryDeal();
            //Console.WriteLine(retCode);
        }

        public void SetData()
        {
            var tcItems = GenerateTestCommandTrading();
            FillCommandTrading(tcItems);

            var csItems = GenerateTestCommandSecurity();
            FillCommandSecurity(csItems);

            var dfItems = GenerateDealFlowData();
            FillDealFlow(dfItems);

            var eiItems = GetDefaultEntrustData();
            FillEntrustGrid(eiItems);

            Dictionary<string, string> efColDataMap;
            var efDataSet = GenerateEntrustFlowData(out efColDataMap);
            FillEntrustFlow(efDataSet, efColDataMap);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.dataGridViewCmdTrading.UpdateRelatedDataGrid = DataGridViewCmdTrading_Select;

            InitializeControl();
            //SetData();
            LoadData();
        }

        private void InitializeControl()
        {
            InitializeCombobox();
        }

        #region combobox
        private void InitializeCombobox()
        {
            var spotBuy = ConfigManager.Instance.GetComboConfig().GetComboOption("spotbuy");
            SetComboBox(this.comboBoxSpotBuy, spotBuy);

            var spotSell = ConfigManager.Instance.GetComboConfig().GetComboOption("spotsell");
            SetComboBox(this.comboBoxSpotSell, spotSell);

            var futureBuy = ConfigManager.Instance.GetComboConfig().GetComboOption("futurebuy");
            SetComboBox(this.comboBoxFutureBuy, futureBuy);

            var futureSell = ConfigManager.Instance.GetComboConfig().GetComboOption("futuresell");
            SetComboBox(this.comboBoxFutureSell, futureSell);
        }

        private void SetComboBox(ComboBox comboBox, ComboOption comboOption)
        {
            foreach (var item in comboOption.Items)
            {
                comboBox.Items.Add(item);
            }

            comboBox.SelectedIndex = 0;
        }


        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null)
                return;

            ComboOptionItem selectedItem = comboBox.SelectedItem as ComboOptionItem;
            if (selectedItem == null)
                return;

            Console.WriteLine("Selected: " + comboBox.Name + " " + selectedItem.Id);
            switch (comboBox.Name)
            {
                case "comboBoxSpotBuy":

                    break;
                case "comboBoxSpotSell":
                    break;
                case "comboBoxFutureBuy":
                    break;
                case "comboBoxFutureSell":
                    break;
            }
            if (sender is ComboBox)
            {
                Console.WriteLine("Event: ", sender.ToString());
            }
        }
        #endregion

        #region 填充数据

        private void FillRawData()
        { 
            
        }

        #endregion

        #region 填充指令交易

        private List<TradingCommandItem> GenerateTestCommandTrading()
        {
            List<TradingCommandItem> tcItems = new List<TradingCommandItem>();
            TradingCommandItem item1 = new TradingCommandItem
            {
                Selection = true,
                CommandId = 12,
                //CommandType = "Test",
                //ExecuteType = "Buy",
                CommandNum = 100,
                TargetNum = 150,
                BaisPrice = 0.25,
                LongMoreThan = 0.35,
                BearMoreThan = 0.25,
                LongRatio = 0.35,
                BearRatio = 0.12,
                CommandAmount = 130,
                EntrustedAmount = 120,
                CommandMoney = 154014.00,
                Exposure = 12.0,
                //StartDate = "20160321",
                //EndDate = "20160321",
                //StartTime = "134200",
                //EndTime = "134500",
                DispatchTime = "134311",
                ExecutePerson = "Magast",
                DispatchPerson = "Youyo",
                //InstanceId = "1120",
                InstanceNo = "A110",
                MonitorUnit = "M10"
            };
            tcItems.Add(item1);

            TradingCommandItem item2 = new TradingCommandItem
            {
                Selection = true,
                CommandId = 20,
                //CommandType = "Test",
                //ExecuteType = "Buy",
                CommandNum = 100,
                TargetNum = 150,
                BaisPrice = 0.25,
                LongMoreThan = 0.35,
                BearMoreThan = 0.25,
                LongRatio = 0.35,
                BearRatio = 0.12,
                CommandAmount = 130,
                EntrustedAmount = 120,
                CommandMoney = 154014.00,
                Exposure = 12.0,
                //StartDate = "20160321",
                //EndDate = "20160321",
                //StartTime = "134200",
                //EndTime = "134500",
                DispatchTime = "134311",
                ExecutePerson = "Magast",
                DispatchPerson = "Youyo",
                //InstanceId = "1120",
                InstanceNo = "A110",
                MonitorUnit = "M10"
            };
            tcItems.Add(item2);

            return tcItems;
        }

        private void FillCommandTrading(List<TradingCommandItem> tcItems)
        {
            foreach (var dataItem in tcItems)
            {
                int rowIndex = this.dataGridViewCmdTrading.Rows.Add();
                DataGridViewRow row = this.dataGridViewCmdTrading.Rows[rowIndex];

                bool isSelected = dataItem.Selection;
                row.Cells["tc_selection"].Value = isSelected;
                row.Cells["tc_commandno"].Value = dataItem.CommandId;
                row.Cells["tc_commandtype"].Value = dataItem.CommandType;
                row.Cells["tc_executetype"].Value = dataItem.ExecuteType;
                row.Cells["tc_commandnum"].Value = dataItem.CommandNum;
                row.Cells["tc_targetnum"].Value = dataItem.TargetNum;
                row.Cells["tc_baisprice"].Value = dataItem.BaisPrice;
                row.Cells["tc_longmorethan"].Value = dataItem.LongMoreThan;
                row.Cells["tc_bearmorethan"].Value = dataItem.BearMoreThan;
                row.Cells["tc_longratio"].Value = dataItem.LongRatio;
                row.Cells["tc_bearratio"].Value = dataItem.BearRatio;
                row.Cells["tc_commandamount"].Value = dataItem.CommandAmount;
                row.Cells["tc_entrustedamount"].Value = dataItem.EntrustedAmount;
                row.Cells["tc_dealamount"].Value = dataItem.DealAmount;
                row.Cells["tc_commandmoney"].Value = dataItem.CommandMoney;
                row.Cells["tc_exposure"].Value = dataItem.Exposure;
                row.Cells["tc_startdate"].Value = dataItem.StartDate;
                row.Cells["tc_enddate"].Value = dataItem.EndDate;
                row.Cells["tc_starttime"].Value = dataItem.StartTime;
                row.Cells["tc_endtime"].Value = dataItem.EndTime;
                row.Cells["tc_dispatchtime"].Value = dataItem.DispatchTime;
                row.Cells["tc_executeperson"].Value = dataItem.ExecutePerson;
                row.Cells["tc_dispatchperson"].Value = dataItem.DispatchPerson;
                row.Cells["tc_instanceid"].Value = dataItem.InstanceId;
                row.Cells["tc_instanceno"].Value = dataItem.InstanceNo;
                row.Cells["tc_monitorunit"].Value = dataItem.MonitorUnit;

                SetSelectionRowBackground(this.dataGridViewCmdTrading, rowIndex, isSelected);
            }
        }

        private List<TradingCommandItem> GetSelectionCommandTradingItems()
        {
            List<TradingCommandItem> selectionItems = new List<TradingCommandItem>();
            var dgv = dataGridViewCmdTrading;
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    TradingCommandItem item = new TradingCommandItem();
                    item.CommandId = (int)row.Cells["tc_commandno"].Value;
                    //item.CommandType = (string)row.Cells["tc_commandtype"].Value;
                    //item.ExecuteType = (string)row.Cells["tc_executetype"].Value;
                    item.CommandNum = (int)row.Cells["tc_commandnum"].Value;
                    item.TargetNum = (int)row.Cells["tc_targetnum"].Value;
                    item.BaisPrice = (double)row.Cells["tc_baisprice"].Value;
                    item.LongMoreThan = (double)row.Cells["tc_longmorethan"].Value;
                    item.BearMoreThan = (double)row.Cells["tc_bearmorethan"].Value;
                    item.LongRatio = (double)row.Cells["tc_longratio"].Value;
                    item.BearRatio = (double)row.Cells["tc_bearratio"].Value;
                    item.CommandAmount = (int)row.Cells["tc_commandamount"].Value;
                    item.EntrustedAmount = (int)row.Cells["tc_entrustedamount"].Value;
                    item.DealAmount = (int)row.Cells["tc_dealamount"].Value;
                    item.CommandMoney = (double)row.Cells["tc_commandmoney"].Value;
                    item.Exposure = (double)row.Cells["tc_exposure"].Value;
                    //item.StartDate = (string)row.Cells["tc_startdate"].Value;
                    //item.EndDate = (string)row.Cells["tc_enddate"].Value;
                    //item.StartTime = (string)row.Cells["tc_starttime"].Value;
                    //item.EndTime = (string)row.Cells["tc_endtime"].Value;
                    item.DispatchTime = (string)row.Cells["tc_dispatchtime"].Value;
                    item.ExecutePerson = (string)row.Cells["tc_executeperson"].Value;
                    item.DispatchPerson = (string)row.Cells["tc_dispatchperson"].Value;
                    //item.InstanceId = (string)row.Cells["tc_instanceid"].Value;
                    item.InstanceNo = (string)row.Cells["tc_instanceno"].Value;
                    item.MonitorUnit = (string)row.Cells["tc_monitorunit"].Value;

                    selectionItems.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return selectionItems;
        }

        private void DataGridViewCmdTrading_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv == null || e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            int selectIndex = dgv.Columns["tc_selection"].Index;
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            int commandNo = (int)row.Cells["tc_commandno"].Value;
            if (e.ColumnIndex == selectIndex)
            {
                bool currentStatus = (bool)row.Cells[e.ColumnIndex].EditedFormattedValue;
                //bool valueStatus = (bool)row.Cells[e.ColumnIndex].Value;

                if (currentStatus)
                {
                    row.Cells[e.ColumnIndex].Value = true;
                    SetSelectionRowBackground(dgv, e.RowIndex, true);
                    //dgv.Rows[e.RowIndex].Selected = true;

                    EntrustItem item = new EntrustItem
                    {
                        Selection = false,
                        CommandNo = commandNo,
                        Copies = 0
                    };

                    FillEntrustGrid(new List<EntrustItem> { item });
                }
                else
                {
                    row.Cells[e.ColumnIndex].Value = false;
                    SetSelectionRowBackground(dgv, e.RowIndex, false);
                    //dgv.Rows[e.RowIndex].Selected = false;
                    RemoveEntrustGrid(commandNo);
                }
            }
        }

        private void DataGridViewCmdTrading_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv == null || e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            //int selectIndex = dgv.Columns["tc_selection"].Index;
            //DataGridViewRow row = dgv.Rows[e.RowIndex];
            //int commandNo = (int)row.Cells["tc_commandno"].Value;
            //if (e.ColumnIndex == selectIndex)
            //{
            //    bool currentStatus = (bool)row.Cells[e.ColumnIndex].EditedFormattedValue;
            //    bool valueStatus = (bool)row.Cells[e.ColumnIndex].Value;

            //    if (currentStatus)
            //    {
            //        if (!valueStatus)
            //        {
            //            row.Cells[e.ColumnIndex].Value = true;
            //            SetSelectionRowBackground(dgv, e.RowIndex, true);
            //            //dgv.Rows[e.RowIndex].Selected = true;

            //            UIEntrustItem item = new UIEntrustItem
            //            {
            //                Selected = 0,
            //                CommandNo = commandNo,
            //                Copies = 0
            //            };

            //            FillEntrustGrid(new List<UIEntrustItem> { item });
            //        }
            //    }
            //    else
            //    {
            //        if (valueStatus)
            //        {
            //            row.Cells[e.ColumnIndex].Value = false;
            //            SetSelectionRowBackground(dgv, e.RowIndex, false);
            //            //dgv.Rows[e.RowIndex].Selected = false;
            //            RemoveEntrustGrid(commandNo);
            //        }
            //    }
            //}
        }

        private void SortCmdTradingGrid()
        {
            this.dataGridViewCmdTrading.Sort(this.dataGridViewCmdTrading.Columns["tc_commanno"], ListSortDirection.Ascending);
        }
        #endregion

        #region 指令证券

        private List<CommandSecurityItem> GenerateTestCommandSecurity()
        {
            List<CommandSecurityItem> csItems = new List<CommandSecurityItem>();

            CommandSecurityItem item1 = new CommandSecurityItem
            {
                Selection = false,
                SecuCode = "000002",
                SecuName = "万科A",
                CommandId = 23,
                FundCode = "FO11123",
                PortfolioName = "Test",
                CommandPrice = 12.23,
                CommandAmount = 12345,
                EntrustDirection = "Buy",
                EntrustedAmount = 1200,
                PriceType = "B1",
                EntrustPrice = 12.20,
                ThisEntrustAmount = 120,
                DealAmount = 200,
                TargetAmount = 250,
                WaitAmount = 50,
                SuspensionFlag = "Up",
                TargetCopies = 5,
                LimitUpPrice = 15.0,
                LimitDownPrice = 10.5,
                LimitUpOrDown = "Up"
            };
            CommandSecurityItem item2 = new CommandSecurityItem
            {
                Selection = false,
                SecuCode = "000001",
                SecuName = "中国平安",
                CommandId = 23,
                FundCode = "FO11123",
                PortfolioName = "Test",
                CommandPrice = 12.23,
                CommandAmount = 12345,
                EntrustDirection = "Buy",
                EntrustedAmount = 1200,
                PriceType = "B1",
                EntrustPrice = 12.20,
                ThisEntrustAmount = 120,
                DealAmount = 200,
                TargetAmount = 250,
                WaitAmount = 50,
                SuspensionFlag = "Up",
                TargetCopies = 5,
                LimitUpPrice = 15.0,
                LimitDownPrice = 10.5,
                LimitUpOrDown = "Up"
            };

            csItems.Add(item1);
            csItems.Add(item2);

            return csItems;
        }

        private void FillCommandSecurity(List<CommandSecurityItem> csItems)
        {
            foreach (var dataItem in csItems)
            {
                int rowIndex = this.dataGridViewCmdSecurity.Rows.Add();
                DataGridViewRow row = this.dataGridViewCmdSecurity.Rows[rowIndex];

                bool isSelected = dataItem.Selection;
                row.Cells["cs_selection"].Value = isSelected;
                row.Cells["cs_secucode"].Value = dataItem.SecuCode;
                row.Cells["cs_secuname"].Value = dataItem.SecuName;
                row.Cells["cs_commandno"].Value = dataItem.CommandId;
                row.Cells["cs_fundcode"].Value = dataItem.FundCode;
                row.Cells["cs_portfolioname"].Value = dataItem.PortfolioName;
                row.Cells["cs_commandprice"].Value = dataItem.CommandPrice;
                row.Cells["cs_commandamount"].Value = dataItem.CommandAmount;
                row.Cells["cs_entrustdirection"].Value = dataItem.EntrustDirection;
                row.Cells["cs_entrustedamount"].Value = dataItem.EntrustedAmount;
                row.Cells["cs_pricetype"].Value = dataItem.PriceType;
                row.Cells["cs_entrustprice"].Value = dataItem.EntrustPrice;
                row.Cells["cs_thisentrustamout"].Value = dataItem.ThisEntrustAmount;
                row.Cells["cs_dealamount"].Value = dataItem.DealAmount;
                row.Cells["cs_targetamount"].Value = dataItem.TargetAmount;
                row.Cells["cs_waitamount"].Value = dataItem.WaitAmount;
                row.Cells["cs_suspensionflag"].Value = dataItem.SuspensionFlag;
                row.Cells["cs_targetcopies"].Value = dataItem.TargetCopies;
                row.Cells["cs_commandcopies"].Value = dataItem.CommandCopies;
                row.Cells["cs_limitupprice"].Value = dataItem.LimitUpPrice;
                row.Cells["cs_limitdownprice"].Value = dataItem.LimitDownPrice;
                row.Cells["cs_limitupdown"].Value = dataItem.LimitUpOrDown;

                SetSelectionRowBackground(this.dataGridViewCmdSecurity, rowIndex, isSelected);
            }
        }

        private List<CommandSecurityItem> GetSelectionCommandSecurityItems()
        {
            List<CommandSecurityItem> selectionItems = new List<CommandSecurityItem>();
            var dgv = dataGridViewCmdTrading;
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    CommandSecurityItem item = new CommandSecurityItem();
                    item.CommandId = (int)row.Cells["tc_commandno"].Value;
                    item.SecuCode = (string)row.Cells["cs_secucode"].Value;
                    item.SecuName = (string)row.Cells["cs_secuname"].Value;
                    item.CommandId = (int)row.Cells["cs_commandno"].Value;
                    item.FundCode = (string)row.Cells["cs_fundcode"].Value;
                    item.PortfolioName = (string)row.Cells["cs_portfolioname"].Value;
                    item.CommandPrice = (double)row.Cells["cs_commandprice"].Value;
                    item.CommandAmount = (int)row.Cells["cs_commandamount"].Value;
                    item.EntrustDirection = (string)row.Cells["cs_entrustdirection"].Value;
                    item.EntrustedAmount = (int)row.Cells["cs_entrustedamount"].Value;
                    item.PriceType = (string)row.Cells["cs_pricetype"].Value;
                    item.EntrustPrice = (double)row.Cells["cs_entrustprice"].Value;
                    item.ThisEntrustAmount = (int)row.Cells["cs_thisentrustamout"].Value;
                    item.DealAmount = (int)row.Cells["cs_dealamount"].Value;
                    item.TargetAmount = (int)row.Cells["cs_targetamount"].Value;
                    item.WaitAmount = (int)row.Cells["cs_waitamount"].Value;
                    item.SuspensionFlag = (string)row.Cells["cs_suspensionflag"].Value;
                    item.TargetCopies = (int)row.Cells["cs_targetcopies"].Value;
                    item.CommandCopies = (int)row.Cells["cs_commandcopies"].Value;
                    item.LimitUpPrice = (double)row.Cells["cs_limitupprice"].Value;
                    item.LimitDownPrice = (double)row.Cells["cs_limitdownprice"].Value;
                    item.LimitUpOrDown = (string)row.Cells["cs_limitupdown"].Value;

                    selectionItems.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return selectionItems;
        }
        #endregion

        #region 成交流水
        private List<DealFlowItem> GenerateDealFlowData()
        {
            List<DealFlowItem> dfItems = new List<DealFlowItem>();

            DealFlowItem item = new DealFlowItem
            {
                CommandNo = 112,
                SecuCode = "600369",
                SecuName = "从啦",
                FundNo = "A125",
                FundName = "Fund 1",
                PortfolioCode = "P1112",
                PortfolioName = "Portfolio 1",
                PriceType = "dddd",
                EntrustPrice = 12.34,
                EntrustDirection = "Buy",
                DealPrice = 12.20,
                DealAmount = 1200,
                DealMoney = 120045.02,
                DealTime = "124545",
                ShareHolderCode = "S123456",
                DeclareNo = "1234",
                DeclareSeat = "A12345",
                EntrustBatchNo = "45678",
                InstanceId = "45789",
                InstanceNo = "Sdfads",
                EntrustNo = "45789",
                DealNo = "789"
            };

            dfItems.Add(item);
            return dfItems;
        }

        private void FillDealFlow(List<DealFlowItem> dfItems)
        {
            foreach (var dataItem in dfItems)
            {
                int rowIndex = this.dataGridViewDealFlow.Rows.Add();

                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_commandno"].Value = dataItem.CommandNo;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_securitycode"].Value = dataItem.SecuCode;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_securityname"].Value = dataItem.SecuName;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_fundno"].Value = dataItem.FundNo;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_fundname"].Value = dataItem.FundName;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_portfoliocode"].Value = dataItem.PortfolioCode;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_portfolioname"].Value = dataItem.PortfolioName;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_pricetype"].Value = dataItem.PriceType;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_entrustprice"].Value = dataItem.EntrustPrice;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_entrustdirection"].Value = dataItem.EntrustDirection;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_dealprice"].Value = dataItem.DealPrice;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_dealamount"].Value = dataItem.DealAmount;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_dealmoney"].Value = dataItem.DealMoney;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_dealtime"].Value = dataItem.DealTime;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_stockholdercode"].Value = dataItem.ShareHolderCode;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_declareno"].Value = dataItem.DeclareNo;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_declareseat"].Value = dataItem.DeclareSeat;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_entrustbatchno"].Value = dataItem.EntrustBatchNo;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_instanceid"].Value = dataItem.InstanceId;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_instanceno"].Value = dataItem.InstanceNo;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_entrustno"].Value = dataItem.EntrustNo;
                this.dataGridViewDealFlow.Rows[rowIndex].Cells["df_dealno"].Value = dataItem.DealNo;
            }
        }
        #endregion

        #region 委托流水

        private Model.Data.RawDataSet GenerateEntrustFlowData(out Dictionary<string, string> colDataMap)
        {
            Model.Data.RawDataSet dataSet = new Model.Data.RawDataSet 
            {
                FunctionCode = FunctionCode.QueryEntrustInstance,
                Rows = new List<Model.Data.RawDataRow>()
            };

            const string CHAR = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random r = new Random((int)DateTime.Now.Ticks);
            colDataMap = new Dictionary<string, string>();
            for (int i = 0; i < 10; i++)
            {
                Model.Data.RawDataRow dataRow = new Model.Data.RawDataRow();
                dataRow.Columns = new Dictionary<string, Model.Data.DataValue>();

                foreach (HSGridColumn column in this.dataGridViewEntrustFlow.GridColumns)
                {
                    if (!colDataMap.ContainsKey(column.Name))
                    {
                        colDataMap.Add(column.Name, column.Name);
                    }

                    Model.Data.DataValue dataValue = new Model.Data.DataValue();
                    dataValue.Type = column.ValueType;

                    switch (column.ValueType)
                    {
                        case Model.Data.DataValueType.Int:
                            {
                                dataValue.Value = r.Next(1, 100000);
                            }
                            break;
                        case Model.Data.DataValueType.Float:
                            {
                                dataValue.Value = 100000 * r.NextDouble();
                            }
                            break;
                        case Model.Data.DataValueType.Char:
                        case Model.Data.DataValueType.String:
                            {
                                StringBuilder str = new StringBuilder();
                                for (int j = 0; j < 15; j++)
                                {
                                    int temp = r.Next(0, 1000000);
                                    int index = temp % 36;
                                    str.Append(CHAR[index]);
                                }

                                dataValue.Value = str.ToString();
                            }
                            break;
                    }

                    dataRow.Columns[column.Name] = dataValue;
                }

                dataSet.Rows.Add(dataRow);
            }

            return dataSet;
        }

        private void FillEntrustFlow(Model.Data.RawDataSet dataSet, Dictionary<string, string> colDataMap)
        {
            //this.dataGridViewEntrustFlow.FillData(dataSet, colDataMap);
        }
        #endregion

        #region 委托

        private List<EntrustItem> GetDefaultEntrustData()
        {
            List<TradingCommandItem> selectionCommandItems = GetSelectionCommandTradingItems();

            List<EntrustItem> eiItems = new List<EntrustItem>();
            foreach (TradingCommandItem tcItem in selectionCommandItems)
            {
                EntrustItem item = new EntrustItem
                {
                    Selection = false,
                    CommandNo = tcItem.CommandId,
                    Copies = 0
                };

                eiItems.Add(item);
            }

            return eiItems;
        }

        private void FillEntrustGrid(List<EntrustItem> eiItems)
        {
            foreach (var dataItem in eiItems)
            {
                int rowIndex = this.dataGridViewBuySell.Rows.Add();
                DataGridViewRow row = this.dataGridViewBuySell.Rows[rowIndex];
                bool isSelected = dataItem.Selection;
                row.Cells["bs_selection"].Value = isSelected;
                row.Cells["bs_commandno"].Value = dataItem.CommandNo;
                row.Cells["bs_copies"].Value = 0;

                Image plusImg = Image.FromFile(@"img\plus.png");
                Bitmap plusBt = new Bitmap(plusImg, new Size(20, 20));
                row.Cells["bs_add"].Value = plusBt;

                Image minusImg = Image.FromFile(@"img\minus.png");
                Bitmap minusBt = new Bitmap(minusImg, new Size(20, 20));
                row.Cells["bs_minus"].Value = minusBt;

                SetSelectionRowBackground(dataGridViewBuySell, rowIndex, isSelected);
            }

            SortBuySellGrid();
        }

        private void RemoveEntrustGrid(int commandNo)
        {
            //this.dataGridViewBuySell.Rows.RemoveAt(rowIndex);
            DataGridView dgv = this.dataGridViewBuySell;

            for (int i = dgv.Rows.Count - 1; i >= 0; i--)
            {
                int bsCommandNo = (int)dgv.Rows[i].Cells["bs_commandno"].Value;
                if (bsCommandNo == commandNo)
                {
                    dgv.Rows.RemoveAt(i);
                }
            }
        }

        private List<EntrustItem> GetSelectionEntrustItems()
        {
            List<EntrustItem> eiItems = new List<EntrustItem>();
            var dgv = this.dataGridViewBuySell;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                EntrustItem item = new EntrustItem();
                item.Selection = true;
                item.CommandNo = (int)row.Cells["bs_commandno"].Value;
                item.Copies = (int)row.Cells["bs_copies"].Value;

                eiItems.Add(item);
            }

            return eiItems;
        }

        private void SortBuySellGrid()
        {
            this.dataGridViewBuySell.Sort(dataGridViewBuySell.Columns["bs_commandno"], ListSortDirection.Ascending);
        }
        #endregion

        #region set grid readonly

        private void SetDataGridVieweReadOnly(DataGridView dataGridView, List<DataGridViewColumn> editColumns)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (!IsEditColumn(editColumns, column.Name))
                {
                    column.ReadOnly = true;
                }
            }
        }

        private bool IsEditColumn(List<DataGridViewColumn> editColumns, string columnName)
        {
            foreach (var column in editColumns)
            {
                if (column.Name == columnName)
                    return true;
            }

            return false;
        }
        #endregion

        #region entrust copies input

        private void DataGridViewBuySell_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv != null && dgv.Columns[e.ColumnIndex].Name == "bs_copies")
            {
                if (e.DesiredType == typeof(int))
                {
                    e.ParsingApplied = true;
                }
                else
                {
                    Console.WriteLine("请输入数字！");
                }
            }
        }

        private void DataGridViewBuySell_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv == null)
                return;

            if (dgv.CurrentCell.ColumnIndex == dgv.Columns["bs_copies"].Index)
            {
                DataGridViewTextBoxEditingControl dgvTxt = e.Control as DataGridViewTextBoxEditingControl;
                dgvTxt.SelectAll();
                dgvTxt.KeyPress += Cells_KeyPress;
            }
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressEx(e, (DataGridViewTextBoxEditingControl)sender);
        }

        private void KeyPressEx(KeyPressEventArgs e, DataGridViewTextBoxEditingControl dgvTxt)
        {
            if (char.IsNumber(e.KeyChar))
            {
                int preValue = 0;
                if (!string.IsNullOrEmpty(dgvTxt.Text))
                {
                    preValue = int.Parse(dgvTxt.Text);
                }
                int curValue = int.Parse(e.KeyChar.ToString());
                if (preValue * 10 + curValue <= MAX_ENTRUST_AMOUNT)
                {
                    //让操作失效
                    e.Handled = false;
                }
                else
                {
                    //input beyond the range
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                //让操作失效
                e.Handled = false;
            }
            else
            {
                Console.WriteLine("输入无效值！");
                //取消事件响应
                e.Handled = true;
            }
        }
        #endregion

        #region 增减委托份数

        private void DataGridViewBuySell_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv == null || e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            int copiesIndex = dgv.Columns["bs_copies"].Index;
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            //const int AddIndex = dgv.Columns["bs_add"].Index;
            switch (dgv.Columns[e.ColumnIndex].Name)
            {
                case "bs_add":
                    {
                        int oldValue = int.Parse(row.Cells["bs_copies"].Value.ToString());
                        if (oldValue < MAX_ENTRUST_AMOUNT)
                        {
                            row.Cells["bs_copies"].Value = oldValue + 1;
                        }
                        else
                        {
                            //invalid input
                        }
                    }
                    break;
                case "bs_minus":
                    {
                        int oldValue = int.Parse(row.Cells["bs_copies"].Value.ToString());
                        if (oldValue > 0)
                        {
                            row.Cells["bs_copies"].Value = oldValue - 1;
                        }
                        else
                        {
                            //invalid input
                        }
                    }
                    break;
            }
        }

        #endregion

        #region

        public void DataGridViewCmdTrading_Select(UpdateDirection direction, Model.Data.RawDataRow dataRow)
        {
            string colKey = "instance_no";
            if (dataRow == null || dataRow.Columns == null || dataRow.Columns.ContainsKey(colKey))
                return;

            if (direction == UpdateDirection.Add)
            {
                Dictionary<string, string> colDataMap = new Dictionary<string, string>();
                Model.Data.RawDataSet eDataSet = new Model.Data.RawDataSet();
                eDataSet.Rows = new List<Model.Data.RawDataRow>();
                Model.Data.RawDataRow eRow = new Model.Data.RawDataRow();
                eRow.Columns = new Dictionary<string, Model.Data.DataValue>();

                foreach (var column in this.dataGridViewBuySell.GridColumns)
                {
                    if (column.ColumnType == HSGridColumnType.None)
                        continue;

                    Model.Data.DataValue dataValue = new Model.Data.DataValue();
                    dataValue.Type = column.ValueType;
                    if (column.Name == "bs_commandno")
                    {
                        dataValue.Value = dataRow.Columns["tc_commandno"].Value;
                    }
                    else if (column.ColumnType == HSGridColumnType.Image)
                    {
                        dataValue.Value = column.DefaultValue;
                    }
                    else
                    {
                        dataValue.Value = 0;
                    }

                    colDataMap.Add(column.Name, column.Name);
                    eRow.Columns.Add(column.Name, dataValue);
                }

                eDataSet.Rows.Add(eRow);
                //this.dataGridViewBuySell.FillData(eDataSet, colDataMap);
            }
            else if (direction == UpdateDirection.Remove)
            {
                var targetValue = dataRow.Columns["tc_commandno"];
                this.dataGridViewBuySell.DeleteRow("bs_commandno", targetValue);
            }
        }

        #endregion

        #region button click

        private void ButtonEntrusting_Click(object sender, EventArgs e)
        {
            ComboOptionItem spotBuyItem = (ComboOptionItem)comboBoxSpotBuy.SelectedItem;
            ComboOptionItem spotSellItem = (ComboOptionItem)comboBoxSpotSell.SelectedItem;
            ComboOptionItem futureBuyItem = (ComboOptionItem)comboBoxFutureBuy.SelectedItem;
            ComboOptionItem futureSellItem = (ComboOptionItem)comboBoxFutureSell.SelectedItem;

            Console.WriteLine(spotBuyItem);

            //submit the entrust portfolio
            MessageBoxButtons msgButton = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("确定委托选中实例吗？", "委托", msgButton, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                //提交委托实例
            }
            else
            { 
                //取消
            }
        }

        private void ButtonCalc_Click(object sender, EventArgs e)
        {
            //计算委托金额
        }

        private void ButtonUnSelectAll_Click(object sender, System.EventArgs e)
        {
            this.dataGridViewEntrustFlow.SelectAll(false);
        }

        private void ButtonSelectAll_Click(object sender, System.EventArgs e)
        {
            this.dataGridViewEntrustFlow.SelectAll(true);
        }
        #endregion

        #region common operation of controls

        private void SetSelectionRowBackground(DataGridView dgv, int rowIndex, bool isSelected)
        {
            if (isSelected)
            {
                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Blue;
            }
            else
            {
                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        #endregion

        #region TabControlMain_SelectedIndexChanged

        private void TabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            string selectTabName = this.tabControlMain.SelectedTab.Name;
            if (selectTabName == "tabParentEntrustFlow")
            {
                this.tlPanelParentEntrustFlow.Controls.Add(this.dataGridViewEntrustFlow, 0, 1);
                this.tlPanelParentEntrustFlow.Controls.Add(this.panelParentEntrustFlow, 0, 2);
            }
            else if (selectTabName == "tabParentDealFlow")
            {
                this.tlPanelParentDealFlow.Controls.Add(this.dataGridViewDealFlow, 0, 1);
            }
            else if (selectTabName == "tabParentCmdTrading")
            {
                //AddDialFlowGridInDetail();
                //this.tlPanelParentCommand.Controls.Add(this.dataGridViewCmdTrading, 0, 1);
                SwitchDetailTabPage(this.tabControlCmdDetail.SelectedTab.Name);
            }
        }

        #endregion

        #region TabControlCmdDetail_SelectedIndexChanged

        private void TabControlCmdDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            SwitchDetailTabPage(this.tabControlCmdDetail.SelectedTab.Name);
            //Console.WriteLine(this.tabControlDetailTrading.SelectedTab.Name);
            //throw new NotImplementedException();
            //if (this.tabControlCmdDetail.SelectedTab.Name == "tabPageDialFlow")
            //{
            //    AddDialFlowGridInDetail();
            //}
        }

        private void SwitchDetailTabPage(string selectTabName)
        {
            if (selectTabName == "tabChildCmdSecurity")
            {
                this.tlPanelChildCmdSecurity.Controls.Add(this.dataGridViewCmdSecurity, 0, 0);
            }
            else if (selectTabName == "tabChildEntrustFlow")
            {
                this.tlPanelChildEntrustFlow.Controls.Add(this.dataGridViewEntrustFlow, 0, 0);
                this.tlPanelChildEntrustFlow.Controls.Add(this.panelParentEntrustFlow, 0, 1);
            }
            else if (selectTabName == "tabChildDealFlow")
            {
                this.tlPanelChildDealFlow.Controls.Add(this.dataGridViewDealFlow, 0, 0);
            }
        }

        private void AddDialFlowGridInDetail()
        {
            //this..Controls.Remove(this.dataGridViewDealFlow);
            //this.tabPageDialFlow.Controls.Clear();
            //this.tabPageDialFlow.Controls.Add(this.dataGridViewDealFlow);
        }
        #endregion

        #region toolstrip click
        
        private void MainRefresh_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ToolStripButton_CmdRefresh_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ToolStripButton_CmdCancelRedo_Click(object sender, System.EventArgs e)
        {
            CancelRedoForm2 cancelRedoForm = new CancelRedoForm2(this._gridConfig);
            cancelRedoForm.Owner = this;
            cancelRedoForm.ShowDialog();
            if (cancelRedoForm.DialogResult == System.Windows.Forms.DialogResult.OK)
            { 
            
            }
            
            throw new System.NotImplementedException();
        }

        private void ToolStripButton_CmdUndo_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region TextBox

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        #endregion
    }
}
