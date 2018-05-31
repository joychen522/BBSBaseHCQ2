using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  尿检结果
    /// </summary>
    public partial interface IBane_UrinalysisRecordBLL
    {
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddUrinalysisRecordUser(BaneUrinalysisRecordModel model);
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditUrinalysisRecordUser(BaneUrinalysisRecordModel model);
        /// <summary>
        ///  验证戒毒人员时，自动记录尿检记录
        /// </summary>
        /// <param name="user_identify"></param>
        /// <returns></returns>
        bool AutoAddUrinalysisRecordUser(string user_identify);
        /// <summary>
        ///  已检人数
        /// </summary>
        /// <returns></returns>
        int GetDetectionCount(int user_id);
        /// <summary>
        ///  获取尿检记录数据
        /// </summary>
        /// <param name="ur_id"></param>
        /// <returns></returns>
        BaneEditUrinalyRecord GetRecordData(int ur_id);
    }
}
