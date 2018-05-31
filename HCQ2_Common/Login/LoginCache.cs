using System.Collections;
using System.Web;

namespace HCQ2_Common.Login
{
    /// <summary>
    ///  用于控制一个账号，同一时间只能在一个地点登录
    /// </summary>
    public class LoginCache
    {
        /// <summary>
        ///  Cache登录后执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loginName"></param>
        public static void SetCheckCacheLogin(string loginName)
        {
            
            if (string.IsNullOrEmpty(loginName))
                return;
            string strValue = Helper.ToString(CacheHelper.GetCacheValue(loginName));//获取sessionid
            if (string.IsNullOrEmpty(strValue) || (!string.IsNullOrEmpty(strValue) && !HttpContext.Current.Session.SessionID.Equals(strValue)))
                CacheHelper.SetCacheValue(loginName, HttpContext.Current.Session.SessionID);
        }
        /// <summary>
        ///  退出登录
        /// </summary>
        /// <param name="loginName"></param>
        public static void ExitCacheLogin(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
                return;
            string strValue = Helper.ToString(CacheHelper.GetCacheValue(loginName));//获取sessionid
            if (!string.IsNullOrEmpty(strValue))
                CacheHelper.RemoveCache(loginName);
        }
        /// <summary>
        ///  判断是否登录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loginName"></param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public static bool IsCacheLogin(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
                return false;
            string strValue = Helper.ToString(CacheHelper.GetCacheValue(loginName));//获取sessionid
            if (string.IsNullOrEmpty(strValue))
                return false;
            if (HttpContext.Current.Session.SessionID.Equals(strValue))
                return true;
            HttpContext.Current.Response.Write("<script>alert('您的帐号已在：" + RequestHelper.GetIP +
                                   " 登录，您已被迫下线！');window.location.href='"+ HttpContext.Current.Request.ApplicationPath+ "/SysLogin/Login'</script>");//退出当前到登录页面
            HttpContext.Current.Response.End();
            return false;
        }
    }
}
