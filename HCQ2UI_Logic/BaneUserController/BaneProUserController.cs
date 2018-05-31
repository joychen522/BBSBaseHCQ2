using HCQ2_Common;
using HCQ2_Model.BaneUser;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using HCQ2_Model.ViewModel;
using HCQ2_Model;
using System;
using System.IO;
using HCQ2_Common.Upload;
using System.Linq;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  尿检业务控制器
    /// </summary>
    public class BaneProUserController: BaseLogic
    {
        //****************************1.定期尿检一栏页面********************************

        #region 1.0 办理戒毒首次进入 + ActionResult ProUserList()
        /// <summary>
        ///  办理戒毒首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult ProUserList()
        {
            var query = HCQ2UI_Helper.Session.SysPermissSession.PermissList.Where(s => s.per_type == "fieldManager").Count();
            if (query > 0)
                ViewBag.isBaneShow = 1;
            else
                ViewBag.isBaneShow = 0;
            return View();
        }
        #endregion

        #region 1.1 获取用户一栏数据 +ActionResult GetBaneUserData()
        /// <summary>
        ///  获取用户一栏数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBaneUserData()
        {
            string keyword = RequestHelper.GetDeStrByName("baneName"),//用户名
                baneType = RequestHelper.GetDeStrByName("baneType"),//人员类别，戒毒，康复
                folder_path = RequestHelper.GetStrByName("folder_path"),
                baneTask = RequestHelper.GetStrByName("baneTask"),
                queryType = RequestHelper.GetStrByName("queryType"),//查询类别
                banedays = RequestHelper.GetStrByName("banedays");
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]),
               orgId = RequestHelper.GetIntByName("orgId"),//组织机构ID
               isParent = RequestHelper.GetIntByName("isParent");
            keyword = (!string.IsNullOrEmpty(keyword)) ? HttpUtility.UrlDecode(keyword) : keyword;
            BaneListParams param = new BaneListParams(operateContext.Usr.user_id,keyword, baneType, "", folder_path, (isParent > 0 ? true : false), orgId, page, rows, banedays, queryType,baneTask);
            List<BaneProModel> bane = operateContext.bllSession.Bane_User.GetBaneProData(param);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.Bane_User.GetBaneProDataCount(param),
                rows = bane
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 根据ID获取尿检记录数据 + ActionResult GetProEditDataById(int id)
        /// <summary>
        ///  1.2 根据ID获取尿检记录数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProEditDataById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            //List<Bane_UrinalysisRecord> record = operateContext.bllSession.Bane_UrinalysisRecord.Select(s => s.ur_id == id && s.approve_status==0);
            BaneEditUrinalyRecord reslut = operateContext.bllSession.Bane_UrinalysisRecord.GetRecordData(id);
            var query = HCQ2UI_Helper.Session.SysPermissSession.PermissList.Where(s => s.per_type == "fieldManager").Count();
            if (reslut == null)
                return operateContext.RedirectAjax(1, "记录为空~", "", "");
            return operateContext.RedirectAjax(0, "获取数据成功~", reslut, query > 0 ? "1" : "0");
        }
        #endregion

        #region 1.3 编辑尿检记录 + ActionResult EditUrinalysisRecord(BaneUrinalysisRecordModel model)
        /// <summary>
        ///  编辑尿检记录
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUrinalysisRecord(BaneUrinalysisRecordModel model)
        {
            //string fileName;
            try
            {
                //string url = UpLoadFile(out fileName,model.ur_attach);
                //if(string.IsNullOrEmpty(url))
                //    return operateContext.RedirectAjax(1, "请上传现场检测记录附件~", "", "");
                int approveStatus = RequestHelper.GetIntByName("approveStatus");
                Bane_UrinalysisRecord record = new Bane_UrinalysisRecord
                {
                    //ur_file_name = fileName,
                    //ur_attach = url,
                    ur_manager = model.ur_manager,
                    ur_result = model.ur_result,
                    approve_status = (approveStatus>0)? approveStatus: model.approve_status,
                    ur_reality_date = Convert.ToDateTime(model.ur_reality_date),
                    ur_code = model.ur_code,
                    ur_site = model.ur_site,
                    ur_method = model.ur_method,
                    ur_input_date = DateTime.ParseExact(model.ur_input_date, "yyyy-MM-dd", new System.Globalization.CultureInfo("zh-CN"))
                };
                operateContext.bllSession.Bane_UrinalysisRecord.Modify(record, s => s.ur_id == model.ur_id,  "ur_manager", "ur_result", "approve_status", "ur_reality_date", "ur_code", "ur_site", "ur_method", "ur_input_date");
                return operateContext.RedirectAjax(0, "编辑记录成功~", "", "");
            }               
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 1.4 处理上传文件 + string UpLoadFile(out string file_name, string ur_attach)
        /// <summary>
        ///  处理上传文件
        /// </summary>
        private string UpLoadFile(out string file_name, string ur_attach)
        {
            #region 1.0 处理上传文件夹
            file_name = string.Empty;
            string path = Server.MapPath("~/UpFile/BaneUser"), sourceUrl = string.Empty;
            if (!Directory.Exists(path.ToString()))
                Directory.CreateDirectory(path.ToString());//文件夹不存在则创建
            #endregion
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmssff") + uploadHelper.CreateRandomCode(8);

            //删除之前上传的文件
            if (!string.IsNullOrEmpty(ur_attach))
                System.IO.File.Delete(Server.MapPath(ur_attach));

            #region 图片
            // 如果在插件中定义可以上传原始图片的话，可在此处理，否则可以忽略。
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];//__avatar1
                //原始图片的文件名
                string sourceFileName = file.FileName;
                //原始文件的扩展名
                string sourceExtendName = sourceFileName.Substring(sourceFileName.LastIndexOf('.') + 1);
                file_name = sourceFileName + "." + sourceExtendName;
                sourceUrl = string.Format("~/UpFile/BaneUser/csharp_source_{0}.{1}", fileName, sourceExtendName);
                file.SaveAs(Server.MapPath(sourceUrl));//上传
            }
            #endregion
            return sourceUrl;
        }
        #endregion

        #region 1.5 删除检测数据 +ActionResult DelBaneProUserById()
        /// <summary>
        ///  删除检测数据
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelBaneProUserById(int id)
        {
            if (id<=0)
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            int mark= operateContext.bllSession.Bane_UrinalysisRecord.Delete(s => s.ur_id == id);
            if (mark > 0)
            {
                operateContext.bllSession.Bane_LogDetail.Add(new Bane_LogDetail { user_id = operateContext.Usr.user_id, user_name = operateContext.Usr.user_name, log_type = "删除检测记录", log_ip = RequestHelper.GetIP, log_title = "删除检测记录", log_context = "删除检测记录ID：" + id, log_date = DateTime.Now });
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            }
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion
    }
}
