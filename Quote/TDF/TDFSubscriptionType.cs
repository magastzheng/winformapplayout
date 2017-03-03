using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.TDF
{
    public enum TDFSubscriptionType
    {
        //增加新证券的订阅
        Add = 1,

        //删除订阅证券
        Delete,

        //改变证券订阅设置
        Set,

        //订阅所有
        Full,
    }
}
