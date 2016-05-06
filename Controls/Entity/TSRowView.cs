using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Entity
{
    public class TSRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo
    {
        private TSGridViewData _owner;
        private int _index;
        private string _error;

        public TSRowView(TSGridViewData owner, int index)
        {
            _owner = owner;
            _index = index;
        }

        internal object GetColumn(int index)
        {
            return _owner.DataTable.Rows[_index].Columns[index].Value;
        }

        internal void SetColumnValue(int index, object value)
        {
            try
            {
                _owner.DataTable.Rows[_index].Columns[index].Value = value;
            }
            catch (Exception e)
            {
                _error = e.ToString();
            }
        }

        #region ICustomTypeDescriptor
        public AttributeCollection GetAttributes()
        {
            return AttributeCollection.Empty;
        }

        public string GetClassName()
        {
            return this.GetType().Name;
        }

        public string GetComponentName()
        {
            return null;
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return EventDescriptorCollection.Empty;
        }

        public EventDescriptorCollection GetEvents()
        {
            return EventDescriptorCollection.Empty;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            int col = _owner.DataTable.Rows[0].Columns.Count;
            //Type type = _owner.DataTable.Rows[0].Columns.GetType().GetElementType();
            //Type type = _owner.DataTable.GetType().GetElementType();
            //Type type = _owner.DataTable.Rows.GetType();
            Type type = _owner.DataTable.Rows[0].Columns[0].GetType();
            PropertyDescriptor[] prop = new PropertyDescriptor[col];
            for (int i = 0; i < col; i++)
            {
                prop[i] = new RowPropertyDescriptor(_owner.Columns[i].Text, type, i);
            }

            return new PropertyDescriptorCollection(prop);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(null);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return _owner;
        }
        #endregion

        #region IEditableObject
        public void BeginEdit()
        {
            //TODO:
        }

        public void CancelEdit()
        {
            //TODO:
        }

        public void EndEdit()
        {
            //TODO:
        }
        #endregion


        #region IDataErrorInfo
        public string Error
        {
            get
            {
                //TODO: 
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                //TODO:
                return null;
            }
        }
        #endregion
    }
}
