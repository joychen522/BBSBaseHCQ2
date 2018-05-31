using System;
using System.Web;

namespace HCQ2_Common
{
    /// <summary>
    ///  request帮助类
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        ///  获取Request对象
        /// </summary>
        static HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        /// <summary>
        ///  获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP
        {
            get
            {
                string iP = string.Empty;
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_VIA"]))
                    iP = Convert.ToString(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
                if (string.IsNullOrEmpty(iP))
                    iP = Convert.ToString(Request.ServerVariables["REMOTE_ADDR"]);
                return iP;
            }
        }

        /// <summary>
        ///  获取浏览器
        /// </summary>
        /// <returns></returns>
        public static string GetBrowser
        {
            get
            {
                return Request.Browser.Browser;
            }
        }

        /// <summary>
        ///  浏览器版本
        /// </summary>
        /// <returns></returns>
        public static string GetBroMaVersion
        {
            get
            {
                return Request.Browser.MajorVersion.ToString();
            }
        }

        /// <summary>
        ///  获取操作系统
        /// </summary>
        /// <returns></returns>
        public static string GetPlatform
        {
            get
            {
                return Request.Browser.Platform;
            }
        }

        /// <summary>
        ///  获取请求地址
        /// </summary>
        /// <returns></returns>
        public static string GetUrl
        {
            get
            {
                return Request.Url.ToString();
            }
        }

        /// <summary>
        ///  通过request获取string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetStrByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            return Helper.ToString(Request[name]);
        }

        /// <summary>
        ///  通过request获取string
        ///  需要解码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDeStrByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            return HttpUtility.UrlDecode(Helper.ToString(Request[name]));
        }

        /// <summary>
        ///  通过request获取int
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetIntByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 0;
            string str = Request[name];
            if (string.IsNullOrEmpty(str))
                return 0;
            return Helper.ToInt(str);
        }
    }
}
