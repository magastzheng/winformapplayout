using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BLL
{
    public enum BLLResponseCode 
    {
        EmptyEntrustItem = -200,
        FailEntrust = -100,
        SuccessEntrust = 0,
    }

    public class BLLResponse
    {
        public ConnectionCode Code { get; set; }
        public string Message { get; set; }

        public BLLResponse()
        { 
        }

        public BLLResponse(ConnectionCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public static bool Success(BLLResponse response)
        {
            return response.Code == ConnectionCode.Success || response.Code == ConnectionCode.SuccessEntrust;
        }
    }
}
