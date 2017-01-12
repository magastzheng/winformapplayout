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
            responseItems = UFXDataSetHelper.ParseSubscribeData<UFXFilterResponse>(dataParser);

            if (responseItems != null && responseItems.Count == 1)
            {
                var typeChar = Char.Parse(responseItems[0].MsgType);
                UFXPushMessageType tempType;
                messageType = Enum.TryParse(typeChar.ToString(), true, out tempType) ? tempType : (UFXPushMessageType)typeChar;
            }

            return messageType;
        }

    }
}
