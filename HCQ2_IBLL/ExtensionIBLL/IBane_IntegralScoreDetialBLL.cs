using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  积分详情业务层接口
    /// </summary>
    public partial interface IBane_IntegralScoreDetialBLL
    {
        /// <summary>
        ///  获取用户获取积分详情
        /// </summary>
        /// <param name="user_identify">身份证</param>
        /// <returns></returns>
        AnswerIntegralModel GetIntegralScoreById(string user_identify);
    }
}
