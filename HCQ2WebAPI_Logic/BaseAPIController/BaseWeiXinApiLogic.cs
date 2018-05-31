using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HCQ2UI_Helper;

namespace HCQ2WebAPI_Logic.BaseAPIController
{
    /// <summary>
    ///  描述：微信端控制器 父类
    ///  创建人：Joychen
    ///  创建时间：2017-04-27
    /// </summary>
    [WeiXinApiAuthorizeFilter]
    [DExceptionFilter]
    public class BaseWeiXinApiLogic: ApiController
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
