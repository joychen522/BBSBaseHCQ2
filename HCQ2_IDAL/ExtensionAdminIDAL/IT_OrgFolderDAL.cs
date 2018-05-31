using HCQ2_Model;
using HCQ2_Model.TreeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_IDAL
{
    /// <summary>
    ///  组织机构数据层接口定义
    /// </summary>
    public partial interface IT_OrgFolderDAL
    {
        /// <summary>
        ///  根据组织机构ID获取所对应的用户集合
        /// </summary>
        /// <param name="folder_id"></param>
        /// <param name="total">输出参数</param>
        /// <returns></returns>
        List<HCQ2_Model.T_User> GetOrgUsers(OrgTableParamModel model, out int total);
        /// <summary>
        ///  获取待分配人员数据
        /// </summary>
        /// <returns></returns>
        List<HCQ2_Model.SelectModel.ListBoxModel> GetOrgDataByPerson();
        /// <summary>
        ///  获取已分配人员数据
        /// </summary>
        /// <param name="folder_id">组织机构ID</param>
        /// <returns></returns>
        List<HCQ2_Model.SelectModel.ListBoxModel> GetFineOrgDataByPerson(int folder_id);
        /// <summary>
        ///  排除指定组织机构下的人员
        /// </summary>
        /// <param name="list"></param>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        bool DelOrgUserRelation(List<int> list,int folder_id);
        /// <summary>
        ///  根据权限获取用户代管区域
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <param name="perList">权限id集合</param>
        /// <returns></returns>
        List<T_OrgFolder> GetFolderByRelation(int user_id,List<int> perList);
        /// <summary>
        ///  根据单位代码获取子单位信息集合
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        List<T_OrgFolder> GetOrgFolderInfo(int user_id);
        /// <summary>
        ///  根据用户ID统计 所属区域及子区域
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        int GetAreaCountByID(int user_id);
    }
}
