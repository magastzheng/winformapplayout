using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SecurityInfo
{
    public class SecurityItem
    {
        public string SecuCode { get; set; }

        public string SecuName { get; set; }

        public string ExchangeCode { get; set; }

        //1 - index; 2 - stock
        public int SecuType { get; set; }

        public string ListDate { get; set; }

        public string DeListDate { get; set; }
    }
}
