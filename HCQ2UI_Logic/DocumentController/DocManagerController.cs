using Aspose.Cells;
using Aspose.Slides.Pptx;
using System;
using System.Collections.Generic;
using System.Web;
using HCQ2_Common;
using System.Web.Mvc;
using HCQ2_Model.DocModel;
using HCQ2_Model;
using System.IO;
using HCQ2_Model.ViewModel;
using System.Data;
using System.Text;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  文档管理控制器
    ///  创建人：Joychen
    ///  创建时间：2017-5-22
    /// </summary>
    public class DocManagerController : BaseLogic
    {
        //private string outPutFileUrl = "";
        //**************************1.0 文档目录树管理*****************************
        #region 1.0 默认首次进入文档管理界面 + ActionResult DocList()
        /// <summary>
        ///  1.0 默认首次进入文档管理界面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        [HttpGet]
        public ActionResult DocList()
        {
            List<T_Permissions> list = HCQ2UI_Helper.Session.SysPermissSession.PermissList.FindAll(s => s.per_type.Equals("docSystem"));
            ViewBag.isdocManager = (null == list || list.Count <= 0) ? false : true;
            ViewBag.userID = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
            return View();
        }
        #endregion

        #region 1.1 获取文档结构树 数据 + ActionResult GetDocTreeData()
        /// <summary>
        ///  1.1 获取文档结构树 数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDocTreeData()
        {
            string pageType = RequestHelper.GetStrByName("pageType");//页面类别
            int doc_type = RequestHelper.GetIntByName("doc_type");//页面
            return Json(operateContext.bllSession.T_DocumentFolder.GetDocTreeData(pageType, doc_type));
        }
        #endregion

        #region 1.2 添加节点 + ActionResult AddNode(DocTreeModel model)
        /// <summary>
        ///  1.2 添加节点
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNode()
        {
            int pId = RequestHelper.GetIntByName("pId");
            string name = RequestHelper.GetDeStrByName("name"),
                pageType = RequestHelper.GetStrByName("pageType");
            DocTreeModel model = new DocTreeModel() { pId = pId, name = name,pageType= pageType };
            int folder_id = operateContext.bllSession.T_DocumentFolder.AddNode(model);
            if (folder_id>0)
                return operateContext.RedirectAjax(0, "添加成功~", folder_id, "");
            return operateContext.RedirectAjax(1, "数据添加失败~", "", "");
        }
        #endregion

        #region 1.3 编辑节点 + ActionResult EditNode()
        /// <summary>
        ///  1.3 编辑节点 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditNode()
        {
            int id = RequestHelper.GetIntByName("id");
            string name = RequestHelper.GetDeStrByName("name");
            int mark = operateContext.bllSession.T_DocumentFolder.EditNode(id,name);
            if (mark > 0)
                return operateContext.RedirectAjax(0, "编辑成功~", "", "");
            return operateContext.RedirectAjax(1, "数据编辑失败~", "", "");
        }
        #endregion
         
        #region 1.4 删除节点 + ActionResult DeleteNode(int id)
        /// <summary>
        ///  1.4 删除节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteNode(int id)
        {
            List<T_DocumentFolder> list = operateContext.bllSession.T_DocumentFolder.Select(s => s.folder_id == id);
            int mark = operateContext.bllSession.T_DocumentFolder.DeleteNode(list[0],id);
            if (mark > 0)
                return operateContext.RedirectAjax(0, "删除成功~", "", "");
            return operateContext.RedirectAjax(1, "需要删除的记录不存在或已删除~", "", "");
        }
        #endregion

        //**************************Table处理************************************
        #region 2.0 初始化Table + ActionResult InitTable()
        /// <summary>
        ///  2.0 初始化Table
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InitTable(DocTableParamModel model)
        {
            if (model.folder_id == 0)
                return null;
            int total = 0;
            model.user_id = operateContext.Usr.user_id;
            List<DocTreeResultModel> list = operateContext.bllSession.T_DocumentInfo.GetTableData(model, out total);
            TableModel tModel = new TableModel()
            {
                total = total,
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 2.1 彻底删除文档 + ActionResult DelNodeById(int id)
        /// <summary>
        ///  2.1 彻底删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelNodeById(int id)
        {
            if(id<=0)
                return operateContext.RedirectAjax(1, "关键字不允许为空~", "", "");
            int mark = operateContext.bllSession.T_DocumentInfo.Delete(s => s.file_id == id);
            if(mark<=0) 
                return operateContext.RedirectAjax(1, "文档删除失败~", "", "");
            //1.删除文档-目录树对于关系
            operateContext.bllSession.T_DocumentFolderRelation.Delete(s => s.file_id == id);
            //2.删除分享设置
            operateContext.bllSession.T_DocumentSetType.Delete(s => s.file_id == id);
            return operateContext.RedirectAjax(0, "文档删除成功~", "", "");
        }
        #endregion

        #region 2.2 删除文档至回收站 + ActionResult RemoveNodeById(int id)
        /// <summary>
        ///  2.2 删除文档至回收站
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveNodeById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "关键字不允许为空~", "", "");
            int mark = operateContext.bllSession.T_DocumentInfo.Modify(new T_DocumentInfo { file_id = id, if_remove = true }, s => s.file_id == id, "if_remove");
            if (mark <= 0)
                return operateContext.RedirectAjax(1, "文档下架失败~", "", "");
            return operateContext.RedirectAjax(0, "文档成功下架~", "", "");
        }
        #endregion

        #region 2.3 文档恢复 +  ActionResult RecoverNodeById(int id)
        /// <summary>
        ///  2.3 文档恢复
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RecoverNodeById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "关键字不允许为空~", "", "");
            int mark = operateContext.bllSession.T_DocumentInfo.Modify(new T_DocumentInfo { if_remove = false }, s => s.file_id == id, "if_remove");
            if (mark <= 0)
                return operateContext.RedirectAjax(1, "文档恢复失败~", "", "");
            return operateContext.RedirectAjax(0, "文档恢复成功~", "", "");
        }
        #endregion

        #region 2.4 我的分享-撤销 + ActionResult RemoveMyShareById(int id)
        /// <summary>
        ///  2.4 我的分享-撤销
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveMyShareById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "关键字不允许为空~", "", "");
            int mark = operateContext.bllSession.T_DocumentSetType.Delete(s => s.file_id == id);
            if (mark <= 0)
                return operateContext.RedirectAjax(1, "撤销分享失败~", "", "");
            return operateContext.RedirectAjax(0, "撤销分享成功~", "", "");
        }
        #endregion

        #region 2.5 收到的分享-撤销 + ActionResult RemoveFileToMeById(int id)
        /// <summary>
        ///  2.5 收到的分享-撤销
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveFileToMeById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "关键字不允许为空~", "", "");
            int mark = operateContext.bllSession.T_DocumentSetType.Delete(s => s.file_id == id && s.user_id == operateContext.Usr.user_id);
            if (mark <= 0)
                return operateContext.RedirectAjax(1, "文档不存在或者分享给的是角色，您不能删除~", "", "");
            return operateContext.RedirectAjax(0, "撤销分享成功~", "", "");
        }
        #endregion

        #region 2.6 审核文档 + ActionResult ApproveFileByID(int id)
        /// <summary>
        ///  2.6 审核文档
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ApproveFileByID(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "关键字不允许为空~", "", "");
            int mark = operateContext.bllSession.T_DocumentInfo.Modify(new T_DocumentInfo { file_status = 5 }, s => s.file_id == id, "file_status");
            if (mark <= 0)
                return operateContext.RedirectAjax(1, "审核失败，数据异常~", "", "");
            return operateContext.RedirectAjax(0, "审核成功通过~", "", "");
        } 
        #endregion

        //**************************2.0 文档目录树--权限管理*********************
        #region 2.0 首次进入权限管理 + ActionResult DocLimitList()
        /// <summary>
        ///  2.0 首次进入权限管理
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult DocLimitList()
        {
            return View();
        }
        #endregion

        #region 2.1 保存文档目录-权限记录 + ActionResult SaveDocLimitData(int id)
        /// <summary>
        ///  2.1 保存文档目录-权限记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDocLimitData(int id)
        {
            string menus = RequestHelper.GetStrByName("menuData"); 
            string reak = RequestHelper.GetStrByName("reak");//是否处理标记
            //清理菜单缓存
            //CacheHelper.RemoveCache(CacheConstant.loginUserCacheMenus + operateContext.Usr.user_id);
            try
            {
                bool back = operateContext.bllSession.T_DocFolderPermissRelation.SaveDocMenuLimitData(menus, reak, id);
                if (back)
                    return operateContext.RedirectAjax(0, "保存数据成功~", "", "");
                return operateContext.RedirectAjax(1, "保存数据失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 2.2 根据权限ID获取文档菜单项 +ActionResult GetDocMenuLimitData(int id)
        /// <summary>
        ///  根据权限ID获取文档菜单项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDocMenuLimitData(int id)
        {
            List<HCQ2_Model.T_DocFolderPermissRelation> list =
                operateContext.bllSession.T_DocFolderPermissRelation.GetDocMenuLimitData(id);
            if (null != list)
                return operateContext.RedirectAjax(0, "获取数据成功~", list, "");
            return operateContext.RedirectAjax(1, "获取数据失败~", "", "");
        }
        #endregion

        #region 2.3 获取子节点 +ActionResult GetDocMenuChildsByParentID()
        /// <summary>
        ///  获取子节点
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDocMenuChildsByParentID()
        {
            int id = RequestHelper.GetIntByName("pid");
            string type = RequestHelper.GetStrByName("type");
            List<HCQ2_Model.TreeModel.TreeTableAttribute> list =
                operateContext.bllSession.T_DocumentFolder.GetMenuDataByPid(id, type);
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("nodes", list);
            return Json(map);
        }
        #endregion

        //**************************3.0 系统管理员文档目录管理*****************************
        #region 3.0 添加系统节点 + ActionResult AddSysNode(T_DocumentFolder folder)
        /// <summary>
        ///  3.0 添加系统节点
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddSysNode(DocTreeModel folder)
        {
            DocTreeModel mark = operateContext.bllSession.T_DocumentFolder.AddSysNode(folder);
            if (null!= mark)
                return operateContext.RedirectAjax(0, "添加成功~", mark, "");
            return operateContext.RedirectAjax(1, "数据添加失败~", "", "");
        }
        #endregion

        #region 3.1 编辑系统节点 + ActionResult EditSysNode(T_DocumentFolder folder)
        /// <summary>
        ///  3.1 编辑系统节点 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSysNode(DocTreeModel folder,int id)
        {
            if(id<=0)
                return operateContext.RedirectAjax(1, "参数异常~", "", "");
            folder.id = id;
            DocTreeModel mark = operateContext.bllSession.T_DocumentFolder.EditSysNode(folder);
            if (null!= mark)
                return operateContext.RedirectAjax(0, "编辑成功~", mark, "");
            return operateContext.RedirectAjax(1, "数据编辑失败~", "", "");
        }
        #endregion

        //**************************4.0 批量上传文档管理*****************************
        #region 4.0 上传页面 + ActionResult DocUpLoadFile()
        /// <summary>
        ///  4.0 上传页面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult DocUpLoadFile()
        {
            return View();
        }
        #endregion

        #region 4.1批量上传文档 + ActionResult UpLoadFile()
        /// <summary>
        ///  4.1批量上传文档
        /// </summary>
        /// <param name="id">folder_id目录树ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpLoadFile(T_DocumentInfo model,int id)
        {
            var files = Request.Files;
            if(id<=0 || files == null || files.Count<=0)
                return null;
            string file_name = RequestHelper.GetStrByName("file_name");
            #region 1.0 处理上传文件夹
            string pathServer = "~/UpFile/DocManager/" + DateTime.Now.ToString("yyyy-MM") + "/" + operateContext.Usr.user_name;
            string path = Server.MapPath(pathServer);//文档存放路径：~/UpFile/DocManager/2017-05/系统管理
            if (!Directory.Exists(path.ToString()))
                Directory.CreateDirectory(path.ToString());//文件夹不存在则创建
            #endregion

            #region 2.0 处理文档
            int file_id = 0;
            for (int i=0;i< files.Count;i++)
            {
                HttpPostedFileBase file = files[i];
                if (null == file)
                    continue;
                //1.上传文档
                file.SaveAs(Server.MapPath(pathServer + "/" + file.FileName));//上传文件
                //2.保存文档信息
                string fileName = file.FileName.Split('.')[0];//文件名
                string oldName = file.FileName;
                if (string.IsNullOrEmpty(model.file_name)) model.file_name = fileName;
                if (string.IsNullOrEmpty(model.alias_name)) model.alias_name = fileName;
                model.file_type = oldName.Substring(oldName.LastIndexOf('.') + 1);//文档类型
                model.file_size = Convert.ToDecimal(Math.Round(Convert.ToDouble(file.ContentLength / 1024), 2));//文件大小
                model.attach_url = pathServer + "/" + oldName;
                model.create_id = operateContext.Usr.user_id;
                model.create_name = operateContext.Usr.user_name;
                model.create_time = DateTime.Now;
                file_id = operateContext.bllSession.T_DocumentInfo.AddDocInfo(model);
                //3.保存文档-节点对应关系
                if (file_id > 0)
                    operateContext.bllSession.T_DocumentFolderRelation.AddDocFolderRelation(new T_DocumentFolderRelation { file_id = file_id, folder_id = id, create_id = operateContext.Usr.user_id });
            }
            #endregion
            if (file_id > 0)
                return operateContext.RedirectAjax(0, "文档上传成功~", "", "");
            return operateContext.RedirectAjax(1, "文档上传失败~", "", "");
        }
        #endregion

        #region 4.2.1 编辑前先获取文档数据 + ActionResult GetNodeDataById(int id)
        /// <summary>
        ///  4.2.1 编辑前先获取文档数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNodeDataById(int id)
        {
            if(id<=0)
                return operateContext.RedirectAjax(1, "参数异常~", "", "");
            List<T_DocumentInfo> list = operateContext.bllSession.T_DocumentInfo.Select(s => s.file_id == id);
            if (list==null || list.Count<=0)
                return operateContext.RedirectAjax(1, "文档数据获取失败~", "", "");
            return operateContext.RedirectAjax(0, "文档数据获取成功~", list[0], "");
        } 
        #endregion

        #region 4.2.2 编辑文档 + ActionResult EditLoadFile(T_DocumentInfo model, int id)
        /// <summary>
        ///  4.2.2 编辑文档
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id">file_id文档id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditLoadFile(T_DocumentInfo model, int id)
        {
            //编辑文本
            int isUpload = RequestHelper.GetIntByName("isUpload");
            int mark = 0;
            #region 有文件上传的编辑
            if (isUpload > 0)
            {
                var files = Request.Files;
                if (id <= 0 || files == null || files.Count <= 0)
                    return null;
                string file_name = RequestHelper.GetStrByName("file_name");
                #region 1.0 处理上传文件夹
                string pathServer = "~/UpFile/DocManager/" + DateTime.Now.ToString("yyyy-MM") + "/" + operateContext.Usr.user_name;
                string path = Server.MapPath(pathServer);//文档存放路径：~/UpFile/DocManager/2017-05/系统管理
                if (!Directory.Exists(path.ToString()))
                    Directory.CreateDirectory(path.ToString());//文件夹不存在则创建
                #endregion

                #region 1.1 查询得到原来上传文档 并删除

                List<T_DocumentInfo> list = operateContext.bllSession.T_DocumentInfo.Select(s => s.file_id == id);
                if (list != null && list.Count > 0)
                {
                    string url = list[0].attach_url;
                    string urlPath = (!string.IsNullOrEmpty(url)) ? Server.MapPath(url) : "";
                    if (System.IO.File.Exists(urlPath))
                        System.IO.File.Delete(urlPath);
                }
                #endregion

                #region 2.0 处理文档
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    if (null == file)
                        continue;
                    //1.上传文档
                    file.SaveAs(Server.MapPath(pathServer + "/" + file.FileName));//上传文件
                    //2.保存文档信息
                    string fileName = file.FileName.Split('.')[0];//文件名
                    string oldName = file.FileName;
                    if (string.IsNullOrEmpty(model.file_name)) model.file_name = fileName;
                    if (string.IsNullOrEmpty(model.alias_name)) model.alias_name = fileName;
                    model.file_type = oldName.Substring(oldName.LastIndexOf('.') + 1);//文档类型
                    model.file_size = Convert.ToDecimal(Math.Round(Convert.ToDouble(file.ContentLength / 1024), 2));//文件大小
                    model.attach_url = pathServer + "/" + oldName;
                    mark = operateContext.bllSession.T_DocumentInfo.Modify(model, s => s.file_id == id, "file_name", "alias_name", "file_type", "file_size", "doc_type", "issue_start", "doc_number", "attach_url", "file_note");
                }
                #endregion
            }
            #endregion

            #region 没有上传编辑文件的 编辑
            else
            {
                mark = operateContext.bllSession.T_DocumentInfo.Modify(model, s => s.file_id == id, "file_name", "alias_name", "font_type", "issue_start", "doc_type", "doc_number", "file_note", "file_money", "file_classify", "file_status");
            }
            #endregion
            if (mark > 0)
                return operateContext.RedirectAjax(0, "文档上传成功~", "", "");
            return operateContext.RedirectAjax(1, "文档上传失败~", "", "");
        } 
        #endregion

        //**************************5.0 分享文档相关操作*****************************
        #region 5.1 获取分享给人员数据 + ActionResult GetShareDataByPerson()
        /// <summary>
        ///  5.1 获取分享给人员数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetShareDataByPerson()
        {
            List<HCQ2_Model.SelectModel.ListBoxModel> list = operateContext.bllSession.T_DocumentSetType.GetShareDataByPerson();
            return operateContext.RedirectAjax(0, "数据获取成功~", list, "");
        }
        #endregion

        #region 5.2 保存分享给人员指定数据 + ActionResult SaveDataByPerson()
        /// <summary>
        ///  5.2 保存分享给人员指定数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDataByPerson()
        {
            string personData = RequestHelper.GetStrByName("personData");
            int file_id = RequestHelper.GetIntByName("file_id");
            if (string.IsNullOrEmpty(personData))
                return operateContext.RedirectAjax(1, "数据不能为空~", "", "");
            bool mark = operateContext.bllSession.T_DocumentSetType.SaveShareDataByPerson(personData, file_id);
            if (mark)
                return operateContext.RedirectAjax(0, "文档分享成功~", "", "");
            return operateContext.RedirectAjax(1, "文档分享失败~", "", "");
        }
        #endregion

        #region  5.3 获取分享给角色数据 + ActionResult GetShareDataByRole()
        /// <summary>
        ///  5.3 获取分享给角色数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetShareDataByRole()
        {
            List<HCQ2_Model.SelectModel.ListBoxModel> list = operateContext.bllSession.T_DocumentSetType.GetShareDataByRole();
            return operateContext.RedirectAjax(0, "数据获取成功~", list, "");
        }
        #endregion

        #region 5.4 保存分享给角色数据 + ActionResult SaveDataByRole()
        /// <summary>
        ///  5.4 保存分享给角色数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDataByRole()
        {
            string personData = RequestHelper.GetStrByName("personData");
            int file_id = RequestHelper.GetIntByName("file_id");
            if (string.IsNullOrEmpty(personData))
                return operateContext.RedirectAjax(1, "数据不能为空~", "", "");
            bool mark = operateContext.bllSession.T_DocumentSetType.SaveShareDataByRole(personData, file_id);
            if(mark)
                return operateContext.RedirectAjax(0, "文档分享成功~", "", "");
            return operateContext.RedirectAjax(1, "文档分享失败~", "", "");
        }
        #endregion

        //**************************6.0 文档在线预览*****************************
        #region 6.0 在线预览Word文档
        /// <summary>
        ///  6.0 在线预览Word文档
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string CourseViewOnLine()
        {
            //原始文件路口
            string fileName = RequestHelper.GetDeStrByName("fileName");
            if (string.IsNullOrEmpty(fileName))
                return null;
            fileName = Server.MapPath(fileName);
            //DataTable dtlist = new DataTable();
            //dtlist.Columns.Add("TempDocHtml", typeof(string));
            //临时文件生成的路径
            string fileDire = "~/TempFile/"+operateContext.Usr.login_name+ "/viewFiles";
            ///Files\ppttest.pptx
            //string sourceDoc = Path.Combine(fileDire, fileName);
            string tempDire = Server.MapPath(fileDire);
            if (!Directory.Exists(tempDire))
                Directory.CreateDirectory(tempDire);
            string saveDoc = "";
            //文件后缀
            string docExtendName = Path.GetExtension(fileName).ToLower();
            bool result = false;
            switch (docExtendName)
            {
                case ".pdf":
                case ".jpg":
                case ".gif":
                case ".png":
                    {
                        //拷贝
                        //fileName:原始位置，saveDoc：保存路径
                        saveDoc = Path.Combine(fileDire, "onlineview"+ docExtendName);
                        FileInfo file = new FileInfo(fileName);
                        if (file.Exists)
                        {
                            file.CopyTo(Server.MapPath(saveDoc), true);
                            result = true;
                        }
                    }
                    break;
                default:
                    {
                        saveDoc = Path.Combine(fileDire, "onlineview.html");
                        result = OfficeDocumentToHtml(fileName, Server.MapPath(saveDoc));
                    }break;
            }
            //if (docExtendName == ".pdf")
            //{
            //    //pdf模板文件
            //    string tempFile = Path.Combine(fileDire, "temppdf.html");
            //    saveDoc = Path.Combine(fileDire, "viewFiles/onlinepdf.html");
            //    result = PdfToHtml(fileName, Server.MapPath(tempFile),Server.MapPath(saveDoc));
            //}

            if (result)
                return saveDoc;
            return null;
        }
        #endregion
        private bool PdfToHtml(string fileName, string tempFile, string saveDoc)
        {
            //---------------------读html模板页面到stringbuilder对象里---- 
            StringBuilder htmltext = new StringBuilder();
            using (StreamReader sr = new StreamReader(fileName)) //模板页路径
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                    htmltext.Append(line);
                sr.Close();
            }
            fileName = fileName.Replace("\\", "/");
            //----------替换htm里的标记为你想加的内容 
            htmltext.Replace("$PDFFILEPATH", fileName);
            //----------生成htm文件------------------―― 
            using (StreamWriter sw = new StreamWriter(saveDoc, false,Encoding.GetEncoding("utf-8"))) //保存地址
            {
                sw.WriteLine(htmltext);
                sw.Flush();
                sw.Close();
            }
            return true;
        }
        private bool OfficeDocumentToHtml(string sourceDoc, string saveDoc)
        {
            bool result = false;
            //获取文件扩展名
            string docExtendName = Path.GetExtension(sourceDoc).ToLower();
            switch (docExtendName)
            {
                case ".doc":
                case ".docx":
                    Aspose.Words.Document doc = new Aspose.Words.Document(sourceDoc);
                    doc.Save(saveDoc, Aspose.Words.SaveFormat.Html);
                    result = true;
                    break;
                case ".xls":
                case ".xlsx":
                    Workbook workbook = new Workbook(sourceDoc);
                    workbook.Save(saveDoc, SaveFormat.Html);
                    result = true;
                    break;
                case ".ppt":
                case ".pptx":
                    PresentationEx pres = new PresentationEx(sourceDoc);
                    pres.Save(saveDoc, Aspose.Slides.Export.SaveFormat.Html);
                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
