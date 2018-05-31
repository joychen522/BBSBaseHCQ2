using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.APPModel.ResultApiModel
{
    public class LoginAPPResultModel
    {
        public string userid { get; set; }
        /// <summary>
        ///  组织结构代码
        ///  成功登录后下发，用于每次请求数据作用范围为本组织下
        /// </summary>
        public string user_type { get; set; }
    }

    public class AppUserModel
    {
        /// <summary>
        ///  身份证
        /// </summary>
        public string user_identify { get; set; }
        /// <summary>
        ///  电话
        /// </summary>
        public string user_phone { get; set; }
        /// <summary>
        ///  用户名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        ///  登录名
        /// </summary>
        public string login_name { get; set; }
    }
}
