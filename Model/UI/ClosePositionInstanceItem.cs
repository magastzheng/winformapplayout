using Model.Binding;
using Model.Dialog;
using System;
using System.Collections.Generic;

namespace Model.UI
{
    public class ClosePositionInstanceItem : OrderConfirmItem
    {
        [BindingAttribute("futubuymktval")]
        public double FutuBuyMktVal { get; set; }

        [BindingAttribute("futusellmktval")]
        public double FutuSellMktVal { get; set; }

        [BindingAttribute("spotbuymktval")]
        public double SpotBuyMktVal { get; set; }

        [BindingAttribute("spotsellmktval")]
        public double SpotSellMktVal { get; set; }
    }
}
