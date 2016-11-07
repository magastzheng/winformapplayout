
namespace Model.EnumType
{
    public enum CommandStatus
    {
        //有效指令
        Effective = 1,
        //已修改
        Modified = 2,
        //已撤销
        Canceled = 3,
        //委托完成
        Entrusted = 4,
        //已完成成交
        Done = 5,
    }
}
