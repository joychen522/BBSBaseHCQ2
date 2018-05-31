using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    /// <summary>
    ///  Web Api服务基本模型
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        ///  用户内部编码，成功登录后下发32位guid
        ///  用于验证是否登录用户
        /// </summary>
        [DisplayName("用户内部编码")]
        [Required]
        public string userid { get; set; }
        /// <summary>
        ///  组织结构代码
        ///  成功登录后下发，用于每次请求数据作用范围为本组织下
        /// </summary>
        //[DisplayName("组织结构代码")]
        //[Required]
        public string orgid { get; set; }
    }
}
