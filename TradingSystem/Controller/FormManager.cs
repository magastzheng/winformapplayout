using Config;
using Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingSystem.View;

namespace TradingSystem.Controller
{
    public class FormManager
    {
        private readonly static FormManager _instance = new FormManager();

        private static GridConfig _gridConfig;

        private FormManager()
        {
            Init();
        }

        static FormManager()
        { 
        
        }

        private void Init()
        {
            _gridConfig = ConfigManager.Instance.GetGridConfig();
        }

        private Dictionary<string, Forms.BaseForm> _childFormMap = new Dictionary<string, Forms.BaseForm>();

        public static FormManager Instance { get { return _instance; } }

        public BaseForm ActiveForm(Form parentForm, Panel parentPanel, string formKey)
        {
            Forms.BaseForm form = null;
            Type formType = null;
            bool hasGrid = false;
            string json = string.Empty;
            if (_childFormMap.ContainsKey(formKey))
            {
                form = _childFormMap[formKey];
            }
            else
            {
                switch (formKey)
                {
                    case "open":
                        {
                            //form = new TradingForm();
                            //_childFormMap[key] = form;
                            formType = typeof(TradingForm);
                        }
                        break;
                    case "close":
                        break;
                    case "commandmanager":
                        break;
                    case "monitorunit":
                        {
                            formType = typeof(MonitorUnitForm);
                            hasGrid = true;
                        }
                        break;
                    case "portfoliomaintain":
                        {
                            formType = typeof(PortfolioForm);
                        }
                        break;
                    case "fundmanagement":
                        {
                            formType = typeof(GeneralForm);
                        }
                        break;
                    case "assetunitmanagement":
                        {
                            formType = typeof(GeneralForm);
                        }
                        break;
                    case "currenttemplate":
                        {
                            formType = typeof(StockTemplateForm);
                            hasGrid = true;
                            //StockTemplateDAO _dbdao = new StockTemplateDAO();
                            //var items = _dbdao.GetTemplate(-1);
                            //json = JsonUtil.SerializeObject(items);
                        }
                        break;
                    case "historytemplate":
                        break;
                    default:
                        break;
                }
            }

            if (formType != null && form == null)
            {
                if (hasGrid)
                {
                    form = LoadForm(parentForm, formType, new object[] { _gridConfig }, json);
                }
                else
                {
                    form = LoadForm(parentForm, formType, null, json);
                }
                _childFormMap[formKey] = form;
            }

            if (form != null)
            {
                ILoadFormActived formActived = form as ILoadFormActived;
                if (formActived != null)
                {
                    //TODO: add the step to load data and refresh the child form
                    formActived.OnLoadFormActived("");
                }

                form.MdiParent = parentForm;
                form.Parent = parentPanel;
                form.Dock = DockStyle.Fill;
                form.BringToFront();
                form.Show();
            }
            else
            {
                //default fomr
            }

            return form;
        }

        public BaseForm LoadForm(Form mainForm, Type formType, object[] constructorArgs, string json)
        {
            bool isFound = false;
            BaseForm childForm = null;
            foreach (Form form in mainForm.MdiChildren)
            {
                if (form is BaseForm)
                {
                    if (form.GetType() == formType)
                    {
                        isFound = true;
                        childForm = form as BaseForm;
                        break;
                    }
                }
            }

            if (!isFound)
            {
                if (constructorArgs != null && constructorArgs.Length > 0)
                {
                    childForm = (BaseForm)Activator.CreateInstance(formType, constructorArgs);
                }
                else
                {
                    childForm = (BaseForm)Activator.CreateInstance(formType);
                }

                //childForm.MdiParent = mainForm;
                //childForm.Show();
            }

            //窗体激活的时候，传递对应的参数信息
            //ILoadFormActived formActived = childForm as ILoadFormActived;
            //if (formActived != null)
            //{
            //    formActived.OnLoadFormActived(json);
            //}

            //childForm.BringToFront();
            //childForm.Activate();

            return childForm;
        }
    }
}
