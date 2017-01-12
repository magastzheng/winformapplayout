using log4net;
using Model.Binding.BindingUtil;
using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UFX.impl
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

            return UFXDataSetHelper.ParseData<UFXBasketWithdrawResponse>(parser);
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

            return UFXDataSetHelper.ParseData<UFXBasketWithdrawResponse>(parser);
        }
    }
}
