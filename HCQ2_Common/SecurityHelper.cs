using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace HCQ2_Common
{
    /// <summary>
    ///  360 安全助手
    /// </summary>
    public class SecurityHelper
    {
        #region 采用 票据对象 方式加密对象
        /// <summary>
        ///  采用 票据对象 方式加密对象
        /// </summary>
        /// <param name="userInfo">待加密字符串</param>
        /// <returns></returns>
        public static string EncryUserInfo(string userInfo)
        {
            //1.0 将数据 存入 票据对象
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "哈哈", DateTime.Now, DateTime.Now, true,
                userInfo);
            //1.2 将票据对象 加密成字符串
            string dataInfo = FormsAuthentication.Encrypt(ticket);
            return dataInfo;
        }
        #endregion

        #region 采用 票据对象 方式解密对象
        /// <summary>
        ///  采用 票据对象 方式加密对象
        /// </summary>
        /// <param name="userInfo">待加密字符串</param>
        /// <returns></returns>
        public static string DecryUserInfo(string DecryInfo)
        {
            //1.0 将数据 存入 票据对象
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(DecryInfo);
            return ticket.UserData;
        }
        #endregion
    }
}
