using Model.EnumType;
using System;

namespace Model.UI
{
    public class EntrustCommandItem
    {
        public int SubmitId { get; set; }

        public int CommandId { get; set; }

        public int Copies { get; set; }

        public int EntrustNo { get; set; }

        public int BatchNo { get; set; }

        public EntrustStatus EntrustStatus { get; set; }

        public DealStatus DealStatus { get; set; }

        public int SubmitPerson { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int EntrustFailCode { get; set; }

        public string EntrustFailCause { get; set; }
    }
}
