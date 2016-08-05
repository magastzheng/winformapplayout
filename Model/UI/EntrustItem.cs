using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class EntrustItem
    {
        [BindingAttribute("commandno")]
        public int CommandNo { get; set; }

        [BindingAttribute("copies")]
        public int Copies { get; set; }
    }
}
