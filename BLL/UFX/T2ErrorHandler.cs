using BLL.UFX.impl;
using Model.UFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UFX
{
    public class T2ErrorHandler
    {
        public static UFXErrorResponse Handle(DataParser dataParser)
        {
            //Set the default error code as -1, meaning failure
            UFXErrorResponse errorResponse = new UFXErrorResponse
            {
                ErrorCode = -1
            };

            if (dataParser.DataSets.Count == 0)
            {
                return errorResponse;
            }

            var dataSet = dataParser.DataSets[0];
            if (dataSet.Rows.Count == 0)
            {
                return errorResponse;
            }

            var row = dataSet.Rows[0];
            if (row.Columns.ContainsKey("ErrorCode"))
            {
                errorResponse.ErrorCode = row.Columns["ErrorCode"].GetInt();
            }
            if (row.Columns.ContainsKey("ErrorMsg"))
            {
                errorResponse.ErrorMessage = row.Columns["ErrorMsg"].GetStr();
            }
            if (row.Columns.ContainsKey("MsgDetail"))
            {
                errorResponse.MessageDetail = row.Columns["MsgDetail"].GetStr();
            }
            if (row.Columns.ContainsKey("DataCount"))
            {
                errorResponse.DataCount = row.Columns["DataCount"].GetInt();
            }

            //subscriber
            if (row.Columns.ContainsKey("msgtype"))
            {
                errorResponse.ErrorCode = 0;
            }
            
            return errorResponse;
        }

        public static bool Success(int errorCode)
        {
            return errorCode == 0;
        }
    }
}
