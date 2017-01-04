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
            DataParser parser = SubmitSync<UFXWithdrawRequest>(Model.FunctionCode.Withdraw, requests);

            return ParseData(parser);
        }

        /// <summary>
        /// 给接口每次仅能传入一个批号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<UFXBasketWithdrawResponse> WithdrawBasket(UFXBasketWithdrawRequest request)
        {
            List<UFXBasketWithdrawRequest> requests = new List<UFXBasketWithdrawRequest> { request };
            DataParser parser = SubmitSync<UFXBasketWithdrawRequest>(Model.FunctionCode.WithdrawBasket, requests);

            return ParseData(parser);
        }

        private List<UFXBasketWithdrawResponse> ParseData(DataParser parser)
        {
            List<UFXBasketWithdrawResponse> responseItems = new List<UFXBasketWithdrawResponse>();

            var errorResponse = T2ErrorHandler.Handle(parser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                if (parser.DataSets.Count > 1)
                {
                    var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXBasketWithdrawResponse>();
                    for (int i = 1, count = parser.DataSets.Count; i < count; i++)
                    {
                        var dataSet = parser.DataSets[i];
                        foreach (var dataRow in dataSet.Rows)
                        {
                            UFXBasketWithdrawResponse p = new UFXBasketWithdrawResponse();
                            UFXDataSetHelper.SetValue<UFXBasketWithdrawResponse>(ref p, dataRow.Columns, dataFieldMap);
                            responseItems.Add(p);
                        }
                    }
                }
            }

            return responseItems;
        }
    }
}
