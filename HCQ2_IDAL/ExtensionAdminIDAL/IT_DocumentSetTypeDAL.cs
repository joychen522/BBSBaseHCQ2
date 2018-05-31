using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  文档分享 数据层接口
    /// </summary>
    public partial interface IT_DocumentSetTypeDAL
    {
        /// <summary>
        ///  获取分享人员数据
        /// </summary>
        /// <returns></returns>
        List<HCQ2_Model.SelectModel.ListBoxModel> GetShareDataByPerson();
        /// <summary>
        ///  获取分享角色数据
        /// </summary>
        /// <returns></returns>
        List<HCQ2_Model.SelectModel.ListBoxModel> GetShareDataByRole();
    }
}
