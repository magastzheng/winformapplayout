using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Controls.Entity
{
    public class TSGridViewData : IBindingList
    {
        private Model.Data.DataTable _dataTable = null;
        private List<HSGridColumn> _columns = null;
        private TSGridViewRow[] _rows;

        public TSGridViewData(Model.Data.DataTable dataTable, List<HSGridColumn> columns)
        {
            _dataTable = dataTable;
            _columns = columns;

            _rows = new TSGridViewRow[_dataTable.Rows.Count];
            for (int i = 0; i < _rows.Length; i++)
            {
                _rows[i] = new TSGridViewRow(this, i);
            }
        }

        public Model.Data.DataTable DataTable { get { return _dataTable; } }
        public List<HSGridColumn> Columns { get { return _columns; } }

        #region implement IBindingList interface
        public void AddIndex(PropertyDescriptor property)
        {
            //TODO: add GridViewData.AddIndex
        }

        public object AddNew()
        {
            //TODO: add GridViewData.AddNew
            return null;
        }

        public bool AllowEdit
        {
            get 
            { 
                return true; 
            }
        }

        public bool AllowNew
        {
            get 
            {
                return false;
            }
        }

        public bool AllowRemove
        {
            get { return false; }
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            //TODO:
        }

        public int Find(PropertyDescriptor property, object key)
        {
            //TODO:
            return 0;
        }

        public bool IsSorted
        {
            get 
            {
                //TODO:
                return false; 
            }
        }

        public event ListChangedEventHandler ListChanged;

        private void OnListChanged(ListChangedEventArgs e)
        {
            if (ListChanged != null)
            {
                ListChanged(this, e);
            }
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            //TODO:
        }

        public void RemoveSort()
        {
            //TODO:
        }

        public ListSortDirection SortDirection
        {
            get 
            {
                //TODO: Add 
                return new ListSortDirection();
            }
        }

        public PropertyDescriptor SortProperty
        {
            get 
            {
                //TODO:
                return null;
            }
        }

        public bool SupportsChangeNotification
        {
            get 
            {
                return true;
            }
        }

        public bool SupportsSearching
        {
            get 
            {
                return false;
            }
        }

        public bool SupportsSorting
        {
            get 
            {
                return false;
            }
        }

        public int Add(object value)
        {
            return 0;
        }

        public void Clear()
        {
            
        }

        public bool Contains(object value)
        {
            return false;
        }

        public int IndexOf(object value)
        {
            return 0;
        }

        public void Insert(int index, object value)
        {
            
        }

        public bool IsFixedSize
        {
            get 
            {
                return true;
            }
        }

        public bool IsReadOnly
        {
            get 
            {
                return true;
            }
        }

        public void Remove(object value)
        {
            //TODO:
        }

        public void RemoveAt(int index)
        {
            //TODO:
        }

        public object this[int index]
        {
            get
            {
                //return _dataTable.Rows[index];
                return _rows[index];
            }
            set
            {
                
            }
        }

        public void CopyTo(Array array, int index)
        {
            //TODO:
        }

        public int Count
        {
            get 
            {
                return _rows.Length;
            }
        }

        public bool IsSynchronized
        {
            get 
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get 
            {
                return null;    
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        #endregion end of IBindingList

    }
}
