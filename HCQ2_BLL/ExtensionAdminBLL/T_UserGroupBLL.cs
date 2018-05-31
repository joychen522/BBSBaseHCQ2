using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2UI_Helper;
using HCQ2_Model;

namespace HCQ2_BLL
{
    /// <summary>
    ///  用户组业务实现层
    /// </summary>
    public partial class T_UserGroupBLL:HCQ2_IBLL.IT_UserGroupBLL
    {
        public List<T_UserGroup> GetUserGroupData(string groupName, int page, int rows,string sm_code)
        {
            if (!string.IsNullOrEmpty(groupName))
                return base.DBSession.IT_UserGroupDAL.Select<int>(s => s.group_name.Contains(groupName) && s.sm_code.Equals(sm_code), s => s.group_order,
                    page, rows, true);
            return base.DBSession.IT_UserGroupDAL.Select<int>(s => (!string.IsNullOrEmpty(s.group_name)) && s.sm_code.Equals(sm_code), s => s.group_order,
                page, rows, true);
        }
        /// <summary>
        /// 实现 添加
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns></returns>
        public bool AddGroup(T_UserGroup userGroup)
        {
            if (null == userGroup)
                return false;
            userGroup.creator_date = DateTime.Now;
            userGroup.creator_id = OperateContext.Current.Usr.user_id;
            userGroup.group_order = 1;
            int count = Add(userGroup);
            if(count>0)
                return true;
            return false;
        }
        /// <summary>
        ///  实现 编辑
        /// </summary>
        /// <param name="userGroup"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public bool EditGroup(T_UserGroup userGroup, int group_id)
        {
            if (null == userGroup)
                return false;
            userGroup.group_id = group_id;
            userGroup.update_date = DateTime.Now;
            userGroup.update_id = OperateContext.Current.Usr.user_id;
            Modify(userGroup,"group_name", "group_cname", "update_id", "update_date", "group_note","sm_code");
            return true;
        }
    }
}
