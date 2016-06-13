using Model.Data;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class EntrustSecurityItem
    {
        public int SubmitId { get; set; }

        public int CommandId { get; set; }

        public string SecuCode { get; set; }

        public SecurityType SecuType { get; set; }

        public int EntrustAmount { get; set; }

        public double EntrustPrice { get; set; }

        public EntrustDirection EntrustDirection { get; set; }

        public EntrustStatus EntrustStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
