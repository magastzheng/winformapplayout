using Controls.Entity;
using Model.Binding;
using Model.config;
using System.Collections.Generic;

namespace ControlsTest.Entity
{
    public class CBRowItem
    {
        [BindingAttribute("name")]
        public string Name { get; set; }

        [BindingAttribute("id")]
        public string Id { get; set; }

        [BindingAttribute("id_source")]
        public ComboOption IdSource { get; set; }
    }

    public static class CBRowItemHelper
    {
        public static TSGrid GetGridConfig()
        {
            TSGrid hsGrid = new TSGrid
            {
                Columns = new List<TSGridColumn>()
            };

            TSGridColumn col1 = new TSGridColumn
            {
                Name = "name",
                Text = "Name",
                ColumnType = TSGridColumnType.Text,
                ValueType = Model.Data.DataValueType.String,
                Width = 60,
                Visible = 1
            };
            hsGrid.Columns.Add(col1);


            TSGridColumn col2 = new TSGridColumn
            {
                Name = "id",
                Text = "Id",
                ColumnType = TSGridColumnType.ComboBox,
                ValueType = Model.Data.DataValueType.String,
                Width = 120,
                Visible = 1
            };
            hsGrid.Columns.Add(col2);

            TSGridColumn col3 = new TSGridColumn
            {
                Name = "id_source",
                Text = "IdSource",
                ColumnType = TSGridColumnType.Text,
                ValueType = Model.Data.DataValueType.String,
                Width = 60,
                Visible = 0
            };
            hsGrid.Columns.Add(col3);

            return hsGrid;
        }

        public static List<CBRowItem> GetData()
        {
            var listData = new List<CBRowItem>();
            CBRowItem item = new CBRowItem
            {
                Name = "Test 1",
                Id = "Cb0_0",
                IdSource = GetComboBoxData(0)
            };
            listData.Add(item);

            item = new CBRowItem
            {
                Name = "Test 2",
                Id = "Cb1_2",
                IdSource = GetComboBoxData(1)
            };
            listData.Add(item);

            item = new CBRowItem
            {
                Name = "Test 3",
                Id = "Cb2_3",
                IdSource = GetComboBoxData(2)
            };
            listData.Add(item);

            return listData;
        }

        public static ComboOption GetComboBoxData(int index)
        {
            string suffix = index.ToString();

            ComboOption cbOption = new ComboOption
            {
                Name = "combobox" + suffix,
                Items = new List<ComboOptionItem>()
            };

            for (int i = 0; i < 4; i++)
            {
                string id = string.Format("Cb{0}_{1}", index, i);
                string text = string.Format("{0} Text", id);
                ComboOptionItem item = new ComboOptionItem
                {
                    Id = id,
                    Name = text,
                    Order1 = i
                };
                cbOption.Items.Add(item);
            }

            return cbOption;
        }
    }
}
