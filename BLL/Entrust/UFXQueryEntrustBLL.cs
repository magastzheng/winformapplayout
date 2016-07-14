﻿using BLL.UFX.impl;
using log4net;
using Model.Binding.BindingUtil;
using Model.Data;
using Model.t2sdk;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Entrust
{
    public class UFXQueryEntrustBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SecurityBLL _securityBLL = null;
        //private TradeCommandBLL _tradeCommandBLL = null;
        private AutoResetEvent _waitEvent = new AutoResetEvent(false);

        public UFXQueryEntrustBLL()
        {
            this._securityBLL = BLLManager.Instance.SecurityBLL;
        }

        public List<EntrustFlowItem> QueryToday()
        {
            List<UFXQueryEntrustRequest> requests = new List<UFXQueryEntrustRequest>();
            UFXQueryEntrustRequest request = new UFXQueryEntrustRequest();
            request.RequestNum = 10000;
            request.CombiNo = "30";
            requests.Add(request);

            Callbacker callbacker = new Callbacker
            {
                Token = new CallbackToken
                {
                    SubmitId = 11111,
                    CommandId = 22222,
                },

                Callback = QueryToday,
            };

            var result = _securityBLL.QueryEntrust(requests, callbacker);

            _waitEvent.WaitOne(30 * 1000);

            return null;
        }

        private int QueryToday(CallbackToken token, DataParser dataParser)
        {
            List<UFXQueryEntrustResponse> responseItems = new List<UFXQueryEntrustResponse>();

            var dataFieldMap = UFXDataBindingHelper.GetProperty<UFXQueryEntrustResponse>();
            for (int i = 1, count = dataParser.DataSets.Count; i < count; i++)
            {
                var dataSet = dataParser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    UFXQueryEntrustResponse p = new UFXQueryEntrustResponse();
                    SetValue(ref p, dataRow.Columns, dataFieldMap);
                    responseItems.Add(p);
                }
            }

            _waitEvent.Set();

            return responseItems.Count();
        }

        private void SetValue(ref UFXQueryEntrustResponse p, Dictionary<string, DataValue> columns, Dictionary<string, UFXDataField> dataFieldMap)
        {
            foreach (var column in columns)
            {
                if (dataFieldMap.ContainsKey(column.Key))
                {
                    var dataField = dataFieldMap[column.Key];
                    Type type = p.GetType();
                    switch (dataField.ValueType)
                    {
                        case Model.Data.DataValueType.Int:
                            {
                                var val = column.Value.GetInt();
                                type.GetProperty(dataField.Name).SetValue(p, val);
                            }
                            break;
                        case Model.Data.DataValueType.Float:
                            {
                                var val = column.Value.GetDouble();
                                type.GetProperty(dataField.Name).SetValue(p, val);
                            }
                            break;
                        case Model.Data.DataValueType.String:
                            {
                                var val = column.Value.GetStr();
                                type.GetProperty(dataField.Name).SetValue(p, val);
                            }
                            break;
                        default:
                            type.GetProperty(dataField.Name).SetValue(p, column.Value.Value);
                            break;
                    }
                }
            }
        }
    }
}
