using Model.Data;
using System.Collections.Generic;

namespace Controls.Entity
{
    public enum TSGridColumnType
    {
        None = -1,
        Text = 0,
        CheckBox = 1,
        Image = 2,
        ComboBox = 3,
        NumericUpDown = 4,
    }

    public class TSGridColumn
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public int Width { get; set; }
        public TSGridColumnType ColumnType { get; set; }
        public DataValueType ValueType { get; set; }
        public int Visible { get; set; }
        public int ReadOnly { get; set; }
        public string DefaultValue { get; set; }
    }

    public class TSGrid
    {
        public string Grid { get; set; }
        public int Background { get; set; }
        public List<TSGridColumn> Columns { get; set; }
    }
}
