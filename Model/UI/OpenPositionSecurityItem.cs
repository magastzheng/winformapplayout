using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class OpenPositionSecurityItem
    {
        [BindingAttribute("secucode")]
        public string SecuCode { get; set; }

        [BindingAttribute("secuname")]
        public string SecuName { get; set; }

        [BindingAttribute("weightamount")]
        public int WeightAmount { get; set; }

        [BindingAttribute("entrustamount")]
        public int EntrustAmount { get; set; }

        [BindingAttribute("commandprice")]
        public double CommandPrice { get; set; }

        [BindingAttribute("commandmoney")]
        public double CommandMoney { get; set; }

        [BindingAttribute("entrustdirection")]
        public int EntrustDirection { get; set; }

        [BindingAttribute("lastprice")]
        public double LastPrice { get; set; }

        [BindingAttribute("buyamount")]
        public int BuyAmount { get; set; }

        [BindingAttribute("sellamount")]
        public int SellAmount { get; set; }

        [BindingAttribute("suspensionflag")]
        public int SuspensionFlag { get; set; }

        [BindingAttribute("replacestatus")]
        public int ReplaceStatus { get; set; }

        [BindingAttribute("limitmove")]
        public int LimitMove { get; set; }
    }
}
