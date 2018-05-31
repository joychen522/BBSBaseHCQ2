using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCQ2_IBLL;
using HCQ2_Model;
using HCQ2_Model.ExtendsionModel;
using HCQ2_Model.TreeModel;

namespace HCQ2_BLL
{
    /// <summary>
    ///  业务实现
    /// </summary>
    public partial class T_PageFolderBLL : IT_PageFolderBLL
    {
        /// <summary>
        ///  菜单管理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="sm_code"></param>
        /// <returns></returns>
        public List<TreeTableAttribute> GetMenuDataByPid(int id, string type,string sm_code,bool isAll=false)
        {
            List<T_PageFolder> list = null;
            if (isAll)
            {
                //全部
                if (!string.IsNullOrEmpty(type) && type.Equals("own"))
                    list = DBSession.IT_PageFolderDAL.Select<int>(s => s.folder_id == id && s.sm_code.Equals(sm_code),
                        s => (int)s.folder_order, true);
                else
                    list = DBSession.IT_PageFolderDAL.Select<int>(s => s.folder_pid == id && s.sm_code.Equals(sm_code),
                        s => (int)s.folder_order, true);
            }
            else
            {
                if (!string.IsNullOrEmpty(type) && type.Equals("own"))
                    list = DBSession.IT_PageFolderDAL.Select<int>(s => s.folder_id == id && s.sm_code.Equals(sm_code) && !s.folder_type.Equals("childPage"),
                        s => (int)s.folder_order, true);
                else
                    list = DBSession.IT_PageFolderDAL.Select<int>(s => s.folder_pid == id && s.sm_code.Equals(sm_code) && !s.folder_type.Equals("childPage"),
                        s => (int)s.folder_order, true);
            }
            if (list.Count <= 0)
                return null;
            List<TreeTableAttribute> listTree = new List<TreeTableAttribute>();
            foreach (T_PageFolder item in list)
            {
                listTree.Add(new TreeTableAttribute()
                {
                    id = item.folder_id.ToString(),
                    name = item.folder_name,
                    pId = item.folder_pid.ToString(),
                    url = item.folder_url,
                    icon = item.folder_image,
                    folder_type = item.folder_type,
                    hasChild = (isAll) ? item.have_child : Select(s => s.folder_pid == item.folder_id && !s.folder_type.Equals("childPage")).Count() > 0 ? true : false,//item.have_child
                    sm_code = item.sm_code
                });
            }
            return listTree;
        }

        /// <summary>
        ///  保存编辑菜单
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EditPageFolder(T_PageFolder folder, int id)
        {
            if (folder == null)
                return false;
            int tempCopunt = DBSession.IT_PageFolderDAL.Modify(folder, s => s.folder_id == id, "folder_name",
                "folder_image","folder_url","sm_code", "is_Bus", "Bus_Code", "folder_type");
            return true;
        }

        /// <summary>
        ///  保存信息菜单
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="folder_pid"></param>
        /// <returns></returns>
        public int AddFolder(T_PageFolder folder, int folder_pid)
        {
            if (folder == null)
                return 0;
            folder.folder_pid = folder_pid;
            //根据folder_pid获取当前加入项的排序号
            T_PageFolder Order = DBSession.IT_PageFolderDAL.Select<int>(s => s.folder_pid == folder_pid,
                s => (int) s.folder_order, false).FirstOrDefault();
            int folder_order;
            if (Order != null)
                folder_order = (int) Order.folder_order + 1;
            else
                folder_order = 1;
            folder.folder_order = folder_order;
            int tempCount = DBSession.IT_PageFolderDAL.Add(folder);
            if (tempCount > 0)
            {
                T_PageFolder pageFolder =
                    DBSession.IT_PageFolderDAL.Select(s => s.folder_id == folder_pid).FirstOrDefault();
                if (pageFolder != null)
                {
                    pageFolder.have_child = true;
                    DBSession.IT_PageFolderDAL.Modify(pageFolder, s => s.folder_id == folder_pid, "have_child");
                }
                return folder.folder_id;
            }
            return 0;
        }

        /// <summary>
        ///  实现删除
        /// </summary>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        public bool DelFolder(int folder_id)
        {
            T_PageFolder folder = Select(s => s.folder_id == folder_id).FirstOrDefault();
            T_PageFolder page = Select(s => s.folder_id == folder.folder_pid).FirstOrDefault();
            int isBack = DBSession.IT_PageFolderDAL.Delete(s => s.folder_id == folder_id || s.folder_pid == folder_id);
            if (page != null && SelectCount(s => s.folder_pid == folder.folder_pid) == 0)
            {
                page.have_child = false;
                //有父目录
                Modify(page, s => s.folder_id == page.folder_id, "have_child");
            }
            return isBack > 0 ? true : false;
        }

