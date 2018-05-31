using System.Collections.Generic;
using HCQ2_Model;
using HCQ2_Model.DocModel;
using System.Linq;
using System;
using HCQ2_Model.TreeModel;

namespace HCQ2_BLL
{
    public partial class T_DocumentFolderBLL:HCQ2_IBLL.IT_DocumentFolderBLL
    {
        private List<DocTreeListModel> docList = new List<DocTreeListModel>();
        private List<DocTreeListModel> docTempList = new List<DocTreeListModel>();
        /// <summary>
        ///  根据用户权限获取文档结构树
        /// </summary>
        /// <returns></returns>
        public List<DocTreeModel> GetDocTreeData(string pageType,int doc_type)
        {
            //1.获取当前用户权限集合
            List<T_Permissions> PerList = HCQ2UI_Helper.Session.SysPermissSession.PermissList;
            List<int> pList = new List<int>();
            if(PerList != null && PerList.Count > 0)
                pList = PerList.Select(s => s.per_id).ToList();
            List<T_DocumentFolder> list = DBSession.IT_DocumentFolderDAL.GetDocTreeData(HCQ2UI_Helper.OperateContext.Current.Usr.user_id,pageType, pList);
            if (null == list || list.Count <= 0)
                return null;
            List<DocTreeModel> listModel = new List<DocTreeModel>();
            #region 1. 一级目录
            List<T_DocumentFolder> temp = list.FindAll(s => s.folder_pid == 0);
            foreach (T_DocumentFolder item in temp)
            {
                var len = list.Where(s => s.folder_pid == item.folder_id).ToList();
                int total = GetFileInfoCount(doc_type, item.folder_id);//获取节点下文档数量
                docList.Add(new DocTreeListModel { folder_id = item.folder_id, folder_pid = item.folder_pid, total = total, doc_type = doc_type });
                listModel.Add(new DocTreeModel()
                {
                    id = item.folder_id,
                    name = item.folder_name,
                    pId = item.folder_pid,
                    if_create_child = item.if_create_child,
                    read_only = item.read_only,
                    was_share = item.was_share,
                    if_sys = item.if_sys,
                    doc_type = item.doc_type,
                    children = (len.Count > 0) ? GetModelById(list, item.folder_id,doc_type) : null
                });
            }
            #endregion

            #region 2.遍历更新docList
            if(null== docList || docList.Count<=0)
                return listModel;
            var lTemp = docList.Where(s => s.folder_pid == 0).ToList();
            foreach (DocTreeListModel item in lTemp)
            {
                int ownCount =item.total, childCount = 0, count;
                var child = docList.Where(s => s.folder_pid == item.folder_id).ToList();
                if (child.Count > 0)
                    childCount = GetFolderName(listModel,child, item.folder_id,doc_type);
                count = ownCount + childCount;
                if (count > 0)
                    listModel.Where(s => s.id == item.folder_id).FirstOrDefault().name += string.Format("({0})", count);
            }
            #endregion

            #region 3.更新二级目录及以下
            if(null== docTempList || docTempList.Count<=0)
                return listModel;
            foreach (DocTreeModel item in listModel)
            {
                var obj = docTempList.Where(s => s.folder_id == item.id).FirstOrDefault();
                if (obj != null)
                    item.name += string.Format("({0})", obj.total);
                if (item.children != null)
                    SetFolderName(item.children);
            }
            #endregion
            return listModel;
        }
        private void SetFolderName(List<DocTreeModel> model)
        {
            foreach (DocTreeModel item in model)
            {
                var obj = docTempList.Where(s => s.folder_id == item.id).FirstOrDefault();
                if (obj != null)
                    item.name += string.Format("({0})", obj.total);
                if (item.children != null)
                    SetFolderName(item.children);
            }
        }
        private int GetFolderName(List<DocTreeModel> listModel, List<DocTreeListModel> model, int folder_id,int doc_type)
        {
            int total = 0;
            foreach (DocTreeListModel item in model)
            {
                int ownCount = item.total, childCount = 0;
                var child = docList.Where(s => s.folder_pid == item.folder_id).ToList();
                if (child.Count > 0)
                    childCount += GetFolderName(listModel, child, item.folder_id,doc_type);
                total += ownCount + childCount;
                if (total > 0 && child.Count > 0)
                    docTempList.Add(new DocTreeListModel { folder_id = item.folder_id, total = total });
            }
            return total;
        }
        /// <summary>
        ///  递归获取文档树
        /// </summary>
        /// <param name="list"></param>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        private List<DocTreeModel> GetModelById(List<T_DocumentFolder> list, int folder_id,int doc_type)
        {
            List<DocTreeModel> listModel = new List<DocTreeModel>();
            List<T_DocumentFolder> listKey = list.FindAll(s => s.folder_pid == folder_id);
            if (listKey.Count > 0)
            {
                foreach (T_DocumentFolder item in listKey)
                {
                    var len = list.Where(s => s.folder_pid == item.folder_id).ToList();
                    int total = GetFileInfoCount(doc_type, item.folder_id);
                    string folderName = item.folder_name;
                    if(len.Count<=0 && total > 0)
                        folderName += total > 0 ? string.Format("({0})", total) : "";
                    docList.Add(new DocTreeListModel { folder_id = item.folder_id, folder_pid = item.folder_pid, total = total });
                    listModel.Add(new DocTreeModel()
                    {
                        id = item.folder_id,
                        name = folderName,
                        pId = item.folder_pid,
                        if_create_child = item.if_create_child,
                        read_only = item.read_only,
                        was_share = item.was_share,
                        if_sys = item.if_sys,
                        doc_type = item.doc_type,
                        children = (len.Count > 0) ? GetModelById(list, item.folder_id,doc_type) : null
                    });
                }
            }
            else
                return null;
            return listModel;
        }
        /// <summary>
        ///  统计文档节点下的文档数量
        /// </summary>
        /// <param name="doc_type">节点类型</param>
        /// <param name="folder_id">节点ID</param>
        /// <returns></returns>
        private int GetFileInfoCount(int doc_type,int folder_id)
        {
            int total = 0;
            int user_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
            var dal = DBSession.IT_DocumentInfoDAL;
            DocTableParamModel model = new DocTableParamModel { folder_id = folder_id };
            switch (doc_type)
            {
                case 1: total = dal.GetTableByOwnShareDocCount(model, user_id); break;
                case 2: {
                        List<int> roles = HCQ2UI_Helper.Session.SysPermissSession.RolesList;
                        total = dal.GetTableShareByOwnDocCount(model, user_id, roles);
                    }  break;
                case 3: total = dal.GetTablePublicDocCountByOwn(model, user_id); break;//公用5
                case 4: total = dal.GetTableRemoveDocCount(model, user_id); break;
                case 5: total = dal.GetTableApproveDocCount(model, user_id); break;//待审核1
                default: total = dal.GetTableByOwnDocCount(model, user_id); break;
            }
            return total;
        }
        /// <summary>
        ///  添加节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNode(DocTreeModel model)
        {
            if (null == model)
                return 0;
            T_DocumentFolder folder = new T_DocumentFolder
            {
                folder_name = model.name,
                folder_pid = model.pId,
                if_create_child = true,
                read_only = false,
                create_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id,
                create_name = HCQ2UI_Helper.OperateContext.Current.Usr.user_name,
                create_time = DateTime.Now,
                page_type = model.pageType,
                have_child = false
            };
            Add(folder);
            if (folder.folder_pid > 0)
                Modify(new T_DocumentFolder { have_child = true }, s => s.folder_id == folder.folder_pid, "have_child");
            return folder.folder_id;
        }
        /// <summary>
        ///  编辑节点
        /// </summary>
        /// <param name="mdoel"></param>
        /// <returns></returns>
        public int EditNode(int id, string name)
        {
            if (id==0 || string.IsNullOrEmpty(name))
                return 0;
            T_DocumentFolder folder = new T_DocumentFolder
            {
                folder_id = id,
                folder_name = name
            };
            return Modify(folder, s => s.folder_id == folder.folder_id, "folder_name");
        }
        /// <summary>
        ///  删除节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteNode(T_DocumentFolder model,int id)
        {
            if (id <= 0 || model==null)
                return 0;
            var count = Select(s => s.folder_pid == model.folder_pid && s.folder_id != id);
            int mark = Delete(s => s.folder_id == id);
            if (mark > 0 && (count == null || count.Count == 0))
                Modify(new T_DocumentFolder { have_child = false }, s => s.folder_id == model.folder_pid, "have_child");
            return mark;
        }
        /// <summary>
    ///  获取子目录
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
        public List<TreeTableAttribute> GetMenuDataByPid(int id, string type)
        {
            List<T_DocumentFolder> list = null;
            if (!string.IsNullOrEmpty(type) && type.Equals("own"))
                list = DBSession.IT_DocumentFolderDAL.Select<int>(s => s.folder_id == id,
                    s => (int)s.folder_order, true);
            else
                list = DBSession.IT_DocumentFolderDAL.Select<int>(s => s.folder_pid == id && s.if_sys==true,
                    s => (int)s.folder_order, true);
            if (list.Count <= 0)
                return null;
            List<TreeTableAttribute> listTree = new List<TreeTableAttribute>();
            foreach (T_DocumentFolder item in list)
            {
                listTree.Add(new TreeTableAttribute()
                {
                    id = item.folder_id.ToString(),
                    name = item.folder_name,
                    pId = item.folder_pid.ToString(),
                    url = item.folder_url,
                    icon = item.folder_image,
                    hasChild = item.have_child
                });
            }
            return listTree;
        }
        //系统节点
        /// <summary>
        ///  添加系统节点
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public DocTreeModel AddSysNode(DocTreeModel folder)
        {
            if(null==folder || string.IsNullOrEmpty(folder.name))
                return null;
            T_DocumentFolder model = new T_DocumentFolder
            {
                folder_name = folder.name,
                folder_pid = folder.pId,
                was_share = folder.was_share,
                if_create_child = folder.if_create_child,
                read_only = folder.read_only,
                if_sys = folder.if_sys,
                doc_type = folder.doc_type,
                create_id = HCQ2UI_Helper.OperateContext.Current.Usr.user_id,
                create_name = HCQ2UI_Helper.OperateContext.Current.Usr.user_name,
                create_time = DateTime.Now,
                page_type = folder.pageType,
                have_child = false
            };
            int mark = Add(model);
            if(folder.pId>0)
                Modify(new T_DocumentFolder { have_child = true }, s => s.folder_id == folder.pId, "have_child");
            if (mark > 0)
            {
                folder.id = model.folder_id;
                return folder;
            }
            return null;
        }
        /// <summary>
        ///  编辑系统节点
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public DocTreeModel EditSysNode(DocTreeModel folder)
        {
            if(null==folder || folder.id<=0 || string.IsNullOrEmpty(folder.name))
                return null;
            T_DocumentFolder model = new T_DocumentFolder
            {
                folder_id = folder.id,
                folder_name = folder.name,
                folder_pid = folder.pId,
                was_share = folder.was_share,
                if_create_child = folder.if_create_child,
                read_only = folder.read_only,
                if_sys = folder.if_sys,
                doc_type = folder.doc_type
            };
            int mark = Modify(model, s => s.folder_id == model.folder_id, "folder_name", "was_share", "if_create_child", "read_only", "if_sys", "doc_type");
            if (mark > 0)
                return folder;
            return null;
        }
    }
}
