using HCQ2_Model;
using HCQ2_Model.BaneUser.APP.Params;
using HCQ2_Model.BaneUser.APP.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class Bane_QuestionInfoBLL:HCQ2_IBLL.IBane_QuestionInfoBLL
    {
        /// <summary>
        ///  后端获取一栏试题数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<Bane_QuestionInfo> GetAllAnswerQuestion(string key, int page, int size)
        {
            if (!string.IsNullOrEmpty(key))
                return Select(s => s.sub_title.Contains(key),s=>s.sub_id,page,size,true);
            return Select(s => (!string.IsNullOrEmpty(s.sub_title)), s => s.sub_id).Skip((page - 1) * size).Take(size).ToList();
        }
        /// <summary>
        ///  添加试题
        /// </summary>
        /// <param name="model"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public int AddAnswerQuestion(Bane_QuestionInfo model, string options)
        {
            //1. 添加试题
            if (model == null)
                return 0;
            model.sub_date = DateTime.Now;
            Add(model);
            //2. 添加选项
            string[] options_value = options.Trim('∭').Split('∭');//题目之间
            for (int i = 0; i < options_value.Length; i++)
            {
                string[] str = options_value[i].Split('∬');//标题之间
                DBSession.IBane_QuestionValueDAL.Add(new HCQ2_Model.Bane_QuestionValue { sub_id = model.sub_id, score_option = str[0].ToString().ToUpper(), score_value = str[1].ToString() });
            }
            return 1;
        }
        /// <summary>
        ///  编辑试题
        /// </summary>
        /// <param name="model"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public int EditAnswerQuestion(Bane_QuestionInfo model, string options)
        {
            //1. 更新试题
            Modify(model, s => s.sub_id == model.sub_id, "sub_title", "sub_value", "sub_score", "sub_note", "sub_essay");
            //2. 更新选项
            string[] options_value = options.Trim('∭').Split('∭');//题目之间
            for (int i = 0; i < options_value.Length; i++)
            {
                string[] str = options_value[i].Split('∬');//标题之间
                string[] head = str[0].Split('∫');
                if (string.IsNullOrEmpty(head[0].Trim()) || head[0] == "0")
                    DBSession.IBane_QuestionValueDAL.Add(new HCQ2_Model.Bane_QuestionValue { sub_id = model.sub_id, score_option = head[1].ToString().ToUpper(), score_value = str[1].ToString() });
                else
                {
                    int score_id = Convert.ToInt32(head[0].Trim());
                    DBSession.IBane_QuestionValueDAL.Modify(new HCQ2_Model.Bane_QuestionValue { score_option = head[1].ToString().ToUpper(), score_value = str[1].ToString() }, s => s.score_id == score_id, "score_option", "score_value");
                } 
            }
            return 1;
        }

        //******************************************APP*************************************
        /// <summary>
        ///  随机获取题目
        /// </summary>
        /// <param name="howLen">随机几条</param>
        /// <returns></returns>
        public List<BaneTopicModel> GetAnswerQuestion(int howLen)
        {
            //1. 判断试题库是否有howLen 到试题
            List<BaneTopicModel> listAnswer = DBSession.IBane_QuestionInfoDAL.GetAnswerQuestion(howLen);
            if (listAnswer == null || listAnswer.Count < howLen)
                return null;
            List<int> sub_id = listAnswer.Select(s => s.sub_id).ToList();
            List<HCQ2_Model.Bane_QuestionValue> AnswerValue = DBSession.IBane_QuestionValueDAL.GetOptionsById(sub_id);
            if (AnswerValue == null || AnswerValue.Count <= 0)
                return null;
            foreach(var item in listAnswer)
            {
                var query = AnswerValue.Where(s => s.sub_id == item.sub_id);
                List<BaneTopicDetial> list = new List<BaneTopicDetial>();
                foreach (var val in query)
                    list.Add(new BaneTopicDetial { score_option = val.score_option, score_value = val.score_value });
                item.topic = list;
            }
            return listAnswer;
        }

        /// <summary>
        ///  批阅试题 记录积分 并返回分数
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public int CheckAnswer(List<SubmitAnswerDetail> options, string user_identify, out int core)
        {
            List<Bane_QuestionInfo> info = DBSession.IBane_QuestionInfoDAL.GetAnswerByOptions(options);
            //1. 历史答题详细记录
            List<Bane_HistoryScoreDetial> scoreDetial = new List<Bane_HistoryScoreDetial>();
            int count_num = 0;
            foreach (var item in options)
            {
                var query = info.Where(s => s.sub_id == item.sub_id).FirstOrDefault();
                if (item.score_option.ToLower() == query.sub_value.ToLower())
                    count_num += query.sub_score;
                scoreDetial.Add(new Bane_HistoryScoreDetial {
                    hs_id=0,sub_id=item.sub_id,hd_value=item.score_option,sub_value=query.sub_value
                });
            }
            //2. 判断用户分数>90 是否获得积分，一周内只能第一次获得积分
            string hs_title = "答题获得积分";
            int mark = HCQ2UI_Helper.OperateContext.Current.bllSession.Bane_HistoryScore.GetAnswerNums(user_identify),jifen=1;
            if (mark > 0 || count_num < 90)
            {
                hs_title = "";
                jifen = 0;
            }
            core = jifen;
            //3. 添加 历史答题分数记录
            Bane_HistoryScore hScore = new Bane_HistoryScore
            {
                user_identify = user_identify,
                hs_time = DateTime.Now,
                hs_score = count_num,
                hs_title = hs_title,
                hs_total = jifen
            };
            DBSession.IBane_HistoryScoreDAL.Add(hScore);
            //4. 添加 历史答题详细记录
            foreach (var item in scoreDetial)
            {
                item.hs_id = hScore.hs_id;
                DBSession.IBane_HistoryScoreDetialDAL.Add(item);
            }
            //5. 是否记录积分库
            if (jifen > 0)
                DBSession.IBane_IntegralScoreDetialDAL.Add(new Bane_IntegralScoreDetial { user_identify = user_identify, his_title = hs_title, his_time = DateTime.Now, his_total = jifen });
            return count_num;//分数
        }
    }
}
