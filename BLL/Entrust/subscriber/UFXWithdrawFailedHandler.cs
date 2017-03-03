using BLL.EntrustCommand;
using Config.ParamConverter;
using log4net;
using Model.UFX;
using System.Collections.Generic;
using UFX;
using UFX.impl;
using UFX.subscriber;

namespace BLL.Entrust.subscriber
{
    public class UFXWithdrawFailedHandler : IUFXMessageHandlerBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        public UFXWithdrawFailedHandler()
        { 
        
        }

        public int Handle(DataParser dataParser)
        {
            List<UFXWithdrawFailedResponse> responseItems = new List<UFXWithdrawFailedResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                responseItems = responseItems = UFXDataSetHelper.ParseSubscribeData<UFXWithdrawFailedResponse>(dataParser);
            }

            //update the database
            if (responseItems != null && responseItems.Count > 0)
            {
                foreach (var responseItem in responseItems)
                {
                    int commandId;
                    int submitId;
                    int requestId;

                    //TODO: add log
                    if (EntrustRequestHelper.ParseThirdReff(responseItem.ThirdReff, out commandId, out submitId, out requestId))
                    {
                        _entrustSecurityBLL.UpdateEntrustStatus(submitId, commandId, responseItem.StockCode, Model.EnumType.EntrustStatus.CancelFail);
                    }
                    else
                    {
                        string msg = string.Format("Fail to parse the third_reff: [{0}], entrust_no: [{1}].", responseItem.ThirdReff, responseItem.EntrustNo);
                        logger.Error(msg);
                    }
                }
            }

            return responseItems.Count;
        }

    }
}
