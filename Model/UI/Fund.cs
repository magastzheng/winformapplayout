using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
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
        public string AccountType
        {
            get { return EnumTypeDisplayHelper.GetFundAccountType(EAccountType); }
        }

        public string ManagerCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public FundAccountType EAccountType { get; set; }
    }
}
