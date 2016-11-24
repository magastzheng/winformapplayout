using BLL.UFX;
using BLL.UFX.impl;
using log4net;
using Model.Binding.BindingUtil;
using Model.UFX;
using System;
using System.Collections.Generic;

namespace BLL.Entrust.subscriber
{
    public class UFXFilterBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UFXFilterBLL()
        { 
        }

        public UFXPushMessageType GetMessageType(DataParser dataParser)
        {
            UFXPushMessageType messageType = UFXPushMessageType.None;
            List<UFXFilterResponse> responseItems = new List<UFXFilterResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXFilterResponse>();

            //TODO: check the count of dataset.
            for (int i = 0, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXFilterResponse p = new UFXFilterResponse();
                    UFXDataSetHelper.SetValue<UFXFilterResponse>(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            if (responseItems.Count == 1)
            {
                var typeChar = Char.Parse(responseItems[0].MsgType);
                UFXPushMessageType tempType;
                messageType = Enum.TryParse(typeChar.ToString(), true, out tempType) ? tempType : (UFXPushMessageType)typeChar;
            }

            return messageType;
        }

    }
}
