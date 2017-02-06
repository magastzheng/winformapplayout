using log4net;
using Model.UFX;
using System.Collections.Generic;

namespace BLL.UFX.impl
{
    public class QuerySyncBLL:UFXBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public QuerySyncBLL(T2SDKWrap t2SDKWrap)
            :base(t2SDKWrap)
        {
        }

        public List<UFXQueryMoneyResponse> QueryAccountMoney(List<UFXQueryMoneyRequest> requests)
        {
            DataParser parser = SubmitSync<UFXQueryMoneyRequest>(UFXFunctionCode.QueryAccountMoney, requests);
            return UFXDataSetHelper.ParseData<UFXQueryMoneyResponse>(parser);
        }

        public List<UFXHoldingResponse> QuerySecurityHolding(List<UFXHoldingRequest> requests)
        {
            DataParser parser = SubmitSync<UFXHoldingRequest>(UFXFunctionCode.QuerySecurityHolding, requests);
            return UFXDataSetHelper.ParseData<UFXHoldingResponse>(parser);
        }

        public List<UFXFutureHoldingResponse> QueryFutureHolding(List<UFXFutureHoldingRequest> requests)
        {
            DataParser parser = SubmitSync<UFXFutureHoldingRequest>(UFXFunctionCode.QueryFutureHolding, requests);
            return UFXDataSetHelper.ParseData<UFXFutureHoldingResponse>(parser);
        }

        public List<UFXFutureHoldingDetailResponse> QueryFutureHoldingDetail(List<UFXFutureHoldingDetailRequest> requests)
        {
            DataParser parser = SubmitSync<UFXFutureHoldingDetailRequest>(UFXFunctionCode.QueryFutureDetailHolding, requests);
            return UFXDataSetHelper.ParseData<UFXFutureHoldingDetailResponse>(parser);
        }

        public List<UFXMultipleHoldingResponse> QueryMultipleHolding(List<UFXHoldingRequest> requests)
        {
            DataParser parser = SubmitSync<UFXHoldingRequest>(UFXFunctionCode.QueryMultipleHolding, requests);
            return UFXDataSetHelper.ParseData<UFXMultipleHoldingResponse>(parser);
        }
    }
}
