using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls.GridView
{
    public class DataGridViewRowHeaderCellEx: DataGridViewRowHeaderCell
    {
        public DataGridViewRowHeaderCellEx()
            : base()
        { 
        }

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            TextRenderer.DrawText(graphics, (rowIndex + 1).ToString(), DataGridView.Font, cellBounds, Color.Black);
        }
    }
}
