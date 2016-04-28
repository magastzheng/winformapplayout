using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public class FormManager
    {
        public static BaseForm LoadForm(Form mainForm, Type formType, string json)
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
                childForm = (BaseForm)Activator.CreateInstance(formType);
                childForm.MdiParent = mainForm;
                childForm.Show();
            }

            //窗体激活的时候，传递对应的参数信息
            ILoadFormActived formActived = childForm as ILoadFormActived;
            if (formActived != null)
            {
                formActived.OnLoadFormActived(json);
            }

            childForm.BringToFront();
            childForm.Activate();

            return childForm;
        }

        public static BaseForm LoadForm(Form mainForm, Type formType, object[] constructorArgs, string json)
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

                childForm.MdiParent = mainForm;
                childForm.Show();
            }

            //窗体激活的时候，传递对应的参数信息
            ILoadFormActived formActived = childForm as ILoadFormActived;
            if (formActived != null)
            {
                formActived.OnLoadFormActived(json);
            }

            childForm.BringToFront();
            childForm.Activate();

            return childForm;
        }
    }
}
