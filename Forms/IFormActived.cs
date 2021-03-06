﻿
namespace Forms
{
    /// <summary>
    /// 加载窗体时（构建或激活后），触发一个通知窗体的事件，方便传递相关参数到目标窗体。
    /// </summary>
    public interface IFormActived
    {
        /// <summary>
        /// 窗体激活的事件处理
        /// </summary>
        /// <param name="json">传递的参数内容，自定义JSON格式</param>
        void OnFormActived(string json);
    }
}
