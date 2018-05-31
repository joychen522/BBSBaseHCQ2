using HCQ2_Model.BaneUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  违法犯罪档案袋业务接口
    /// </summary>
    public partial interface IBane_CriminalRecordBLL
    {
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddCriminalUser(BaneCriminalModel model);
        /// <summary>
        ///  添加对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditCriminalUser(BaneCriminalModel model);
    }
}
