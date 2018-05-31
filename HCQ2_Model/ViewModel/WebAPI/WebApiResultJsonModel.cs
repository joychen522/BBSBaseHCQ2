using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel.WebAPI
{
    /// <summary>
    ///  WebApi返回模型
    /// </summary>
    public class WebApiResultJsonModel
    {
        /// <summary>
        ///  返回编码
        /// 0：成功
        /// 101：发生异常
        /// 102：认证失败
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        ///  返回提示消息
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        ///  返回数据
        /// </summary>
        public object value { get; set; }
    }
}
