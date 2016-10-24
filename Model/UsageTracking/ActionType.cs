using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UsageTracking
{
    public enum ActionType
    {
        //创建对象: 模板, 监控单元, 交易指令等
        Create = 1,
        
        //编辑对象
        Edit = 2,
        
        //删除对象
        Delete = 3,
        
        //获取或查询对象
        Get = 4,

        //保存对象
        Save = 5,

        //授予权限
        Grant = 6,

        //回收权限
        Revoke = 7,

        //提交对象
        Submit = 8,

        //计算对象
        Calc = 9,

        //委托对象
        Entrust = 10,

        //撤销对象
        Cancel = 11,

        //撤补对象
        CancelRedo = 12,
    }
}
