using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Binding
{
    public class StandardCodeAttribute : Attribute
    {
        public string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public StandardCodeAttribute(string code)
        {
            _code = code;
        }
    }
}
