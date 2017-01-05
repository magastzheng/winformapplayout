using Model.EnumType;
using System;

namespace Model.Database
{
    //使用篮子委托接口，委托之后，获得BatchNo
    //如果使用普通委托接口，委托之后，仅能获得委托号EntrustNo
    public class EntrustCommand
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
