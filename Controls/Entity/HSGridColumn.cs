using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Entity
{
    public enum HSGridColumnType
    {
        None = -1,
        Text = 0,
        CheckBox = 1,
        Image = 2,
        ComboBox = 3,
        NumericUpDown = 4,
    }

    public class HSGridColumn
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public int Width { get; set; }
        public HSGridColumnType ColumnType { get; set; }
        public DataValueType ValueType { get; set; }
        public int Visible { get; set; }
        public int ReadOnly { get; set; }
        public string DefaultValue { get; set; }
    }

    public class HSGrid
    {
        public string Grid { get; set; }
        public List<HSGridColumn> Columns { get; set; }
    }
}
