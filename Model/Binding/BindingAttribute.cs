using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Binding
{
    public class BindingAttribute : Attribute
    {
        private string _columnName;

        public BindingAttribute(string columnName)
        {
            this._columnName = columnName;
        }

        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }
    }
}
