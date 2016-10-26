using Model.EnumType;

namespace Model.Setting
{
    public class DefaultEntrustSetting
    {
        public PriceType BuyFutuPrice { get; set; }

        public PriceType SellFutuPrice { get; set; }

        public PriceType BuySpotPrice { get; set; }

        public PriceType SellSpotPrice { get; set; }

        public int BuySellEntrustOrder { get; set; }

        public int AutoRatio { get; set; }

        public int FragmentedStockMode { get; set; }

        public EntrustPriceType SzseEntrustPriceType { get; set; }

        public EntrustPriceType SseEntrustPriceType { get; set; }
    }
}
