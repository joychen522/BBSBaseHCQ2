using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HCQ2_Common.Bean
{
    /// <summary>
    ///  Cookie帮助类
    /// </summary>
    public class CookieHelper
    {
        #region 添加Cookie
        /// <summary>
        ///  将数据 添加 Cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="Path">区域路径</param>
        /// <param name="Exptime">过期时间 以天计算</param>
        public static void addCookies(string cookieName, string cookieValue , string Path = null,int Exptime = 1)
        {
            if (string.IsNullOrEmpty(cookieName))
                return;
            HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
            cookie.Expires = DateTime.Now.AddDays(1);
            if (!string.IsNullOrEmpty(Path))
                cookie.Path = Path;
        } 
        #endregion

        #region  获取Cookie数据
        /// <summary>
        ///  获取Cookie数据
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        /// <returns></returns>
        public static string getCookieValue(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
                return null;
            return cookie.Value;
        }
        #endregion
    }
}
