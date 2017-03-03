using log4net;
using Model.Binding.BindingUtil;
using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFX.impl
{
    public class WithdrawSyncBLL:UFXBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WithdrawSyncBLL(T2SDKWrap t2SDKWrap)
            :base(t2SDKWrap)
        {
        }

        /// <summary>
        /// 可以批量撤销委托证券
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        public List<UFXBasketWithdrawResponse> Withdraw(List<UFXWithdrawRequest> requests)
        {
            DataParser parser = SubmitSync<UFXWithdrawRequest>(UFXFunctionCode.Withdraw, requests);

            return GetResponse(parser);
        }

        /// <summary>
        /// 给接口每次仅能传入一个批号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<UFXBasketWithdrawResponse> WithdrawBasket(UFXBasketWithdrawRequest request)
        {
            List<UFXBasketWithdrawRequest> requests = new List<UFXBasketWithdrawRequest> { request };
            DataParser parser = SubmitSync<UFXBasketWithdrawRequest>(UFXFunctionCode.WithdrawBasket, requests);

            return GetResponse(parser);
        }

        private List<UFXBasketWithdrawResponse> GetResponse(DataParser parser)
        {
            var errorResponse = T2ErrorHandler.Handle(parser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                return UFXDataSetHelper.ParseData<UFXBasketWithdrawResponse>(parser);
            }
            else
            {
                string msg = string.Format("Fail to withdraw - error code: {0}, message: {1}, detail: {2}", 
                    errorResponse.ErrorCode, errorResponse.ErrorMessage, errorResponse.MessageDetail);
                logger.Error(msg);

                return new List<UFXBasketWithdrawResponse>();
            }
        }
    }
}
