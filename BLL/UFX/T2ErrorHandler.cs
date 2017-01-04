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
            foreach (var column in row.Columns)
            {
                switch (column.Key)
                {
                    case "ErrorCode":
                        {
                            errorResponse.ErrorCode = column.Value.GetInt();
                        }
                        break;
                    case "ErrorMsg":
                        {
                            errorResponse.ErrorMessage = column.Value.GetStr();
                        }
                        break;
                    case "MsgDetail":
                        {
                            errorResponse.MessageDetail = column.Value.GetStr();
                        }
                        break;
                    case "DataCount":
                        {
                            errorResponse.DataCount = column.Value.GetInt();
                        }
                        break;
                }
            }

            return errorResponse;
        }

        public static bool Success(int errorCode)
        {
            return errorCode == 0;
        }
    }
}
