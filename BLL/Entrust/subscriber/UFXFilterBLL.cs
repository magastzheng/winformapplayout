using BLL.UFX.impl;
using log4net;
using Model.Binding.BindingUtil;
using Model.t2sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entrust.subscriber
{
    public class UFXFilterBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UFXFilterBLL()
        { 
        }

        public PushMessageType GetMessageType(DataParser dataParser)
        {
            PushMessageType messageType = PushMessageType.None;
            List<UFXFilterResponse> responseItems = new List<UFXFilterResponse>();
            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXFilterResponse>();

            //TODO: check the count of dataset.
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
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
                PushMessageType tempType;
                messageType = Enum.TryParse(typeChar.ToString(), true, out tempType) ? tempType : (PushMessageType)typeChar;
            }

            return messageType;
        }

    }
}
