using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum ConnectionCode
    {
        //0 ~ 199 basic with UFX success
        Success = 0,
        SuccessSubscribe = 1,

        //200~299 database

        //300~500 business success
        //SuccessEntrust = 300,
        //SuccessWithdraw = 301,

        //-1 ~ -199 basic UFX connect error code
        ErrorReadConf = -1,
        ErrorInitConn = -2,
        ErrorConn = -3,
        ErrorSendMsg = -4,
        ErrorFailContent = -5,
        ErrorRecvMsg = -6,
        ErrorLogin = -10001,
        ErrorNoLogin = -10002,
        ErrorNoCallback = -30001,
        ErrorNoFunctionCode = -35001,
        ErrorFailSubscribe = -40001,

        //-200~-299 database error code
        DBInsertFail = -200,
        DBTransctionFail = -201,
        DBUpdateFail = -202,

        //-300 ~ -500 business success
        EmptyEntrustItem = -300,
        FailEntrust = -301,
        FailSubmit = -302,
        FailWithdraw = -303,
        FailQueryEntrust = -304,
        FailQueryDeal = -305,
        FailQueryHolding = -306,
    }
}
