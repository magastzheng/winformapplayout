using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class Fund
    {
        //public int FundId { get; set; }
        [BindingAttribute("fundcode")]
        public string FundCode { get; set; }

        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("accounttype")]
        public int AccountType { get; set; }

        public string ManagerCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
