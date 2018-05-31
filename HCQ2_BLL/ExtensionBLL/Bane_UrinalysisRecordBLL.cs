using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_BLL
{
    public partial class Bane_UrinalysisRecordBLL:HCQ2_IBLL.IBane_UrinalysisRecordBLL
    {
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUrinalysisRecordUser(BaneUrinalysisRecordModel model)
        {
            if (model == null)
                return false;
            Bane_UrinalysisRecord record = new Bane_UrinalysisRecord
            {
                user_identify = model.user_identify,
                ur_should_date = DateTime.ParseExact(model.ur_should_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")),
                ur_reality_date = Convert.ToDateTime(model.ur_reality_date), //DateTime.ParseExact(model.ur_reality_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")),
                ur_manager = model.ur_manager,
                ur_result = model.ur_result,
                ur_attach = model.ur_attach,
                ur_note = model.ur_note,
                approve_status = model.approve_status,
                ur_site = model.ur_site,
                ur_method = model.ur_method,
                ur_input_date = DateTime.ParseExact(model.ur_input_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN"))
            };
            int mark = DBSession.IBane_UrinalysisRecordDAL.Add(record);
            return mark > 0 ? true : false;
        }
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditUrinalysisRecordUser(BaneUrinalysisRecordModel model)
        {
            if (model == null)
                return false;
            Bane_UrinalysisRecord record = new Bane_UrinalysisRecord
            {
                user_identify = model.user_identify,
                ur_should_date = DateTime.ParseExact(model.ur_should_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")),
                ur_reality_date = Convert.ToDateTime(model.ur_reality_date), //DateTime.ParseExact(model.ur_reality_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")),
                ur_manager = model.ur_manager,
                ur_result = model.ur_result,
                ur_attach = model.ur_attach,
                ur_note = model.ur_note,
                approve_status = model.approve_status,
                ur_site = model.ur_site,
                ur_method = model.ur_method,
                ur_input_date = DateTime.ParseExact(model.ur_input_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN"))
            };
            DBSession.IBane_UrinalysisRecordDAL.Modify(record,s=>s.ur_id==model.ur_id, "ur_should_date", "ur_reality_date", "ur_manager", "ur_result", "approve_status", "ur_note");
            return true;
        }
        /// <summary>
        ///  验证戒毒人员时，自动记录尿检记录
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        public bool AutoAddUrinalysisRecordUser(string user_identify)
        {
            if (string.IsNullOrEmpty(user_identify))
                return false;
            //删除之前已启动 未完成的任务
            Delete(s => s.user_identify == user_identify && s.approve_status == 0);
            BaneAddUser usr = HCQ2UI_Helper.OperateContext.Current.bllSession.Bane_User.GetBaneUser(user_identify);
            Bane_UrinalysisRecord record = new Bane_UrinalysisRecord
            {
                user_identify = user_identify,
                ur_should_date = DateTime.ParseExact(usr.ur_next_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")),//应该尿检时间
                ur_reality_date = DateTime.Now,//实际尿检时间
                approve_status = 0
            };
            int mark = Add(record);
            return mark > 0 ? true : false;
        }
        /// <summary>
        ///  已检人数
        /// </summary>
        /// <returns></returns>
        public int GetDetectionCount(int user_id)
        {
            return DBSession.IBane_UrinalysisRecordDAL.GetDetectionCount(user_id);
        }
        /// <summary>
        ///  获取尿检记录数据
        /// </summary>
        /// <param name="ur_id"></param>
        /// <returns></returns>
        public BaneEditUrinalyRecord GetRecordData(int ur_id)
        {
            if (ur_id <= 0)
                return null;
            return DBSession.IBane_UrinalysisRecordDAL.GetRecordData(ur_id);
        }
    }
}
