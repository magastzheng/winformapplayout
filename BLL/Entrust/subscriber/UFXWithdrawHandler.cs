using BLL.EntrustCommand;
using Config.ParamConverter;
using log4net;
using Model.Binding.BindingUtil;
using Model.Database;
using Model.UFX;
using System;
using System.Collections.Generic;
using UFX;
using UFX.subscriber;

namespace BLL.Entrust.subscriber
{
    public class UFXWithdrawHandler : IUFXMessageHandlerBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EntrustCombineBLL _entrustCombineBLL = new EntrustCombineBLL();

        public UFXWithdrawHandler()
        { 
        }

        public int Handle(UFX.impl.DataParser dataParser)
        {
            List<UFXWithdrawResponse> responseItems = new List<UFXWithdrawResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXWithdrawResponse>();
            var errorResponse = T2ErrorHandler.Handle(dataParser);
            if (T2ErrorHandler.Success(errorResponse.ErrorCode))
            {
                responseItems = UFXDataSetHelper.ParseSubscribeData<UFXWithdrawResponse>(dataParser);
            }

            //update the database
            if (responseItems != null && responseItems.Count > 0)
            {
                var entrustItems = Convert(responseItems);
                _entrustCombineBLL.UpdateSecurityEntrustStatus(entrustItems, Model.EnumType.EntrustStatus.CancelToUFX);
            }

            return responseItems.Count;
        }

        private List<EntrustSecurity> Convert(List<UFXWithdrawResponse> responseItems)
        {
            List<EntrustSecurity> entrustItems = new List<EntrustSecurity>();

            foreach (var responseItem in responseItems)
            {
                int commandId;
                int submitId;
                int requestId;

                if (EntrustRequestHelper.ParseThirdReff(responseItem.ThirdReff, out commandId, out submitId, out requestId))
                {
                    EntrustSecurity entrustItem = new EntrustSecurity
                    {
                        CommandId = commandId,
                        SubmitId = submitId,
                        RequestId = requestId,
                        SecuCode = responseItem.StockCode,
                        EntrustNo = responseItem.EntrustNo,
                        BatchNo = responseItem.BatchNo,
                    };

                    entrustItems.Add(entrustItem);
                }
                else
                {
                    string msg = string.Format("Fail to parse the third_reff: [{0}], entrust_no: [{1}].", responseItem.ThirdReff, responseItem.EntrustNo);
                    logger.Error(msg);
                }
            }

            return entrustItems;
        }
    }
}
