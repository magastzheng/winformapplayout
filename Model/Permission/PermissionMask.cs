
namespace Model.Permission
{
    public enum PermissionMask
    {
        Owner   = 1,
        Add  = 2,       //创建、添加
        Delete  = 4,    //删除
        Edit    = 8,    //修改
        Query   = 16,   
        View    = 32,
        Execute = 64,   //执行命令
    }
}
