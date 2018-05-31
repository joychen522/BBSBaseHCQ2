using System.Collections.Generic;
using System.Linq;
using HCQ2_Model;
using HCQ2_Model.SysModel;

namespace HCQ2_BLL
{
    public partial class T_UserUnitRelationBLL:HCQ2_IBLL.IT_UserUnitRelationBLL
    {
        /// <summary>
        ///  用户user_id获取代管的区域
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetUserUnitDataById(int user_id)
        {
            //封装单位键值对数据
            List<Dictionary<string, object>> listUnit = new List<Dictionary<string, object>>();
            Dictionary<string, object> map = null;
            //1 获取所有区域
            List<T_OrgFolder> listAll = DBSession.IT_OrgFolderDAL.Select(s => (!string.IsNullOrEmpty(s.folder_name))).ToList();
            //2 获取用户对应单位
            List<T_UserUnitRelation> listUser = Select(s => s.user_id == user_id);
            //3 设置单位
            foreach (T_OrgFolder org in listAll)
            {
                map = new Dictionary<string, object>();
                map.Add("id", org.folder_id);
                map.Add("pId", org.folder_pid);
                map.Add("name", org.folder_name);
                //3.1当前节点是否被选中
                var temp = listUser.Where(s => s.unit_id == org.folder_id.ToString()).ToList();
                if (temp.Count> 0)
                    map.Add("checked", true);
                //3.2 是否有子节点
                if (org.have_child)
                {
                    map.Add("open",true);
                    map.Add("children", GetChildData(listAll, listUser, org.folder_id,user_id));
                }
                listUnit.Add(map);
            }
            return listUnit;
        }
        /// <summary>
        ///  获取全部区域 以及区域下的人员  根据权限设置对于数据
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetUserAreaAndPersonById(int user_id, bool isAll = true)
        {
            //封装单位键值对数据
            List<Dictionary<string, object>> listUnit = new List<Dictionary<string, object>>();
            Dictionary<string, object> map = null;
            //1 获取所有区域
            List<T_OrgFolder> listAll = (isAll) ? DBSession.IT_OrgFolderDAL.Select(s => (!string.IsNullOrEmpty(s.folder_name))).ToList() : DBSession.IT_OrgFolderDAL.GetOrgFolderInfo(user_id);
            //1.1 获取所有区域下的人员
            List<Bane_User> userAll = DBSession.IBane_UserDAL.Select(s => s.org_id > 0).ToList();
            //1.2 获取全部用户-人员-区域 权限记录
            List<T_UserUnitPersonRelation> personAll = DBSession.IT_UserUnitPersonRelationDAL.Select(s => s.user_id > 0).ToList();
            //2 获取用户对应单位
            List<T_UserUnitRelation> listUser = Select(s => s.user_id == user_id);
            //3 设置单位
            foreach (T_OrgFolder org in listAll)
            {
                map = new Dictionary<string, object>();
                map.Add("id", org.folder_id);
                map.Add("pId", org.folder_pid);
                map.Add("name", org.folder_name);
                map.Add("tree_type","area");//区域
                //3.1当前节点是否被选中
                var temp = listUser.Where(s => s.unit_id == org.folder_id.ToString()).ToList();
                if (temp.Count > 0)
                    map.Add("checked", true);
                //3.2 是否有子节点
                if (org.have_child)
                {
                    map.Add("open", true);
                    map.Add("children", GetChildData(listAll, listUser, org.folder_id,user_id, personAll, userAll, true));
                }
                else
                {
                    #region 无子节点 添加人员
                    var query = userAll.Where(s => s.org_id == org.folder_id).ToList();
                    if(query!=null && query.Count > 0)
                    {
                        map.Add("open", true);
                        map.Add("children", GetChildTreePersonData(query, org.folder_id,user_id, personAll));
                    }
                    #endregion
                }

                listUnit.Add(map);
            }
            return listUnit;
        }
        public bool SaveUserUnitData(string userData, int user_id)
        {
            //保存之前先清理
            Delete(s => s.user_id == user_id);
            DBSession.IT_UserUnitPersonRelationDAL.Delete(s => s.user_id == user_id);
            if (string.IsNullOrEmpty(userData))
                return true;
            List<UserUnitRelation> list = new List<UserUnitRelation>();
            //List<T_UserUnitRelation> list = new List<T_UserUnitRelation>();
            list = HCQ2_Common.JsonHelper.JsonStrToList(userData, list);
            if (list == null || list.Count <= 0)
                return true;
            //1：用户-区域权限
            var query = list.Where(s => s.tree_type.Equals("area")).ToList();
            foreach(UserUnitRelation item in query)
            {
                Add(new T_UserUnitRelation {
                    user_id=item.user_id,
                    unit_id=item.unit_id,
                    unit_name=item.unit_name
                });
            }
            //2：用户-人员-区域权限
            query = list.Where(s => s.tree_type.Equals("person")).ToList();
            foreach(UserUnitRelation item in query)
            {
                DBSession.IT_UserUnitPersonRelationDAL.Add(new T_UserUnitPersonRelation
                {
                    user_id=item.user_id,
                    person_id=HCQ2_Common.Helper.ToInt(item.unit_id),
                    org_id=item.unit_pid
                });
            }
            return true;
        }

