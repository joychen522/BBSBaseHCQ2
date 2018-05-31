using HCQ2_Model;
using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class Bane_CriminalRecordBLL:HCQ2_IBLL.IBane_CriminalRecordBLL
    {
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCriminalUser(BaneCriminalModel model)
        {
            if (model == null)
                return false;
            Bane_CriminalRecord record = new Bane_CriminalRecord
            {
                user_identify = model.user_identify,
                start_drug_date = (!string.IsNullOrEmpty(model.start_drug_date)) ? DateTime.ParseExact(model.start_drug_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null,
                drug_year = model.drug_year,
                force_time = model.force_time,
                force_insulate = model.force_insulate,
                other_record = model.other_record
            };
            int mark = DBSession.IBane_CriminalRecordDAL.Add(record);
            return mark > 0 ? true : false;
        }

        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditCriminalUser(BaneCriminalModel model)
        {
            if (model == null)
                return false;
            Bane_CriminalRecord record = new Bane_CriminalRecord
            {
                user_identify = model.user_identify,
                start_drug_date = (!string.IsNullOrEmpty(model.start_drug_date)) ? DateTime.ParseExact(model.start_drug_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN")) : (DateTime?)null,
                drug_year = model.drug_year,
                force_time = model.force_time,
                force_insulate = model.force_insulate,
                other_record = model.other_record
            };
            DBSession.IBane_CriminalRecordDAL.Modify(record,s=>s.cr_id==model.cr_id, "start_drug_date","drug_year","force_time","force_insulate","other_record");
            return true;
        }
    }
}
