using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_Model;

namespace HCQ2_BLL
{
    public partial class Bane_FamilyRecordBLL:HCQ2_IBLL.IBane_FamilyRecordBLL
    {
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddFamilyRecordUser(BaneFamilyRecordModel model)
        {
            if (model == null)
                return false;
            Bane_FamilyRecord record = new Bane_FamilyRecord
            {
                user_identify = model.user_identify,
                fr_name=model.fr_name,
                fr_sex=model.fr_sex,
                fr_birth= (!string.IsNullOrEmpty(model.fr_birth)) ? DateTime.ParseExact(model.fr_birth, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null,
                fr_edu = model.fr_edu,
                fr_family_url = model.fr_family_url,
                fr_job = model.fr_job,
                fr_unit = model.fr_unit,
                fr_relation = model.fr_relation,
                fr_phone = model.fr_phone,
                fr_type = model.fr_type
            };
            int mark = DBSession.IBane_FamilyRecordDAL.Add(record);
            return mark > 0 ? true : false;
        }
        /// <summary>
        ///  编辑对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditFamilyRecordUser(BaneFamilyRecordModel model)
        {
            Bane_FamilyRecord record = new Bane_FamilyRecord
            {
                user_identify = model.user_identify,
                fr_name = model.fr_name,
                fr_sex = model.fr_sex,
                fr_birth = (!string.IsNullOrEmpty(model.fr_birth)) ? DateTime.ParseExact(model.fr_birth, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null,
                fr_edu = model.fr_edu,
                fr_family_url = model.fr_family_url,
                fr_job = model.fr_job,
                fr_unit = model.fr_unit,
                fr_relation = model.fr_relation,
                fr_phone = model.fr_phone,
                fr_type = model.fr_type
            };
            DBSession.IBane_FamilyRecordDAL.Modify(record,s=>s.fr_id==model.fr_id, "fr_name", "fr_sex", "fr_birth", "fr_edu", "fr_family_url", "fr_job", "fr_unit", "fr_relation", "fr_phone");
            return true;
        }
    }
}