        /// <summary>
        ///  实现排序
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int OrderMenuById(int id, string type)
        {
            if (id == 0)
                return 0;
            HCQ2_Model.T_PageFolder folder = DBSession.IT_PageFolderDAL.Select(s => s.folder_id == id).FirstOrDefault();
            HCQ2_Model.T_PageFolder folderNew = new T_PageFolder();
            int folder_order = 0;
            switch (type)
            {
                case "up":
                {
                    folder_order = (int) folder.folder_order - 1;
                    folderNew =
                        DBSession.IT_PageFolderDAL.Select<int>(
                            s => s.folder_pid == folder.folder_pid && s.folder_order <= folder_order,
                            s => (int) s.folder_order, false).FirstOrDefault();
                    if (folder != null) folder.folder_order = folder_order;
                    if (folderNew != null) folderNew.folder_order = folder_order + 1;
                }
                    break;
                case "down":
                {
                    folder_order = (int) folder.folder_order + 1;
                    folderNew =
                        DBSession.IT_PageFolderDAL.Select<int>(
                            s => s.folder_pid == folder.folder_pid && s.folder_order >= folder_order,
                            s => (int) s.folder_order, true).FirstOrDefault();
                    if (folder != null) folder.folder_order = folder_order;
                    if (folderNew != null) folderNew.folder_order = folder_order - 1;
                }
                    break;
            }
            Modify(folder, s => s.folder_id == folder.folder_id, "folder_order");
            Modify(folderNew, s => s.folder_id == folderNew.folder_id, "folder_order");
            return folderNew.folder_id;
        }

        /// <summary>
        ///  实现首页获取菜单树
        ///  isAll：是否查看全部结构
        /// </summary>
        /// <returns></returns>
        public List<T_PageFolderModel> GetMainMenu(string sm_code, bool isAll = false,bool isExcel=true)
        {
            List<T_PageFolder> strTemp = (isAll) ? Select(s => !string.IsNullOrEmpty(s.folder_name)).ToList() : HCQ2UI_Helper.Session.SysPermissSession.MenusList;
            List<T_PageFolderModel> folderModel = new List<T_PageFolderModel>();
            if (strTemp == null || strTemp.Count <= 0)
                return null;
            List<T_PageFolder> list = new List<T_PageFolder>();
            if (string.IsNullOrEmpty(sm_code) || !sm_code.Equals(HCQ2_Common.Constant.MainDateConstant.mainPageType, StringComparison.InvariantCultureIgnoreCase))
            {
                var str =(isExcel)? strTemp.Where(s => s.sm_code.Equals(sm_code) && s.folder_type != "childPage") : strTemp.Where(s => s.sm_code.Equals(sm_code));
                if (null != str)
                    list = str.ToList();
            }
            else
            {
                //首页菜单数据
                List<T_SysModule> mList = HCQ2UI_Helper.Session.SysPermissSession.SysModule;
                if (mList == null) return null;
                List<string> cList = mList.Select(s => s.sm_code).ToList();
                list = (isExcel) ? strTemp.Where(s => cList.Contains(s.sm_code) && s.folder_type != "childPage").ToList() : strTemp.Where(s => cList.Contains(s.sm_code)).ToList();   
            }
            if (null == list || list.Count<=0)
                return null;
            List<T_PageFolder> baseList = list.FindAll(s => s.folder_pid == 0);
            if (baseList.Count > 0)
            {
                foreach (T_PageFolder item in baseList)
                {
                    folderModel.Add(new T_PageFolderModel()
                    {
                        folder_id = item.folder_id,
                        text = item.folder_name,
                        folder_url = item.folder_url,
                        folder_image = item.folder_image,
                        folder_order = HCQ2_Common.Helper.ToInt(item.folder_order),
                        have_child = item.have_child,
                        sm_code=item.sm_code,
                        nodes = GetMenuById(list, item.folder_id)
                    });
                }
            }
            return folderModel;
        }

        /// <summary>
        ///  递归遍历子目录
        /// </summary>
        /// <param name="list"></param>
        /// <param name="folder_id"></param>
        /// <returns></returns>
        private List<T_PageFolderModel> GetMenuById(List<T_PageFolder> list, int folder_id)
        {
            List<T_PageFolderModel> listModel = new List<T_PageFolderModel>();
            List<T_PageFolder> listKey = list.FindAll(s => s.folder_pid == folder_id);
            if (listKey.Count > 0)
            {
                T_PageFolderModel temp = null;
                for (int i = 0; i < listKey.Count; i++)
                {
                    temp = new T_PageFolderModel();
                    temp.folder_id = listKey[i].folder_id;
                    temp.text = listKey[i].folder_name;
                    temp.folder_order = HCQ2_Common.Helper.ToInt(listKey[i].folder_order);
                    temp.folder_url = listKey[i].folder_url;
                    temp.have_child = listKey[i].have_child;
                    temp.sm_code = listKey[i].sm_code;
                    if (temp.have_child)
                        temp.nodes = GetMenuById(list, listKey[i].folder_id);
                    listModel.Add(temp);
                }
            }
            return listModel;
        }
    }
}