using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    public enum ResourceType
    {
        //无效
        None = -1,
        //插件
        Plugin  = 1,    
        //功能
        Feature = 2,    
        //基金
        Product = 101,  
        //资产单元
        AssetUnit = 102,    
        //投资组合
        Portfolio = 103,  
        //现货模板
        SpotTemplate = 111,
        //历史现货模板
        HistoricalSpotTemplate = 112,
        //交易实例
        TradeInstance = 113,    
        //交易指令
        TradeCommand = 121,     
        //委托指令
        EntrustCommand = 122, 
    }
}
