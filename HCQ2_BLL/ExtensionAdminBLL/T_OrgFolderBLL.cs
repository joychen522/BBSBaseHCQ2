using HCQ2_Common;
using HCQ2_Model;
using HCQ2_Model.TreeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_BLL
{
    public partial class T_OrgFolderBLL:HCQ2_IBLL.IT_OrgFolderBLL
    {
        /// <summary>
        ///  获取组织机构目录树数据
        /// </summary>
        /// <returns></returns>
        public List<OrgTreeModel> GetOrgTreeData(int user_id=0)
        {
            List<OrgTreeModel> listModel = new List<OrgTreeModel>();
            List<T_OrgFolder> list = new List<T_OrgFolder>();
            if (user_id<=0)
                 list = DBSession.IT_OrgFolderDAL.Select(s => !string.IsNullOrEmpty(s.folder_name)).ToList();
            else
            {
                List<T_Permissions> perList = DBSession.IT_PermissionsDAL.GetPermissById(user_id);
                List<int> perID = new List<int>();
                if (perList != null && perList.Count > 0)
                    perID = perList.Select(s => s.per_id).ToList();
                list = DBSession.IT_OrgFolderDAL.GetFolderByRelation(user_id, perID);
            }     
            //根据权限获取树结构 数据
            if (list == null || list.Count <= 0)
                return null;
            //一级目录
            List<T_OrgFolder> temp = list.FindAll(s => s.folder_pid == 0);
            if (temp == null || temp.Count <= 0)
            {
                var folder_path= list.Min(s => s.folder_path);
                temp = list.FindAll(s => s.folder_path == folder_path);
            }   
            foreach (T_OrgFolder item in temp)
            {
                var len = list.Where(s => s.folder_pid == item.folder_id).ToList();
                listModel.Add(new OrgTreeModel()
                {
                    id = item.folder_id,
                    name = item.folder_name,
                    pId = item.folder_pid,
                    if_sys = item.if_sys,
                    folder_path=item.folder_path,
                    children = (len.Count > 0) ? GetModelById(list, item.folder_id) : null
                });
            }
            return listModel;
        }
        /// <summary>
        ///  递归获取文档树
        /// </summary>
        /// <param name="list"></param>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        private List<OrgTreeModel> GetModelById(List<T_OrgFolder> list, int folder_id)
        {
            List<OrgTreeModel> listModel = new List<OrgTreeModel>();
            List<T_OrgFolder> listKey = list.FindAll(s => s.folder_pid == folder_id);
            if (listKey.Count > 0)
            {
                foreach (T_OrgFolder item in listKey)
                {
                    var len = list.Where(s => s.folder_pid == item.folder_id).ToList();
                    listModel.Add(new OrgTreeModel()
                    {
                        id = item.folder_id,
                        name = item.folder_name,
                        pId = item.folder_pid,
                        if_sys = item.if_sys,
                        folder_path = item.folder_path,
                        children = (len.Count > 0) ? GetModelById(list, item.folder_id) : null
                    });
                }
            }
            else
                return null;
            return listModel;
        }

        /// <summary>
        ///  添加节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNode(OrgTreeModel model)
        {
            if (null == model)
                return 0;
            T_OrgFolder folder = new T_OrgFolder
            {
                folder_name = model.name,
                folder_pid = model.pId,
                create_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id,
                create_name = HCQ2UI_Helper.OperateContext.Current.Usr.user_name,
                create_time = DateTime.Now,
                have_child = false
            };
            Add(folder);
            if (folder.folder_pid > 0)
                Modify(new T_OrgFolder { have_child = true }, s => s.folder_id == folder.folder_pid, "have_child");
            return folder.folder_id;
        }
        /// <summary>
        ///  编辑节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditNode(OrgTreeModel model)
        {
            if (model==null)
                return 0;
            T_OrgFolder folder = new T_OrgFolder
            {
                folder_id = model.id,
                folder_name = model.name
            };
            return Modify(folder, s => s.folder_id == folder.folder_id, "folder_name");
        }
        /// <summary>
        ///  删除节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteNode(T_OrgFolder model, int id)
        {
            if (id <= 0 || model == null)
                return 0;
            //删除前查询当前组织机构下是否有人员
            var query = DBSession.IBane_UserDAL.Select(s => s.org_id == id);
            if (query != null && query.Count > 0)
                return 2;//2表示当前组织机构下还有人员数据
            //查询其他同辈节点
            var count = Select(s => s.folder_pid == model.folder_pid && s.folder_id != id);
            int mark = Delete(s => s.folder_id == id);
            if (mark > 0 && (count == null || count.Count == 0))
                //没有同辈节点
                Modify(new T_OrgFolder { have_child = false }, s => s.folder_id == model.folder_pid, "have_child");
            if (mark > 0)
                DBSession.IT_OrgUserRelationDAL.Delete(s => s.folder_id == id);
            return mark;
        }
        /// <summary>
        ///  获取Table数据
        /// </summary>
        /// <param name="model">参数</param>
        /// <param name="total">根据条件返回记录条数</param>
        /// <returns></returns>
        public List<T_User> GetTableData(OrgTableParamModel model, out int total)
        {
            total = 0;
            if (null == model || model.folder_id <= 0)
                return null;
            return DBSession.IT_OrgFolderDAL.GetOrgUsers(model,out total);
        }
        /// <summary>
        ///  获取待分配人员数据
        /// </summary>
        /// <returns></returns>
        public List<HCQ2_Model.SelectModel.ListBoxModel> GetOrgDataByPerson()
        {
            return DBSession.IT_OrgFolderDAL.GetOrgDataByPerson();
        }
        /// <summary>
        ///  获取已分配人员数据
        /// </summary>
        /// <param name="folder_id">组织机构ID</param>
        /// <returns></returns>
        public List<HCQ2_Model.SelectModel.ListBoxModel> GetFineOrgDataByPerson(int folder_id)
        {
            return DBSession.IT_OrgFolderDAL.GetFineOrgDataByPerson(folder_id);
        }
        /// <summary>
        ///  保存组织机构数据设置
        /// </summary>
        /// <param name="personData"></param>
        /// <returns></returns>
        public bool SaveOrgDataByPerson(string personData, int folder_id)
        {
            if (string.IsNullOrEmpty(personData) || folder_id <= 0)
                return false;
            string[] str = personData.Trim(',').Split(',');//用户ID
            if (str.Length <= 0)
                return false;
            List<T_OrgUserRelation> list = DBSession.IT_OrgUserRelationDAL.Select(s => s.folder_id == folder_id);
            List<T_OrgUserRelation> temp = list;
            //1：添加
            foreach (string item in str)
            {
                var obj = list.FindAll(s => s.user_id == Helper.ToInt(item));
                if (obj != null && obj.Count > 0)
                {
                    temp.Remove(obj[0]);
                    continue;
                }
                DBSession.IT_OrgUserRelationDAL.Add(new T_OrgUserRelation
                {
                     folder_id=folder_id,
                     user_id= Helper.ToInt(item)
                });
            }
            //2：删除排除的
            if (temp != null && temp.Count > 0)
                DBSession.IT_OrgFolderDAL.DelOrgUserRelation(temp.Select(s => s.user_id).ToList(), folder_id);
            return true;
        }
        /// <summary>
        ///  查询所有单位信息，AreaTreeModel，
        ///  如果为空或者. 则查询一级单位
        ///  创建人：Joychen
        ///  创建时间：2018-04-23
        /// </summary>
        /// <returns></returns>
        public List<AreaTreeModel> GetAreaData(int user_id)
        {
            List<AreaTreeModel> listModel = new List<AreaTreeModel>();
            //1：获取当前用户对应的所有单位数据
            var orgUser = DBSession.IT_Org_UserDAL.Select(s => s.user_id == user_id);
            int tempID = (orgUser == null || orgUser.Count <= 0) ? 0 : user_id;
            List<T_OrgFolder> list = DBSession.IT_OrgFolderDAL.GetOrgFolderInfo(tempID);
            if (list.Count <= 0)
                return null;
            listModel = GetListData(list);
            if (tempID == 0)
            {
                AreaTreeModel model = new AreaTreeModel()
                {
                    text = "系统用户",
                    folder_id = 0,
                    keyChild = 0,
                    nodes = null
                };
                listModel.Insert(0, model);
            }
            return listModel; 
        }
        private List<AreaTreeModel> GetListData(List<T_OrgFolder> list)
        {
            List<AreaTreeModel> listModel = new List<AreaTreeModel>();
            //查找一级目录
            List<T_OrgFolder> first = list.FindAll(s => s.folder_pid == 0);
            if(first==null || first.Count <= 0)
            {
                var folder = list.Min(s => s.folder_path);
                first = list.Where(s => s.folder_path == folder).ToList();  
            }
            if (first.Count > 0)
            {
                for (int i = 0; i < first.Count; i++)
                {
                    listModel.Add(new AreaTreeModel()
                    {
                        text = first[i].folder_name,
                        folder_id = first[i].folder_id,
                        keyChild = first[i].have_child?1:0,
                        nodes = GetAreaModelById(list, first[i].folder_id)
                    });
                }
            }
            return listModel;
        }
        /// <summary>
        ///  递归查询单位信息
        ///  创建人：Joychen
        ///  创建时间：2018-04-23
        /// </summary>
        /// <param name="list"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        private List<AreaTreeModel> GetAreaModelById(List<T_OrgFolder> list, int folder_id)
        {
            List<AreaTreeModel> listModel = null;
            List<T_OrgFolder> listKey = list.FindAll(s => s.folder_pid == folder_id);
            if (listKey.Count > 0)
            {
                listModel = new List<AreaTreeModel>();
                AreaTreeModel temp = null;
                for (int i = 0; i < listKey.Count; i++)
                {
                    temp = new AreaTreeModel();
                    temp.text = listKey[i].folder_name;
                    temp.folder_id = listKey[i].folder_id;
                    temp.keyChild = listKey[i].have_child?1:0;
                    if (temp.keyChild > 0)
                        temp.nodes = GetAreaModelById(list, listKey[i].folder_id);
                    listModel.Add(temp);
                }
            }
            return listModel;
        }
        /// <summary>
        ///  根据用户ID统计 所属区域及子区域
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public int GetAreaCountByID(int user_id)
        {
            return DBSession.IT_OrgFolderDAL.GetAreaCountByID(user_id);
        }
    }
}
