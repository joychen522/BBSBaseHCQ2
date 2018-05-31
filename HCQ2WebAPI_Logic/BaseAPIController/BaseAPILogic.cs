using System.Web;
using System.Web.Http;
using HCQ2UI_Helper;

namespace HCQ2WebAPI_Logic
{
    //[MyApiAuthorizeFilter]
    [DExceptionFilter]
    public class BaseApiLogic:ApiController
    {
        /// <summary>
        /// 获取操作上下文对象
        /// </summary>
        protected OperateContext operateContext
        {
            get { return OperateContext.Current; }
        }
        /// <summary>
        ///  请求上下文
        /// </summary>
        protected HttpRequest request
        {
            get { return HttpContext.Current.Request; }
        }
    }
}
