using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HCQ2_Common.Bean
{
    /// <summary>
    ///  Session 帮助类
    ///  创建时间：2016-11-11
    ///  创建人：Joychen
    /// </summary>
    public class SessionHelper
    {
        private static string SESSION_USER = "SESSION_USER";
        /// <summary>
        ///  写入session
        /// </summary>
        /// <param name="obj"></param>
        public static void AddSessionValue(object obj)
        {
            HttpContext context = HttpContext.Current;
            context.Session[SESSION_USER] = obj;
        }
        /// <summary>
        ///  写入session
        /// </summary>
        /// <param name="SESSION_NAME">session名称</param>
        /// <param name="DATA">值</param>
        public static void AddSessionValue(string SESSION_NAME,object DATA)
        {
            if (string.IsNullOrEmpty(SESSION_NAME))
                return;
            HttpContext context = HttpContext.Current;
            context.Session[SESSION_NAME] = DATA;
        }
        /// <summary>
        ///  读取session
        /// </summary>
        /// <returns></returns>
        public static object GetSessionValue()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[SESSION_USER];
        }
        /// <summary>
        ///  读取指定session
        /// </summary>
        /// <returns></returns>
        public static object GetSessionValue(string SESSION_NAME)
        {
            if (string.IsNullOrEmpty(SESSION_NAME))
                return null;
            HttpContext context = HttpContext.Current;
            return context.Session[SESSION_NAME];
        }
        /// <summary>
        ///  删除指定session
        /// </summary>
        /// <param name="sessionName"></param>
        public static void RemoveSession(string sessionName)
        {
            HttpContext context = HttpContext.Current;
            context.Session.Remove(sessionName);
        }

        /// <summary>
        ///  删除所有session
        /// </summary>
        public static void RemoveAllSession()
        {
            HttpContext context = HttpContext.Current;
            context.Session.RemoveAll();
        }
    }
}
