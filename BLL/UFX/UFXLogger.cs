using BLL.UFX.impl;
using log4net;
using Model;
using Model.UFX;

namespace BLL.UFX
{
    public class UFXLogger
    {
        public static void Error(ILog logger, UFXFunctionCode functionCode, string info)
        {
            string msg = string.Format("功能号: [{0}], {1}", (int)functionCode, info);
            logger.Error(msg);
        }

        public static void Error(ILog logger, UFXFunctionCode functionCode, UFXErrorResponse err)
        {
            string msg = string.Format("功能号: [{0}], 错误码: {1}, 消息: {2}", (int)functionCode, err.ErrorCode, err.ErrorMessage);
            logger.Error(msg);
        }

        public static void Info(ILog logger, UFXFunctionCode functionCode, string info)
        {
#if DEBUG
            string msg = string.Format("功能号: [{0}], {1}", (int)functionCode, info);
            logger.Info(msg);
#endif
        }

        public static void Info(ILog logger, string info)
        { 
#if DEBUG
            logger.Info(info);
#endif
        }
    }
}
