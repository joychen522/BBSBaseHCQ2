using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.BaneUser.APP.Params
{
    public class BaseBaneModel
    {
        /// <summary>
        ///  guid
        /// </summary>
        [DisplayName("guid")]
        [Required]
        public string userid { get; set; }
    }
}
