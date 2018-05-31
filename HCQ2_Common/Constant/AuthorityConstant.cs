using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common.Constant
{
    /// <summary>
    ///  WebAPI授权验证常量
    /// </summary>
    public class AuthorityConstant
    {
        /// <summary>
        ///  加密签名
        /// </summary>
        public const string MATCH_SIGNATURE = "match_signature";
        /// <summary>
        ///  时间戳
        /// </summary>
        public const string MATCH_TIMESTAMP = "match_timestamp";
        /// <summary>
        ///  1-20数字随机字符串
        /// </summary>
        public const string MATCH_NONCE = "match_nonce";
        /// <summary>
        ///  握手密码
        /// </summary>
        public const string SECRET_KEY = "R9WxY+1da1hr@ke2y1ok";
        /// <summary>
        ///  用户编码，登录后获取
        /// </summary>
        public const string USER_ID = "userid";
    }

}
