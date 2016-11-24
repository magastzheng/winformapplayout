using BLL.UFX;
using BLL.UFX.impl;
using log4net;
using Model.Binding.BindingUtil;
using Model.UFX;
using Model.UI;
using System.Collections.Generic;

namespace BLL.Entrust.subscriber
{
    public class UFXEntrustCommitBLL : IUFXSubsriberBLLBase
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UFXEntrustCommitBLL()
        { 
        }

        public int Handle(DataParser dataParser)
        {
            List<UFXEntrustCompletedResponse> responseItems = new List<UFXEntrustCompletedResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXEntrustCompletedResponse>();
            for (int i = 0, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXEntrustCompletedResponse p = new UFXEntrustCompletedResponse();
                    UFXDataSetHelper.SetValue<UFXEntrustCompletedResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            //update the database


            return responseItems.Count;
        }

        private EntrustSecurityItem Convert(UFXEntrustCompletedResponse responseItem)
        {
            var entrustItem = new EntrustSecurityItem 
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
