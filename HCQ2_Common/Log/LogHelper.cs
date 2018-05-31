using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Common.Log
{
    /// <summary>
    ///  日志记录
    /// </summary>
    public class LogHelper : IDisposable
    {
        #region 0.0 参数
        /// <summary>
        ///  调用的类
        /// </summary>
        private static readonly Type T = MethodBase.GetCurrentMethod().DeclaringType;
        /// <summary>
        ///  Ilog对象
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(T);
        #endregion

        #region 1.0 错误日志
        #region 1.1 void ErrorLog(Type t, Exception ex)
        /// <summary>
        ///  记录错误日志
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void ErrorLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            if (log.IsErrorEnabled)
                log.Error(ex);
        }
        #endregion

        #region 1.2 void ErrorLog(Type t, string meg)
        /// <summary>
        ///  记录错误日志
        /// </summary>
        /// <param name="t"></param>
        /// <param name="meg"></param>
        public static void ErrorLog(Type t, string meg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(meg);
        }
        #endregion

        #region 1.3 void ErrorLog(Exception ex)
        public static void ErrorLog(Exception ex)
        {
            Log.Error(ex);
        }
        #endregion

        #region 1.4 void ErrorLog(string meg)
        public static void ErrorLog(string meg)
        {
            Log.Error(meg);
        }
        #endregion
        #endregion

        #region 2.0 操作日志
        #region 2.1 操作日志 +void InfoLog(Type t, string mes)
        /// <summary>
        ///  操作日志
        /// </summary>
        /// <param name="t"></param>
        /// <param name="mes"></param>
        public static void InfoLog(Type t, string mes)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info(mes);
        }
        #endregion

        #region 2.2 操作日志 +void InfoLog(Type t, Exception ex)
        /// <summary>
        ///  操作日志
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void InfoLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info(ex);
        }
        #endregion

        #region 2.3 void InfoLog(Exception ex)
        public static void InfoLog(Exception ex)
        {
            Log.Info(ex);
        }
        #endregion

        #region 2.4 void InfoLog(string mes)
        public static void InfoLog(string mes)
        {
            Log.Info(mes);
        }
        #endregion
        #endregion

        #region 3.0 调试日志
        #region 3.1 void DebugLog(Type t, Exception mes)
        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="t"></param>
        /// <param name="mes"></param>
        public static void DebugLog(Type t, Exception mes)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Debug(mes);
        }
        #endregion

        #region 3.2 void DebugLog(Type t, string mes)
        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="t"></param>
        /// <param name="mes"></param>
        public static void DebugLog(Type t, string mes)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Debug(mes);
        }
        #endregion

        #region 3.3 void DebugLog(Exception e)
        public static void DebugLog(Exception e)
        {
            Log.Error(e);
        }
        #endregion

        #region 3.4 void DebugLog(string e)
        public static void DebugLog(string e)
        {
            Log.Error(e);
        }
        #endregion
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
