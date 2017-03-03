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
    public class UFXWithdrawCompletedHandler : IUFXMessageHandlerBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        public UFXWithdrawCompletedHandler()
        { 
        }

        public int Handle(DataParser dataParser)
        {
            List<UFXWithdrawCompletedResponse> responseItems = new List<UFXWithdrawCompletedResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                responseItems = UFXDataSetHelper.ParseSubscribeData<UFXWithdrawCompletedResponse>(dataParser);
            }

            //update the database
            //handle in the message or in the return of the call place?
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
                        _entrustSecurityBLL.UpdateEntrustStatusByRequestId(requestId, responseItem.EntrustNo, responseItem.BatchNo, 0, string.Empty);
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
