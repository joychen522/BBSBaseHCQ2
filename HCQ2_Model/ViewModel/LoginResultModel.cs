using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    public class LoginResultModel
    {
        /// <summary>
        ///  登录状态true：成功，false：失败
        /// </summary>
        public bool Status { get; set; }
        public EnumModel.LoginEnum.LoginResult Msg { get; set; }
        public string Message { get; set; }
        public HCQ2_Model.T_User user { get; set; }
    }
}
