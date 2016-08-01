using Model.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class ClosePositionCmdItem
    {
        [BindingAttribute("copies")]
        public int Copies { get; set; }

        [BindingAttribute("instanceid")]
        public int InstanceId { get; set; }

        [BindingAttribute("instancecode")]
        public string InstanceCode { get; set; }

        [BindingAttribute("monitorname")]
        public string MonitorName { get; set; }

        [BindingAttribute("tradedirection")]
        public string TradeDirection { get; set; }

        [BindingAttribute("spottemplate")]
        public string SpotTemplate { get; set; }

        [BindingAttribute("futurescontract")]
        public string FuturesContract { get; set; }
    }
}
