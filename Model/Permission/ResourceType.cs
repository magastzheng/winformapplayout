using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Permission
{
    /// <summary>
    /// 定义资源类型是为了限权控制，通过(资源类型,资源ID)组合就可以唯一确定一个资源。
    /// 同时，可以通过授予用户不同的角色，并对角色开放不同资源类型的访问权限，就可以
    /// 达到对角色权限控制的目的
    /// </summary>
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
        //交易实例调整
        TradeInstanceAdjustment = 114,
        //监控单元
        MonitorUnit = 115,
        //交易指令
        TradeCommand = 121,
        //委托指令
        EntrustCommand = 122,
    }
}
