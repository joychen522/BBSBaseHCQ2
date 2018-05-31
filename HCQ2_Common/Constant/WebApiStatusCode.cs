using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common.Constant
{
    /// <summary>
    ///  WebAPI 返回状态码枚举
    /// </summary>
    public enum WebApiStatusCode
    {
        成功 = 0,
        发生异常 = 101,
        认证失败 = 102
    }
    /// <summary>
    ///  WebApi服务返回状态码
    /// </summary>
    public class WebResultCode
    {
        /// <summary>
        ///  成功
        /// </summary>
        public const int Ok = 0;
        /// <summary>
        ///  发生异常
        /// </summary>
        public const int Exception = 101;
        /// <summary>
        ///  认证失败
        /// </summary>
        public const int Error = 102;
    }
}
