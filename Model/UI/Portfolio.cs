using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;

namespace Model.UI
{
    public class Portfolio
    {
        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("fundcode")]
        public string FundCode { get; set; }

        [BindingAttribute("accounttype")]
        public string AccountType
        {
            get { return EnumTypeDisplayHelper.GetFundAccountType(EAccountType); }
        }

        [BindingAttribute("capitalaccount")]
        public string CapitalAccount { get; set; }

        [BindingAttribute("assetno")]
        public string AssetNo { get; set; }

        [BindingAttribute("assetname")]
        public string AssetName { get; set; }

        [BindingAttribute("portfoliono")]
        public string PortfolioNo { get; set; }

        [BindingAttribute("portfolioname")]
        public string PortfolioName { get; set; }

        public int PortfolioId { get; set; }

        public PortfolioStatus PortfolioStatus { get; set; }

        public FundAccountType EAccountType { get; set; }
    }
}
