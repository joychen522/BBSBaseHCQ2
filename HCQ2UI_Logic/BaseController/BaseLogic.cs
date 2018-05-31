using System.Web;
using System.Web.Mvc;
using HCQ2UI_Helper;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  控制器父类
    /// </summary>
    public class BaseLogic: Controller
    {
        /// <summary>
        /// 获取操作上下文对象
        /// </summary>
        protected OperateContext operateContext
        {
            get { return OperateContext.Current; }
        }
        protected HttpRequestBase request
        {
            get { return Request; }
        }
    }
}