        /// <summary>
        ///  添加区域下的人员数据
        /// </summary>
        /// <param name="users"></param>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> GetChildTreePersonData(List<Bane_User> users,int folder_id,int user_id,List<T_UserUnitPersonRelation> personAll)
        {
            List<Dictionary<string, object>> listChild = new List<Dictionary<string, object>>();
            Dictionary<string, object> map = null;
            var userTemp = personAll.Where(s => s.user_id==user_id && s.org_id == folder_id).ToList();
            foreach (Bane_User item in users)
            {
                map = new Dictionary<string, object>();
                map.Add("id", item.user_id);
                map.Add("pId", folder_id);
                map.Add("name", item.user_name);
                map.Add("tree_type","person");//人员
                //1.1当前节点是否被选中
                var temp = userTemp?.Where(s => s.person_id==item.user_id).ToList();
                if (temp!=null && temp.Count > 0)
                    map.Add("checked", true);
                listChild.Add(map);
            }
            return listChild;
        }

        /// <summary>
        ///  递归获取子节点
        /// </summary>
        /// <param name="listAll"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> GetChildData(List<T_OrgFolder> listAll, List<T_UserUnitRelation> listUser, int parentID,int user_id,List<T_UserUnitPersonRelation> personAll=null, List<Bane_User> userAll=null, bool showPerson=false)
        {
            List<Dictionary<string, object>> listChild = new List<Dictionary<string, object>>();
            List<T_OrgFolder> list = listAll.Where(s => s.folder_pid == parentID).ToList();
            Dictionary<string, object> map = null;
            if (list.Count > 0)
            {
                foreach (T_OrgFolder org in list)
                {
                    map = new Dictionary<string, object>();
                    map.Add("id", org.folder_id);
                    map.Add("pId", org.folder_pid);
                    map.Add("name", org.folder_name);
                    //1.1当前节点是否被选中
                    var temp = listUser.Where(s => s.unit_id == org.folder_id.ToString()).ToList();
                    if (temp.Count > 0)
                        map.Add("checked", true);
                    //1.2 是否有子节点
                    if (org.have_child)
                    {
                        map.Add("open", true);
                        map.Add("children", GetChildData(listAll, listUser, org.folder_id, user_id, personAll, userAll, showPerson));
                    }else if(showPerson)
                    {
                        //显示区域下的人员信息
                        #region 无子节点 添加人员
                        var query = userAll.Where(s => s.org_id == org.folder_id).ToList();
                        if (query != null && query.Count > 0)
                        {
                            map.Add("open", true);
                            map.Add("children", GetChildTreePersonData(query, org.folder_id, user_id, personAll));
                        }
                        #endregion
                    }
                    //listChild.Add(map);
                }
            }
            return listChild;
        }
    }
}
