using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class Bane_IntegralScoreDetialBLL:HCQ2_IBLL.IBane_IntegralScoreDetialBLL
    {
        /// <summary>
        ///  获取用户获取积分详情
        /// </summary>
        /// <param name="user_identify">身份证</param>
        /// <returns></returns>
        public AnswerIntegralModel GetIntegralScoreById(string user_identify)
        {
            AnswerIntegralModel model = new AnswerIntegralModel();
            model.num_count = 0;
            if (string.IsNullOrEmpty(user_identify))
                return model;
            List<AnswerIntegralDetial> list = DBSession.IBane_IntegralScoreDetialDAL.GetIntegralScoreById(user_identify);
            if (list == null || list.Count <= 0)
                return model;
            model.num_count = list.Sum(s => s.hs_total);
            model.interral = list;
            return model;
        }
    }
}
