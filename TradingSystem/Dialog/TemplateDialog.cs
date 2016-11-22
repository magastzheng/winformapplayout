using BLL.Permission;
using BLL.SecurityInfo;
using Config;
using Forms;
using Model.config;
using Model.EnumType;
using Model.Permission;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TradingSystem.Dialog
{
    public partial class TemplateDialog : Forms.BaseDialog
    {
        //const string MsgPerm = "用户[{0}]权限设置为可编辑但未设为可浏览，可编辑用户必须同时设为可浏览！";
        //const string MsgInfoCaption = "提示";

        private const string msgTempPermSetting = "temppermsetting";

        private BenchmarkBLL _benchmarkBLL = new BenchmarkBLL();
        private UserBLL _userBLL = new UserBLL();
        private PermissionManager _permissionManager = new PermissionManager();
        
        private List<User> _users = null;
        private StockTemplate _oldTemplate = null;

        public TemplateDialog()
        {
            InitializeComponent();

            this.LoadControl += new FormLoadHandler(Form_LoadControl);
            this.LoadData += new FormLoadHandler(Form_LoadData);

            cbEditUser.DisplayMember = "Name";
            cbViewUser.DisplayMember = "Name";
        }

        #region load control

        private bool Form_LoadControl(object sender, object data)
        {
            var weightType = ConfigManager.Instance.GetComboConfig().GetComboOption("weighttype");
            ComboBoxUtil.SetComboBox(this.cbWeightType, weightType);

            var replaceType = ConfigManager.Instance.GetComboConfig().GetComboOption("replacetype");
            ComboBoxUtil.SetComboBox(this.cbReplaceType, replaceType);

            LoadBenchmark();
            LoadUser();

            return true;
        }

        private bool LoadBenchmark()
        {
            var benchmarks = _benchmarkBLL.GetAll();
            ComboOption cbOption = new ComboOption
            {
                Items = new List<ComboOptionItem>()
            };
            foreach (var benchmark in benchmarks)
            {
                ComboOptionItem item = new ComboOptionItem
                {
                    Id = benchmark.BenchmarkId,
                    Code = benchmark.BenchmarkId,
                    Name = benchmark.BenchmarkName
                };

                cbOption.Items.Add(item);
            }

            cbOption.Selected = benchmarks[0].BenchmarkId;
            ComboBoxUtil.SetComboBox(this.cbBenchmark, cbOption);

            return true;
        }

        private bool LoadUser()
        {
            if (_users == null || _users.Count == 0)
            {
                _users = _userBLL.GetAll();
            }

            var loginUserId = LoginManager.Instance.GetUserId();
            for(int i = 0, count = _users.Count; i < count; i++)
            {
                var user = _users[i];
                cbEditUser.Items.Add(user);
                if (user.Id == loginUserId)
                {
                    cbEditUser.SetItemChecked(i, true);
                }

                cbViewUser.Items.Add(user);
                if (user.Id == loginUserId)
                {
                    cbViewUser.SetItemChecked(i, true);
                }
            }

            return true;
        }

        #endregion

        private bool Form_LoadData(object sender, object data)
        {
            if (sender == null || data == null)
                return false;
            if (!(data is StockTemplate))
                return false;

            StockTemplate stockTemplate = data as StockTemplate;
            if (stockTemplate != null)
            {
                _oldTemplate = stockTemplate;
                FillData(_oldTemplate);
                SetUserPermission(_oldTemplate);
            }

            return true;
        }

        private void FillData(StockTemplate stockTemplate)
        {
            this.tbTemplateNo.Text = stockTemplate.TemplateId.ToString();
            this.tbTemplateNo.Enabled = false;

            this.tbTemplateName.Text = stockTemplate.TemplateName;

            this.tbFutureCopies.Text = stockTemplate.FutureCopies.ToString();
            this.tbMarketCapOpt.Text = stockTemplate.MarketCapOpt.ToString();

            ComboBoxUtil.SetComboBoxSelect(this.cbBenchmark, stockTemplate.Benchmark);
            ComboBoxUtil.SetComboBoxSelect(this.cbWeightType, stockTemplate.EWeightType.ToString());
            ComboBoxUtil.SetComboBoxSelect(this.cbReplaceType, stockTemplate.EReplaceType.ToString());
        }

        private void SetUserPermission(StockTemplate stockTemplate)
        {
            var urPerms = stockTemplate.Permissions;
            if (urPerms == null)
                return;

            for(int i = 0, count = this.cbEditUser.Items.Count; i < count; i++)
            {
                var user = (User)this.cbEditUser.Items[i];
                var findItem = urPerms.Find(p => p.Token == user.Id && p.TokenType == TokenType.User);
                if (findItem != null && _permissionManager.HasPermission(user.Id, stockTemplate.TemplateId, ResourceType.SpotTemplate, PermissionMask.Edit))
                {
                    this.cbEditUser.SetItemChecked(i, true);
                }
                else
                {
                    this.cbEditUser.SetItemChecked(i, false);
                }
            }

            for (int i = 0, count = this.cbViewUser.Items.Count; i < count; i++)
            {
                var user = (User)this.cbViewUser.Items[i];
                var findItem = urPerms.Find(p => p.Token == user.Id && p.TokenType == TokenType.User);
                if (findItem != null && _permissionManager.HasPermission(user.Id, stockTemplate.TemplateId, ResourceType.SpotTemplate, PermissionMask.View))
                {
                    this.cbViewUser.SetItemChecked(i, true);
                }
                else
                {
                    this.cbViewUser.SetItemChecked(i, false);
                }
            }
        }

        private StockTemplate GetTemplate()
        {
            StockTemplate stockTemplate = new StockTemplate
            {
                EStatus = TemplateStatus.Normal,
                Permissions = new List<TokenResourcePermission>(),
                CanEditUsers = new List<User>(),
                CanViewUsers = new List<User>(),
            };

            if (!string.IsNullOrEmpty(this.tbTemplateNo.Text))
            {
                int tempNo = -1;
                if (int.TryParse(this.tbTemplateNo.Text, out tempNo))
                {
                    stockTemplate.TemplateId = tempNo;
                }
            }

            stockTemplate.TemplateName = this.tbTemplateName.Text;

            int temp = 0;
            if(int.TryParse(this.tbFutureCopies.Text, out temp))
            {
                stockTemplate.FutureCopies = temp;
            }
            
            double dTemp = 0.0f;
            if(double.TryParse(this.tbMarketCapOpt.Text, out dTemp))
            {
                stockTemplate.MarketCapOpt = dTemp;
            }

            if (this.cbWeightType.SelectedItem is ComboOptionItem)
            {
                ComboOptionItem item = this.cbWeightType.SelectedItem as ComboOptionItem;
                int type = 0;
                if (int.TryParse(item.Id, out type))
                {
                    stockTemplate.EWeightType = (WeightType)type;
                }
            }

            if (this.cbReplaceType.SelectedItem is ComboOptionItem)
            {
                ComboOptionItem item = this.cbReplaceType.SelectedItem as ComboOptionItem;
                int type = 0;
                if (int.TryParse(item.Id, out type))
                {
                    stockTemplate.EReplaceType = (ReplaceType)type;
                }
            }

            if (this.cbBenchmark.SelectedItem is ComboOptionItem)
            {
                ComboOptionItem item = this.cbBenchmark.SelectedItem as ComboOptionItem;
                stockTemplate.Benchmark = item.Id;
            }

            if (_oldTemplate != null)
            {
                stockTemplate.DCreatedDate = _oldTemplate.DCreatedDate;
                stockTemplate.DModifiedDate = DateTime.Now;
            }

            stockTemplate.CreatedUserId = LoginManager.Instance.GetUserId();
            UpdatePermission(ref stockTemplate);
            
            return stockTemplate;
        }

        private void UpdatePermission(ref StockTemplate stockTemplate)
        {
            if (this.cbEditUser.CheckedItems != null && this.cbEditUser.CheckedItems.Count > 0)
            {
                foreach (var item in this.cbEditUser.CheckedItems)
                {
                    var user = item as User;
                    stockTemplate.CanEditUsers.Add(user);
                }
            }

            if (this.cbViewUser.CheckedItems != null && this.cbViewUser.CheckedItems.Count > 0)
            {
                foreach (var item in this.cbViewUser.CheckedItems)
                {
                    var user = item as User;
                    stockTemplate.CanViewUsers.Add(user);
                }
            }

            //处理权限改变的情况，添加新权限，或者去掉某一项权限
            var permUsers = stockTemplate.CanEditUsers.Union(stockTemplate.CanViewUsers);
            var urPermission = new List<TokenResourcePermission>();
            foreach (var user in permUsers)
            {
                int oldPerm = 0;
                int newPerm = 0;
                TokenResourcePermission urPerm = null;
                if (_oldTemplate != null && _oldTemplate.Permissions != null)
                {
                    urPerm = _oldTemplate.Permissions.Find(p => p.Token == user.Id && p.TokenType == TokenType.User);
                    if (urPerm != null)
                    {
                        oldPerm = urPerm.Permission;
                        newPerm = oldPerm;
                    }
                }

                List<PermissionMask> addRights = new List<PermissionMask>();
                List<PermissionMask> removeRights = new List<PermissionMask>();
                if (stockTemplate.CanEditUsers.Contains(user))
                {
                    addRights.Add(PermissionMask.Edit);
                }
                else
                {
                    removeRights.Add(PermissionMask.Edit);
                }

                if (stockTemplate.CanViewUsers.Contains(user))
                {
                    addRights.Add(PermissionMask.View);
                }
                else
                {
                    removeRights.Add(PermissionMask.View);
                }

                newPerm = _permissionManager.AddPermission(newPerm, addRights);
                newPerm = _permissionManager.RemovePermission(newPerm, removeRights);

                TokenResourcePermission nurPerm = new TokenResourcePermission
                {
                    Token = user.Id,
                    TokenType = TokenType.User,
                    ResourceId = stockTemplate.TemplateId,
                    ResourceType = ResourceType.SpotTemplate,
                    Permission = newPerm,
                };

                if (urPerm != null)
                {
                    nurPerm.Id = urPerm.Id;
                }

                urPermission.Add(nurPerm);
            }

            //处理两种权限都被去掉的情况
            if (_oldTemplate != null && _oldTemplate.Permissions != null)
            {
                foreach (var oldPerm in _oldTemplate.Permissions)
                {
                    var findPerm = urPermission.Find(p => p.Token == oldPerm.Token
                        && p.TokenType == oldPerm.TokenType
                        && p.ResourceId == oldPerm.ResourceId
                        && p.ResourceType == oldPerm.ResourceType);

                    if (findPerm == null)
                    {
                        List<PermissionMask> rights = new List<PermissionMask>() { PermissionMask.Edit, PermissionMask.View };
                        int newPerm = _permissionManager.RemovePermission(oldPerm.Permission, rights);
                        TokenResourcePermission nurPerm = new TokenResourcePermission
                        {
                            Token = oldPerm.Token,
                            TokenType = TokenType.User,
                            ResourceId = oldPerm.ResourceId,
                            ResourceType = ResourceType.SpotTemplate,
                            Permission = newPerm,
                        };

                        urPermission.Add(nurPerm);
                    }
                }
            }

            stockTemplate.Permissions.AddRange(urPermission);
        }

        private bool CheckInputValue(StockTemplate stockTemplate)
        {
            if (string.IsNullOrEmpty(stockTemplate.TemplateName))
            {
                return false;
            }

            if (stockTemplate.FutureCopies < 1)
            {
                return false;
            }

            if (stockTemplate.MarketCapOpt < 1)
            {
                return false;
            }

            if (string.IsNullOrEmpty(stockTemplate.Benchmark))
            {
                return false;
            }

            foreach (var user in stockTemplate.CanEditUsers)
            {
                var findItem = stockTemplate.CanViewUsers.Find(p => p.Id == user.Id);
                if (findItem == null)
                {
                    string format = ConfigManager.Instance.GetLabelConfig().GetLabelText(msgTempPermSetting);
                    string msg = string.Format(format, findItem.Name);
                    MessageDialog.Info(this, msg);

                    return false;
                }
            }

            return true;
        }

        #region Button click event handler
        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            StockTemplate stockTemplate = GetTemplate();
            if (CheckInputValue(stockTemplate))
            {
                OnSave(this, stockTemplate);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.No;
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        #endregion
    }
}
