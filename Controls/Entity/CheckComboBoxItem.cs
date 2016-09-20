using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Entity
{
    public class CheckComboBoxItem
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool IsCheck { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
