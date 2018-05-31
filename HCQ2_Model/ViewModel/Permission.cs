using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ViewModel
{
    public class Permission
    {
        [DisplayName("ID")]
        public int access_id { get; set; }
        [DisplayName("模块名")]
        public string module_name { get; set; }
        [DisplayName("folder_id")]
        public int folder_id { get; set; }
        [DisplayName("权限代码")]
        public string access_code { get; set; }
        [DisplayName("权限值")]
        public string access_value { get; set; }
        [DisplayName("用户ID")]
        public int user_id { get; set; }
        [DisplayName("用户名")]
        public string user_name { get; set; }
        [DisplayName("user_type_id")]
        public int user_type_id { get; set; }
        [DisplayName("单位代码")]
        public string unit_code { get; set; }
    }
}
