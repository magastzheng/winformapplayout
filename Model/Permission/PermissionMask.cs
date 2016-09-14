
namespace Model.Permission
{
    /// <summary>
    /// Each bit represents one rights. The bit value 1 means having rights, 0 means no rights.
    /// BIT: 6      5       4       3       2       1       0
    ///     Execute View    Query   Edit    Delete  Add     Owner
    /// </summary>
    public enum PermissionMask
    {
        Owner   = 1,    //拥有者一般为创建者
        Add     = 2,    //创建、添加
        Delete  = 4,    //删除
        Edit    = 8,    //修改
        Veiw    = 16,   //浏览
        Execute = 32,   //执行命令
        Query   = 64,   //???
    }
}
