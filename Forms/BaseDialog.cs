
namespace Forms
{
    public partial class BaseDialog : Forms.BaseForm
    {
        public BaseDialog()
        {
            InitializeComponent();
        }

        public virtual object GetData()
        {
            return null;
        }
    }
}
