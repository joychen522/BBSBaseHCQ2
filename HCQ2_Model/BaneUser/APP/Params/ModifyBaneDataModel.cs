using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Params
{
    /// <summary>
    ///  修改密码
    /// </summary>
    public class ModifyBaneDataModel:BaseBaneModel
    {
        /// <summary>
        ///  旧密码
        /// </summary>
        [DisplayName("旧密码")]
        [Required]
        public string old_pwd { get; set; }
        /// <summary>
        ///  新密码
        /// </summary>
        [DisplayName("新密码")]
        [Required]
        public string new_pwd { get; set; }
    }
}
