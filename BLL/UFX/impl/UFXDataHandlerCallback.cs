using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.UFX.impl
{
    /// <summary>
    /// Call the callback in UFX interface after receiving the data.
    /// </summary>
    /// <param name="token">The raw input parameter setting while calling UFX BLL interface.</param>
    /// <param name="dataParser"></param>
    /// <returns></returns>
    public delegate int UFXDataHandler(CallerToken token, DataParser dataParser);

    /// <summary>
    /// The caller can set the callback in the UFX input parameter. Then it will be called in the
    /// UFX callback.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public delegate int CallerCallback(CallerToken token, object data, UFXErrorResponse errorResponse);

    /// <summary>
    /// 
    /// </summary>
    public class UFXErrorResponse
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string MessageDetail { get; set; }

        public int DataCount { get; set; }
    }

    /// <summary>
    /// The CallerToken can save the input information. Then the callback can use the information to finish
    /// the following process.
    /// </summary>
    public class CallerToken
    {
        public int SubmitId { get; set; }

        public int CommandId { get; set; }

        public object InArgs { get; set; }

        public object OutArgs { get; set; }

        public EventWaitHandle WaitEvent { get; set; }

        public CallerCallback Caller { get; set; }
    }

    /// <summary>
    /// The Callbacker wrap is used to register in the UFX interface.
    /// </summary>
    public class Callbacker
    {
        public CallerToken Token { get; set; }
        public UFXDataHandler DataHandler { get; set; }
    }
}
