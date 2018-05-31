using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IBLL
{
    /// <summary>
    ///  文档分享 业务接口
    /// </summary>
    public partial interface IT_DocumentSetTypeBLL
    {
        /// <summary>
        ///  获取分享人员数据
        /// </summary>
        /// <returns></returns>
        List<HCQ2_Model.SelectModel.ListBoxModel> GetShareDataByPerson();
        /// <summary>
        ///  保存分享人员数据设置
        /// </summary>
        /// <param name="personData"></param>
        /// <returns></returns>
        bool SaveShareDataByPerson(string personData,int file_id);
        /// <summary>
        ///  获取分享角色数据
        /// </summary>
        /// <returns></returns>
        List<HCQ2_Model.SelectModel.ListBoxModel> GetShareDataByRole();
        /// <summary>
        ///  保存分享角色数据设置
        /// </summary>
        /// <param name="personData"></param>
        /// <returns></returns>
        bool SaveShareDataByRole(string personData, int file_id);
    }
}
