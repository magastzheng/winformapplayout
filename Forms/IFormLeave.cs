using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms
{
    /// <summary>
    /// 离开或切换窗体时（窗体从激活状态变为非激活状态）触发，用来处理一些清理工作，比如保存数据、状态更新等
    /// </summary>
    public interface IFormLeave
    {
        bool OnFormLeave(object sender, object data);
    }
}
