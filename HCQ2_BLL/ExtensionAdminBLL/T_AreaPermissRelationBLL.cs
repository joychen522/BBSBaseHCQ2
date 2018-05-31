using HCQ2_Common;
using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class T_AreaPermissRelationBLL : HCQ2_IBLL.IT_AreaPermissRelationBLL
    {
        public List<Dictionary<string, object>> GetLimitAreaDataById(int per_id)
        {
            //封装单位键值对数据
            List<Dictionary<string, object>> listUnit = new List<Dictionary<string, object>>();
            Dictionary<string, object> map = null;
            //1 获取所有区域
            List<T_OrgFolder> listAll = DBSession.IT_OrgFolderDAL.Select(s => s.folder_id > 0);
            //2 获取权限对应区域
            List<T_AreaPermissRelation> listPer = Select(s => s.per_id == per_id);
            //3.1 获取所有区域下的人员
            List<Bane_User> userAll = DBSession.IBane_UserDAL.Select(s => s.org_id > 0);
            //3.2 获取所有人员代管权限
            List<Bane_UserPermissRelation> userRelation = DBSession.IBane_UserPermissRelationDAL.Select(s => s.per_id == per_id);
            //3 设置区域
            foreach (T_OrgFolder area in listAll)
            {
                map = new Dictionary<string, object>();
                map.Add("id", area.folder_id);
                map.Add("pId", area.folder_pid);
                map.Add("name", area.folder_name);
                map.Add("tree_type", "area");//区域
                //3.1当前节点是否被选中
                var temp = listPer.Where(s => s.area_code == area.folder_id.ToString()).ToList();
                if (temp.Count > 0)
                {
                    map.Add("checked", true);
                    map.Add("everstate", "checked");//标记
                }
                else
                    map.Add("everstate", "uncheck");//标记

                //3.2 是否有子节点
                if (area.have_child)
                {
                    map.Add("open", true);
                    map.Add("children", GetChildData(listAll, listPer, userAll, userRelation, area.folder_id));
                }
                else
                {
                    #region 无子节点 添加人员
                    var query = userAll.Where(s => s.org_id == area.folder_id).ToList();
                    if (query != null && query.Count > 0)
                    {
                        map.Add("open", true);
                        map.Add("children", GetChildTreePersonData(query, area.folder_id,userRelation));
                    }
                    #endregion
                }
                listUnit.Add(map);
            }
            return listUnit;
        }

        public bool SaveAreaPerData(string userData, string reak, int per_id)
        {
            if (reak.Equals("undeal"))
                return true;//无需后端处理
            if (per_id <= 0)
                return false;//权限主键值有误
            //1. 判断是否删除全部
            if (string.IsNullOrEmpty(userData) || userData.Replace(":", "").Replace(";","").Trim().Length == 0)
            {
                Delete(s => s.per_id == per_id);
                DBSession.IBane_UserPermissRelationDAL.Delete(s => s.per_id == per_id);
                return true;
            }
            //2. 保存之前删除之前设置的权限
            string[] menu = userData.Split(':');//0添加，1删除
            string[] delData = menu[1].Split(';');//0区域，1人员
            //2.1 删除区域代管
            if (delData.Length > 1 && !string.IsNullOrEmpty(delData[0].Trim(',')))
                DBSession.IT_AreaPermissRelationDAL.Delete(new List<string>(delData[0].Trim(',').Split(',')), per_id);
            //2.2 删除人员代管
            if (delData.Length > 1 && !string.IsNullOrEmpty(delData[1].Trim(',')))
                DBSession.IBane_UserPermissRelationDAL.Delete(new List<string>(delData[1].Trim(',').Split(',')), per_id);
            string[] addData = menu[0].Split(';');//0区域，1人员
            //3.1 添加区域
            if (!string.IsNullOrEmpty(addData[0].Trim(',')))
            {
                string[] obj = addData[0].Trim(',').Split(',');
                if (obj.Length > 0)
                {
                    foreach (string item in obj)
                    {
                        DBSession.IT_AreaPermissRelationDAL.Add(new T_AreaPermissRelation {
                            area_code= item,
                            per_id=per_id
                        });
                    }
                }
            }
            //3.2 添加人员 
            if (!string.IsNullOrEmpty(addData[1].Trim(',')))
            {
                string[] obj = addData[1].Trim(',').Split(',');
                if (obj.Length > 0)
                {
                    foreach (string item in obj)
                    {
                        DBSession.IBane_UserPermissRelationDAL.Add(new Bane_UserPermissRelation
                        {
                            user_id = Helper.ToInt(item.Split('[')[1]),
                            per_id = per_id,
                            folder_id = Helper.ToInt(item.Split('[')[0])
                        });
                    }
                }
            }
            return true;
        }

        /// <summary>
        ///  递归获取子节点
        /// </summary>
        /// <param name="listAll"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> GetChildData(List<T_OrgFolder> listAll, List<T_AreaPermissRelation> listPer, List<Bane_User> userAll, List<Bane_UserPermissRelation> userRelation, int parentID)
        {
            List<Dictionary<string, object>> listChild = new List<Dictionary<string, object>>();
            List<T_OrgFolder> list = listAll.Where(s => s.folder_pid == parentID).ToList();
            Dictionary<string, object> map = null;
            if (list.Count > 0)
            {
                foreach (T_OrgFolder area in list)
                {
                    map = new Dictionary<string, object>();
                    map.Add("id", area.folder_id);
                    map.Add("pId", area.folder_pid);
                    map.Add("name", area.folder_name);
                    map.Add("tree_type", "area");//区域
                    //1.1当前节点是否被选中
                    var temp = listPer.Where(s => s.area_code == area.folder_id.ToString()).ToList();
                    if (temp.Count > 0)
                        map.Add("checked", true);
                    //1.2 是否有子节点
                    if (area.have_child)
                    {
                        map.Add("open", true);
                        map.Add("children", GetChildData(listAll, listPer, userAll, userRelation, area.folder_id));
                    }
                }
            }
            return listChild;
        }
        /// <summary>
        ///  添加区域下的人员数据
        /// </summary>
        /// <param name="users"></param>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> GetChildTreePersonData(List<Bane_User> users, int folder_id,List<Bane_UserPermissRelation> userRelation)
        {
            List<Dictionary<string, object>> listChild = new List<Dictionary<string, object>>();
            Dictionary<string, object> map = null;
            var userTemp = userRelation.Where(s => s.folder_id == folder_id).ToList();
            foreach (Bane_User item in users)
            {
                map = new Dictionary<string, object>();
                map.Add("id", item.user_id);
                map.Add("pId", folder_id);
                map.Add("name", item.user_name);
                map.Add("tree_type", "person");//人员
                //1.1当前节点是否被选中
                var temp = userTemp?.Where(s => s.user_id == item.user_id).ToList();
                if (temp != null && temp.Count > 0)
                {
                    map.Add("checked", true);
                    map.Add("everstate", "checked");//标记
                }
                else
                    map.Add("everstate", "uncheck");//标记

                listChild.Add(map);
            }
            return listChild;
        }
    }
}
