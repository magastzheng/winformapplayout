using log4net;
using Model;
using Model.Binding.BindingUtil;
using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<UFXQueryMoneyResponse> responseItems = new List<UFXQueryMoneyResponse>();

            DataParser parser = SubmitSync<UFXQueryMoneyRequest>(UFXFunctionCode.QueryAccountMoney, requests);
            var errorResponse = T2ErrorHandler.Handle(parser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXQueryEntrustResponse>();
                for (int i = 1, count = parser.DataSets.Count; i < count; i++)
                {
                    var dataSet = parser.DataSets[i];
                    foreach (var dataRow in dataSet.Rows)
                    {
                        UFXQueryMoneyResponse p = new UFXQueryMoneyResponse();
                        UFXDataSetHelper.SetValue<UFXQueryMoneyResponse>(ref p, dataRow.Columns, dataFieldMap);
                        responseItems.Add(p);
                    }
                }
            }

            return responseItems;
        }
    }
}
