using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Entity
{
    public class TSGridViewRowPropertyDescriptor : PropertyDescriptor
    {
        private string _name;
        private Type _type;
        private int _index;

        public TSGridViewRowPropertyDescriptor(string name, Type type, int index)
            : base(name, null)
        {
            _name = name;
            _type = type;
            _index = index;
        }

        public override string DisplayName
        {
            get
            {
                return _name;
            }
        }

        #region PropertyDescriptor
        
        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get 
            { 
                return typeof(TSGridViewRow); 
            }
        }

        public override object GetValue(object component)
        {
            try
            {
                return ((TSGridViewRow)component).GetColumn(_index);
            }
            catch (Exception e)
            {
                //TODO: log
                throw;
            }

            return null;
        }

        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return _type;
            }
        }

        public override void ResetValue(object component)
        {
            
        }

        public override void SetValue(object component, object value)
        {
            try
            {
                ((TSGridViewRow)component).SetColumnValue(_index, value);
            }
            catch (Exception e)
            {
                //TODO: log
                throw;
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        #endregion
    }
}
