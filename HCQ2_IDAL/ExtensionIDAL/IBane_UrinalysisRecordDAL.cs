using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    public partial interface IBane_UrinalysisRecordDAL
    {
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
