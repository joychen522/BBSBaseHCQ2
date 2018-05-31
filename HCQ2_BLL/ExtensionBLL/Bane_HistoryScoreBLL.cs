using HCQ2_Model;
using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class Bane_HistoryScoreBLL:HCQ2_IBLL.IBane_HistoryScoreBLL
    {
        /// <summary>
        ///  根据用户身份证获取 本周答题提醒记录数量
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public int GetAnswerNumByID(string user_identify)
        {
            DateTime dt = DateTime.Now;
            int startDate = GetFirstDay(dt.DayOfWeek);
            if (startDate <= 0)
                return 0;
            startDate = 0 - startDate;
            DateTime dtStart = dt.AddDays(startDate);
            return DBSession.IBane_HistoryScoreDAL.GetAnswerNumByID(user_identify, dtStart.ToString("yyyy-MM-dd"));
        }
        /// <summary>
        ///  根据当前星期几 返回星期一需要操作的数
        /// </summary>
        /// <param name="dayWeek"></param>
        /// <returns></returns>
        private int GetFirstDay(DayOfWeek dayWeek)
        {
            DateTime dt = DateTime.Now;
            int startDate = 0;
            switch (dayWeek)
            {
                case DayOfWeek.Thursday: { startDate = dt.Day - 3; } break;
                case DayOfWeek.Friday: { startDate = dt.Day - 4; } break;
                case DayOfWeek.Saturday: { startDate = dt.Day - 5; } break;
                case DayOfWeek.Sunday: { startDate = dt.Day - 6; } break;
            }
            return startDate;
        }



        public int GetAnswerNums(string user_identify)
        {
            DateTime dt = DateTime.Now;
            int startDate = GetFirstDay(dt.DayOfWeek);
            startDate = 0 - startDate;
            DateTime dtStart = dt.AddDays(startDate);
            return DBSession.IBane_HistoryScoreDAL.GetAnswerNumByID(user_identify, dtStart.ToString("yyyy-MM-dd"));
        }
        /// <summary>
        ///  获取完整的每周第一天
        /// </summary>
        /// <param name="dayWeek"></param>
        /// <returns></returns>
        private int GetFirstWorkDay(DayOfWeek dayWeek)
        {
            DateTime dt = DateTime.Now;
            int startDate = 0;
            switch (dayWeek)
            {
                case DayOfWeek.Monday: { startDate = dt.Day; } break;
                case DayOfWeek.Tuesday: { startDate = dt.Day - 1; } break;
                case DayOfWeek.Wednesday: { startDate = dt.Day - 2; } break;
                case DayOfWeek.Thursday: { startDate = dt.Day - 3; } break;
                case DayOfWeek.Friday: { startDate = dt.Day - 4; } break;
                case DayOfWeek.Saturday: { startDate = dt.Day - 5; } break;
                case DayOfWeek.Sunday: { startDate = dt.Day - 6; } break;
            }
            return startDate;
        }

        /// <summary>
        ///  根据用户身份证获取是否需要提醒 答题
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public BaneMyMessageModel GetAnswerContentByID(string user_identify)
        {
            if (GetAnswerNumByID(user_identify) <= 0)
                return null;
            DateTime dt = DateTime.Now;
            int startDate = GetFirstDay(dt.DayOfWeek);
            if (startDate <= 0)
                return null;
            startDate = 0 - startDate;
            DateTime dtStart = dt.AddDays(startDate), dtEnd = dtStart.AddDays(6);
            return new BaneMyMessageModel
            {
                mess_title = "答题提醒",
                send_name = "系统管理员",
                send_date = dt.ToString("yyyy-MM-dd"),
                mess_content = "您应于" + dtStart.ToString("D") + "-" + dtEnd.ToString("D") + "登录移动端做在线答题，答题得分90分以上才为合格。"
            };
        }
        /// <summary>
        ///  获取用户历史答题记录
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public List<AnswerHistoryResultModel> GetAnswerHistoryList(string user_identify)
        {
            return DBSession.IBane_HistoryScoreDAL.GetAnswerHistoryList(user_identify);
        }
        /// <summary>
        ///  根据答题历史记录ID 获取详细的答题记录
        /// </summary>
        /// <param name="answer_id"></param>
        /// <returns></returns>
        public AnswerResultModel GetAnswerHistoryDetial(int answer_id)
        {
            AnswerResultModel model = new AnswerResultModel();
            //1. 历史答题分数记录
            Bane_HistoryScore hscore = Select(s => s.hs_id == answer_id).FirstOrDefault();
            if (hscore == null)
                return null;
            model.answer_date = hscore.hs_time.ToString("D");
            model.answer_score = hscore.hs_score;
            model.answer_total = hscore.hs_total;
            //2. 历史答题详细记录
            List<Bane_HistoryScoreDetial> sDetial = DBSession.IBane_HistoryScoreDetialDAL.Select(s => s.hs_id == answer_id);
            List<int> sub_ids = sDetial.Select(s => s.sub_id).ToList();
            //3. 试题库
            List<Bane_QuestionInfo> qInfo = DBSession.IBane_QuestionInfoDAL.GetAnswerByOptions(sub_ids);
            //4. 选项库
            List<HCQ2_Model.Bane_QuestionValue> qValue = DBSession.IBane_QuestionValueDAL.GetOptionsById(sub_ids);
            List<AnswerResultDetial> issue = new List<AnswerResultDetial>();
            foreach (var item in qInfo)
            {
                //4.1 选择项
                List<HCQ2_Model.BaneUser.APP.Result.Bane_QuestionValue> option = new List<HCQ2_Model.BaneUser.APP.Result.Bane_QuestionValue>();
                var valueTemp = qValue.Where(s => s.sub_id == item.sub_id).ToList();
                foreach(var obj in valueTemp)
                    option.Add(new HCQ2_Model.BaneUser.APP.Result.Bane_QuestionValue {score_option=obj.score_option,score_value=obj.score_value });
                //4.2 试题
                var query = sDetial.Where(s => s.sub_id == item.sub_id).FirstOrDefault();
                issue.Add(new AnswerResultDetial { issue_title = item.sub_title, correct_option = query.sub_value, user_option = query.hd_value, option = option });
            }
            model.issue = issue;
            return model;
        }
    }
}
