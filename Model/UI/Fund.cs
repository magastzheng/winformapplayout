using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class Fund
    {
        public int FundId { get; set; }

        public string FundCode { get; set; }

        public string FundName { get; set; }

        public string ManagerCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
