using BLL.UFX.impl;
using log4net;
using Model.Binding.BindingUtil;
using Model.t2sdk;
using System.Collections.Generic;

namespace BLL.Entrust.subscriber
{
    public class UFXWithdrawCompletedBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UFXWithdrawCompletedBLL()
        { 
        }

        public int Handle(DataParser dataParser)
        {
            List<UFXWithdrawCompletedResponse> responseItems = new List<UFXWithdrawCompletedResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXWithdrawCompletedResponse>();
            
            //TODO:
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXWithdrawCompletedResponse p = new UFXWithdrawCompletedResponse();
                    UFXDataSetHelper.SetValue<UFXWithdrawCompletedResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            //update the database

            return responseItems.Count;
        }
    }
}
