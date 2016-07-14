using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Binding
{
    public class UFXDataAttribute : Attribute
    {
        private string _name;
        private DataValueType _valueType;

        public UFXDataAttribute(string name, DataValueType valueType)
        {
            _name = name;
            _valueType = valueType;
        }

        public UFXDataAttribute(string name)
            :this(name, DataValueType.String)
        { 
        }

        public string Name
        {
            get { return _name; }
        }

        public DataValueType ValueType
        {
            get { return _valueType; }
        }
    }
}
