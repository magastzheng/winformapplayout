using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace WAPIWrapperCSharp
{
    public class WindAPIWrap : IDisposable
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly static WindAPIWrap _instance = new WindAPIWrap();
        private WindAPI _windAPI = new WindAPI();

        public static WindAPIWrap Instance { get { return _instance; } }

        private WindAPIWrap()
        {
        }

        public int Start()
        {
            int ret = _windAPI.start("", "", 5000);
            if (ret == 0)
            {
                string msg = "WindAPI启动成功!";
                Console.WriteLine(msg);
                logger.Info(msg);
            }
            else
            {
                string msg = "WindAPI启动失败!";
                Console.WriteLine(msg);
                logger.Info(msg);
            }

            return ret;
        }

        private void Callback(ulong reqId, WindData wd)
        { 
            //Fill data here
            Console.WriteLine(reqId);
        }

        public ulong RequestData(ref int errCode, List<string> secuCodes, List<string> fields, Dictionary<string, string> options, bool updateAll, WindCallback cb)
        { 
            string strCodes = string.Join(",", secuCodes);
            string strFields = string.Join(",", fields);
            string strOptions = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach(var kv in options)
            {
                sb.AppendFormat("{0}={1};", kv.Key, kv.Value);
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            strOptions = sb.ToString();

            if (cb == null)
            {
                cb = new WindCallback(Callback);
            }

            return _windAPI.wsq(ref errCode, strCodes, strFields, strOptions, cb, updateAll);
        }

        public WindData SyncRequestData(List<string> secuCodes, List<string> fields, Dictionary<string, string> options)
        {
            string strCodes = string.Join(",", secuCodes);
            string strFields = string.Join(",", fields);
            string strOptions = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var kv in options)
            {
                sb.AppendFormat("{0}={1};", kv.Key, kv.Value);
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            strOptions = sb.ToString();

            return _windAPI.wsq(strCodes, strFields, strOptions);
        }

        public void CancelRequest(ulong reqId)
        {
            _windAPI.cancelRequest(reqId);
        }

        public void Dispose()
        {
            _windAPI.stop();
        }
    }
}
