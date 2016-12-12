
namespace Model.Database
{
    public class EntrustSecurityCombine : EntrustSecurity
    {
        public int InstanceId { get; set; }

        public string InstanceCode { get; set; }

        public int MonitorUnitId { get; set; }

        public int PortfolioId { get; set; }

        public string PortfolioCode { get; set; }

        public string PortfolioName { get; set; }

        public string AccountCode { get; set; }

        public string AccountName { get; set; }
    }
}
