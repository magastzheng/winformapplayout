using BLL.EntrustCommand;
using Config.ParamConverter;
using log4net;
using Model.Binding.BindingUtil;
using Model.UFX;
using System;
using System.Collections.Generic;
using UFX;
using UFX.subscriber;

namespace BLL.Entrust.subscriber
{
    public class UFXEntrustConfirmHandler : IUFXMessageHandlerBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EntrustSecurityBLL _entrustSecurityBLL = new EntrustSecurityBLL();

        public UFXEntrustConfirmHandler()
        { 
        }

        public int Handle(UFX.impl.DataParser dataParser)
        {
            List<UFXEntrustConfirmResponse> responseItems = new List<UFXEntrustConfirmResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXEntrustConfirmResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                responseItems = UFXDataSetHelper.ParseSubscribeData<UFXEntrustConfirmResponse>(dataParser);
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
                        //_entrustSecurityBLL.UpdateEntrustStatus(submitId, commandId, responseItem.StockCode, Model.EnumType.EntrustStatus.Completed);
                        _entrustSecurityBLL.UpdateConfirmNo(submitId, commandId, responseItem.StockCode, responseItem.ConfirmNo);
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
