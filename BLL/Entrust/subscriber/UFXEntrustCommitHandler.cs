using BLL.EntrustCommand;
using Config.ParamConverter;
using log4net;
using Model.Binding.BindingUtil;
using Model.Database;
using Model.UFX;
using System.Collections.Generic;
using UFX;
using UFX.impl;
using UFX.subscriber;

namespace BLL.Entrust.subscriber
{
    public class UFXEntrustCommitHandler : IUFXMessageHandlerBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        public UFXEntrustCommitHandler()
        { 
        }

        public int Handle(DataParser dataParser)
        {
            List<UFXEntrustCompletedResponse> responseItems = new List<UFXEntrustCompletedResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXEntrustCompletedResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                responseItems = UFXDataSetHelper.ParseSubscribeData<UFXEntrustCompletedResponse>(dataParser);
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
                        _entrustSecurityBLL.UpdateEntrustStatus(submitId, commandId, responseItem.StockCode, Model.EnumType.EntrustStatus.Completed);
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

        private EntrustSecurity Convert(UFXEntrustCompletedResponse responseItem)
        {
            var entrustItem = new EntrustSecurity 
            {
                RequestId = responseItem.ExtSystemId,
                SecuCode = responseItem.StockCode,
                EntrustNo = responseItem.EntrustNo,
                BatchNo = responseItem.BatchNo,
            };

            return entrustItem;
        }
    }
}
