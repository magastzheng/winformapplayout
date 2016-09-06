using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EnumType
{
    public enum TemplateStatus
    {
        //正常
        Normal = 1,
        //未使用
        Inactive = 2,
    }

    public enum WeightType
    { 
        //数量权重
        AmountWeight = 1,   
        //比例权重
        ProportionalWeight = 2,
    }

    public enum ReplaceType
    { 
        //个股替代
        StockReplace = 0,
        //模板替代
        TemplateReplace = 1,
    }
}
