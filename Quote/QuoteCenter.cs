using Model.EnumType;
using Model.Quote;
using Model.SecurityInfo;
using System;
using System.Collections.Generic;
using WAPIWrapperCSharp;

namespace Quote
{
    public class QuoteCenter
    {
        private readonly static QuoteCenter _instance = new QuoteCenter();
        private Quote _quote = new Quote();
        public Quote Quote
        {
            get { return _quote; }
        }

        private QuoteCenter()
        {

        }

        public void Query()
        {
            QueryHelper queryHelper = new QueryHelper();
            List<string> windCodes = queryHelper.GetSecuCode();
            List<string> fields = new List<string> { "rt_last" };
            Dictionary<string, string> optionMap = new Dictionary<string, string>();
            Dictionary<string, int> fieldIndexMap = QueryHelper.GetFieldIndex(fields);

            WindData wd = WindAPIWrap.Instance.SyncRequestData(windCodes, fields, optionMap);
            if (wd != null)
            {
                FillData(wd, fieldIndexMap);
            }
        }

        public void Query(List<string> secuCodes, List<PriceType> priceTypes)
        {
            QueryHelper queryHelper = new QueryHelper();
            List<string> windCodes = queryHelper.GetSecuCode(secuCodes);
            //List<string> fields = new List<string> { "rt_last" };
            List<string> fields = queryHelper.GetPriceFields(priceTypes);
            Dictionary<string, string> optionMap = new Dictionary<string, string>();
            Dictionary<string, int> fieldIndexMap = QueryHelper.GetFieldIndex(fields);

            WindData wd = WindAPIWrap.Instance.SyncRequestData(windCodes, fields, optionMap);
            if (wd != null)
            {
                FillData(wd, fieldIndexMap);
            }
        }

        public void Query(List<SecurityItem> secuItems, List<PriceType> priceTypes)
        {
            QueryHelper queryHelper = new QueryHelper();
            List<string> windCodes = queryHelper.GetSecuCode(secuItems);
            //List<string> fields = new List<string> { "rt_last" };
            List<string> fields = queryHelper.GetPriceFields(priceTypes);
            Dictionary<string, string> optionMap = new Dictionary<string, string>();
            Dictionary<string, int> fieldIndexMap = QueryHelper.GetFieldIndex(fields);

            WindData wd = WindAPIWrap.Instance.SyncRequestData(windCodes, fields, optionMap);
            if (wd != null)
            {
                FillData(wd, fieldIndexMap);
            }
        }

        public void Query(List<SecurityItem> secuItems)
        {
            QueryHelper queryHelper = new QueryHelper();
            List<string> allFields = QueryHelper.AllFields;
            List<string> windCodes = queryHelper.GetSecuCode(secuItems);
            Dictionary<string, string> optionMap = new Dictionary<string, string>();
            if (windCodes == null || windCodes.Count == 0)
            {
                return;
            }

            foreach (var field in allFields)
            {
                var fields = new List<string>() { field };
                var fieldIndexMap = QueryHelper.GetFieldIndex(fields);
                var wd = WindAPIWrap.Instance.SyncRequestData(windCodes, fields, optionMap);
                if (wd != null && wd.errorCode == 0)
                {
                    FillData(wd, fieldIndexMap);
                }
                else
                {
                    //TODO: report the error
                }
            }
        }

        public MarketData GetMarketData(SecurityItem secuItem)
        {
            string windCode = QueryHelper.GetWindCode(secuItem);

            MarketData marketData = new MarketData
            {
                InstrumentID = windCode
            };


            return _quote.Get(windCode);
        }

        private void FillData(WindData wd, Dictionary<string, int> fieldIndexMap)
        {
            if (wd == null || wd.errorCode != 0)
            {
                return;
            }

            int clength = wd.codeList.Length;
            int flength = wd.fieldList.Length;
            int tlength = wd.timeList.Length;
            for (int i = 0; i < tlength; i++)
            {
                int serialLen = clength * flength;

                //loop for the code
                for (int j = 0; j < clength; j++)
                {
                    string investmentId = wd.codeList[j];
                    MarketData marketData = _quote.Get(investmentId);

                    //loop for the field
                    if (wd.data is Array)
                    {
                        foreach (var kv in fieldIndexMap)
                        {
                            int index = kv.Value;
                            double dval = (double)((Array)wd.data).GetValue(i * serialLen + j * flength + index);
                            switch (kv.Key)
                            {
                                case "rt_last":
                                    marketData.CurrentPrice = dval;
                                    break;
                                case "rt_ask1":
                                    marketData.SellPrice1 = dval;
                                    break;
                                case "rt_ask2":
                                    marketData.SellPrice2 = dval;
                                    break;
                                case "rt_ask3":
                                    marketData.SellPrice3 = dval;
                                    break;
                                case "rt_ask4":
                                    marketData.SellPrice4 = dval;
                                    break;
                                case "rt_ask5":
                                    marketData.SellPrice5 = dval;
                                    break;
                                case "rt_ask6":
                                    break;
                                case "rt_ask7":
                                    break;
                                case "rt_ask8":
                                    break;
                                case "rt_ask9":
                                    break;
                                case "rt_ask10":
                                    break;
                                case "rt_bid1":
                                    marketData.BuyPrice1 = dval;
                                    break;
                                case "rt_bid2":
                                    marketData.BuyPrice2 = dval;
                                    break;
                                case "rt_bid3":
                                    marketData.BuyPrice3 = dval;
                                    break;
                                case "rt_bid4":
                                    marketData.BuyPrice4 = dval;
                                    break;
                                case "rt_bid5":
                                    marketData.BuyPrice5 = dval;
                                    break;
                                case "rt_upward_vol":
                                    marketData.BuyAmount = Convert.ToInt64(dval);
                                    break;
                                case "rt_downward_vol":
                                    marketData.SellAmount = Convert.ToInt64(dval);
                                    break;
                                case "rt_susp_flag":
                                    {
                                        marketData.SuspendFlag = QueryHelper.GetSuspendFlag(Convert.ToInt32(dval));
                                    }
                                    break;
                                case "rt_trade_status":
                                    {
                                        marketData.TradingStatus = QueryHelper.GetTradingStatus(Convert.ToInt32(dval));
                                    }
                                    break;
                                case "rt_high_limit":
                                    {
                                        marketData.HighLimitPrice = dval;
                                    }
                                    break;
                                case "rt_low_limit":
                                    {
                                        marketData.LowLimitPrice = dval;
                                    }
                                    break;
                                case "rt_pre_close":
                                    {
                                        marketData.PreClose = dval;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }//end to fill read the field and fill into MarketData object
                }//end to loop secucode
            }//end to loop datetime
        }

        public static QuoteCenter Instance { get { return _instance; } }
    }
}
