using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.WeiXinApiModel.ParamModel
{
    public class BaseWeiXinModel
    {
        /// <summary>
        ///  用户内部编码，成功登录后下发32位guid
        ///  用于验证是否登录用户
        /// </summary>
        [DisplayName("用户内部编码")]
        [Required]
        public string userid { get; set; }

        /// <summary>
        ///  显示第几页
        /// </summary>
        [DisplayName("第几页")]
        //[Range(10,20)]//取10-20这个范围
        [RegularExpression("^([1-9][0-9]*)$")]//大于1正则表达式
        public int page { get; set; } = 1;//默认显示第一页

        /// <summary>
        ///  每页记录数量
        /// </summary>
        [DisplayName("每页记录数量")]
        [RegularExpression("^([1-9][0-9]*)$")]//大于1正则表达式
        public int size { get; set; } = 10;//默认显示前10条
    }
}
