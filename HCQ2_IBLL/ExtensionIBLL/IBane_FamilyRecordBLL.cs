using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  家庭成员&社会关系
    /// </summary>
    public partial interface IBane_FamilyRecordBLL
    {
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddFamilyRecordUser(BaneFamilyRecordModel model);
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditFamilyRecordUser(BaneFamilyRecordModel model);
    }
}
