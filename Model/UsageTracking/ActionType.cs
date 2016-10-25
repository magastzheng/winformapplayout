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

        //查看权限
        CheckPermission = 6,

        //授予权限
        GrantPermission = 7,

        //回收权限
        RevokePermission = 8,

        //改变权限
        EditPermission = 9,

        //提交对象
        Submit = 10,

        //计算对象
        Calc = 11,

        //委托对象
        Entrust = 12,

        //撤销对象
        Cancel = 13,

        //撤补对象
        CancelRedo = 14,
    }
}
