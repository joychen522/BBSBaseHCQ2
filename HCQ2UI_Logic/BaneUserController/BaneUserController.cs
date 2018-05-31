using HCQ2_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HCQ2_Model.BaneUser;
using HCQ2_Model.ViewModel;
using HCQ2_Model;
using HCQ2_Model.UploadModel;
using System.IO;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  戒毒人员控制器
    /// </summary>
    public class BaneUserController:BaseLogic
    {
        //****************************1.戒毒人员一栏页面********************************
        #region 1.0 戒毒人员页面首次进入 + ActionResult UserList()
        /// <summary>
        ///  戒毒人员页面首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult UserList()
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
                baneEnd = RequestHelper.GetDeStrByName("baneEnd"),//结束监管原因
                folder_path = RequestHelper.GetStrByName("folder_path");
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]),
               orgId = RequestHelper.GetIntByName("orgId"),//组织机构ID
               isParent = RequestHelper.GetIntByName("isParent");
            keyword = (!string.IsNullOrEmpty(keyword)) ? HttpUtility.UrlDecode(keyword) : keyword;
            BaneListParams param = new BaneListParams(operateContext.Usr.user_id,keyword, baneType, baneEnd, folder_path, (isParent>0?true:false), orgId, page, rows, "", "");
            List<BaneListModel> bane = base.operateContext.bllSession.Bane_User.GetBaneData(param);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.Bane_User.GetGetBaneDataCount(param),
                rows = bane
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 删除用户数据 +ActionResult DelBaneUserById()
        /// <summary>
        ///  删除用户数据
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelBaneUserById()
        {
            string user_identify = RequestHelper.GetStrByName("user_identify");
            if (string.IsNullOrEmpty(user_identify))
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            //1.删除戒毒用户表
            int delCount = operateContext.bllSession.Bane_User.Delete(s=>s.user_identify== user_identify);
            //2.删除戒毒情况表
            operateContext.bllSession.Bane_RecoveryInfo.Delete(s => s.user_identify == user_identify);
            //3.删除个人犯罪记录表
            operateContext.bllSession.Bane_CriminalRecord.Delete(s => s.user_identify == user_identify);
            //4.删除家庭成员表
            operateContext.bllSession.Bane_FamilyRecord.Delete(s => s.user_identify == user_identify);
            //5.删除定期尿检记录表
            operateContext.bllSession.Bane_UrinalysisRecord.Delete(s => s.user_identify == user_identify);
            if (delCount > 0)
            {
                operateContext.bllSession.Bane_LogDetail.Add(new Bane_LogDetail { user_id = operateContext.Usr.user_id, user_name = operateContext.Usr.user_name, log_type = "删除禁毒人员", log_ip = RequestHelper.GetIP, log_title = "删除用户", log_context = "删除禁毒人员："+ user_identify, log_date = DateTime.Now });
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            }               
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion

        //****************************2.添加戒毒人员页面********************************
        #region 2.0 添加戒毒人员页面首次进入 + ActionResult UserList()
        /// <summary>
        ///  添加戒毒人员页面首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult AddBaneUser()
        {
            return View();
        }
        #endregion

        #region 2.1 添加用户 + ActionResult AddUser(BaneAddModel user, BaneRecoveryModel model)
        /// <summary>
        ///  添加用户
        /// </summary>
        /// <param name="user">用户基本对象</param>
        /// <param name="model">戒毒情况对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUser(BaneAddModel user, BaneRecoveryModel model)
        {
            ModelState.Remove("user_id");
            ModelState.Remove("ri_id");
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try
            {
                //添加之前判断身份证是否被占用
                List<Bane_User> usr =
                    operateContext.bllSession.Bane_User.Select(
                        s => s.user_identify.Equals(user.user_identify, StringComparison.CurrentCultureIgnoreCase));
                if (null != usr && usr.Count > 0)
                    return operateContext.RedirectAjax(1, "添加用户失败，当前身份证已存在！", "", "");
                bool Isback = operateContext.bllSession.Bane_User.AddUser(user,model);
                operateContext.bllSession.Bane_LogDetail.Add(new Bane_LogDetail { user_id = operateContext.Usr.user_id, user_name = operateContext.Usr.user_name, log_type = "添加禁毒人员", log_ip = RequestHelper.GetIP, log_title = "添加用户", log_context = "添加禁毒人员：" + user.user_identify, log_date = DateTime.Now });
                if (Isback)
                    return operateContext.RedirectAjax(0, "添加用户成功~", "", "");
                return operateContext.RedirectAjax(1, "添加用户失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 2.2 编辑用户数据 + ActionResult EditUser(BaneAddModel user, BaneRecoveryModel model)
        /// <summary>
        ///  编辑用户数据
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(BaneAddModel user, BaneRecoveryModel model)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            bool IsBack = true;
            //编辑身份证之前判断是否被占用
            List<HCQ2_Model.Bane_User> usr =
                    operateContext.bllSession.Bane_User.Select(
                        s => s.user_identify.Equals(user.user_identify, StringComparison.CurrentCultureIgnoreCase) && s.user_id != user.user_id);
            if (null != usr && usr.Count > 0 && usr[0].user_id != user.user_id)
                return operateContext.RedirectAjax(1, "编辑用户失败，身份证已被占用请重新设置！", "", "");
            try
            {
                IsBack = operateContext.bllSession.Bane_User.EditUser(user, model);
                operateContext.bllSession.Bane_LogDetail.Add(new Bane_LogDetail { user_id = operateContext.Usr.user_id, user_name = operateContext.Usr.user_name, log_type = "编辑禁毒人员", log_ip = RequestHelper.GetIP, log_title = "编辑用户", log_context = "编辑禁毒人员：" + user.user_identify, log_date = DateTime.Now });
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
            if (IsBack)
                return operateContext.RedirectAjax(0, "编辑用户成功~", "", "");
            return operateContext.RedirectAjax(1, "编辑用户失败~", "", "");
        }
        #endregion

        #region 2.3 获取人员数据 + ActionResult GetUserById()
        /// <summary>
        ///  2.3 获取人员数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserById()
        {
            string user_identify = RequestHelper.GetStrByName("user_identify");
            if (string.IsNullOrEmpty(user_identify))
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            BaneAddUser model = operateContext.bllSession.Bane_User.GetBaneUser(user_identify);
            return operateContext.RedirectAjax(0, "数据删除成功~", model, "");
        }
        #endregion

        #region 2.4 上传头像 + ActionResult UploadFile()
        /// <summary>
        ///  2.4 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile()
        {
            Result result = new Result();
            result.avatarUrls = new List<string>();
            result.success = false;
            result.msg = "Failure!";
            //身份证作为文件名。
            string fileName = RequestHelper.GetStrByName("user_identify");
            Bane_User user = operateContext.bllSession.Bane_User.Select(s => s.user_identify == fileName).FirstOrDefault();
            int orgId = user.org_id;

            #region 处理上传文件夹

            string path = Server.MapPath("~/UpFile/BaneImg/"+ orgId.ToString());
            if (!Directory.Exists(path.ToString()))
                Directory.CreateDirectory(path.ToString());//文件夹不存在则创建

            #endregion

            #region 处理头像图片
            HttpPostedFileBase file = Request.Files["__source"];//__avatar1
            string[] avatars = new string[3] { "__avatar1", "__avatar2", "__avatar3" };
            int avatars_length = avatars.Length;
            for (int i = 0; i < avatars_length; i++)
            {
                file = Request.Files[avatars[i]];
                if (file == null)
                    break;
                //判断原来是否有上传过文件，有则删除
                if (user != null && !string.IsNullOrEmpty(user.user_photo))
                    System.IO.File.Delete(Server.MapPath(user.user_photo));
                string virtualPath = string.Format("~/UpFile/BaneImg/{0}/{1}.jpg", orgId.ToString(),fileName);
                result.avatarUrls.Add(virtualPath);
                file.SaveAs(Server.MapPath(virtualPath));
                user.user_photo = result.avatarUrls[0];
                operateContext.bllSession.Bane_User.Modify(user, s => s.user_identify == fileName, "user_photo");
            }

            #endregion
            result.success = true;
            result.msg = "Success!";
            //返回图片的保存结果（返回内容为json字符串，可自行构造，该处使用Newtonsoft.Json构造）
            return Json(result);
        } 
        #endregion

        //****************************3.违法犯罪记录--档案袋********************************
        #region 3.0 违法犯罪记录页面首次进入 + ActionResult CriminalList()
        /// <summary>
        ///  违法犯罪记录页面首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult CriminalList()
        {
            return View();
        }
        #endregion

        #region 3.1 获取违法犯罪一栏数据 +ActionResult GetCriminalBaneData()
        /// <summary>
        ///  获取违法犯罪一栏数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCriminalBaneData()
        {
            string user_identify = RequestHelper.GetDeStrByName("user_identify");
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]);
            if(string.IsNullOrEmpty(user_identify))
                return operateContext.RedirectAjax(1, "必传参数异常！", "", "");
            List<Bane_CriminalRecord> bane = operateContext.bllSession.Bane_CriminalRecord.Select(s => s.user_identify.Equals(user_identify), s => s.cr_id, page, rows, true);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.Bane_CriminalRecord.SelectCount(s => s.user_identify.Equals(user_identify)),
                rows = bane
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 3.2 添加违法犯罪记录 + ActionResult CriminalRecord(BaneCriminalModel model)
        /// <summary>
        ///  添加违法犯罪记录
        /// </summary>
        /// <param name="user">违法犯罪对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCriminalRecord(BaneCriminalModel model)
        {
            ModelState.Remove("cr_id");
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try
            {
                //添加之前判断身份证是否被占用
                bool Isback = operateContext.bllSession.Bane_CriminalRecord.AddCriminalUser(model);
                if (Isback)
                    return operateContext.RedirectAjax(0, "添加数据成功~", "", "");
                return operateContext.RedirectAjax(1, "添加数据失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 3.3 编辑违法犯罪数据 + ActionResult EditCriminalRecord(BaneCriminalModel model)
        /// <summary>
        ///  编辑违法犯罪数据
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCriminalRecord(BaneCriminalModel model)
        {

            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            bool IsBack = true;
            try
            {
                IsBack = operateContext.bllSession.Bane_CriminalRecord.EditCriminalUser(model);
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
            if (IsBack)
                return operateContext.RedirectAjax(0, "编辑用户成功~", "", "");
            return operateContext.RedirectAjax(1, "编辑用户失败~", "", "");
        }
        #endregion

        #region 3.4 删除违法犯罪数据 +ActionResult DelCriminalBaneById()
        /// <summary>
        ///  删除违法犯罪数据
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelCriminalBaneById(int id)
        {
            if (id<=0)
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            int delCount = operateContext.bllSession.Bane_CriminalRecord.Delete(s => s.cr_id == id);
            if (delCount > 0)
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion

        //****************************4.家庭成员--档案袋********************************
        #region 3.0 家庭成员&社会关系页面首次进入 + ActionResult FamilyList()
        /// <summary>
        ///  家庭成员&社会关系页面首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult FamilyList()
        {
            return View();
        }
        #endregion

        #region 3.1 家庭成员&社会关系一栏数据 +ActionResult GetFamilyBaneData()
        /// <summary>
        /// 家庭成员&社会关系一栏数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFamilyBaneData()
        {
            string user_identify = RequestHelper.GetStrByName("user_identify");
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]),
               fr_type = RequestHelper.GetIntByName("fr_type");//类别，0：家庭成员:1：社会关系
            if (string.IsNullOrEmpty(user_identify))
                return operateContext.RedirectAjax(1, "必传参数异常！", "", "");
            List<Bane_FamilyRecord> bane = operateContext.bllSession.Bane_FamilyRecord.Select(s => s.user_identify.Equals(user_identify) && s.fr_type==fr_type, s => s.fr_id, page, rows, true);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.Bane_FamilyRecord.SelectCount(s => s.user_identify.Equals(user_identify) && s.fr_type==fr_type),
                rows = bane
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 3.2 添加家庭成员&社会关系 + ActionResult AddFamilyRecord(BaneCriminalModel model)
        /// <summary>
        ///  添加家庭成员&社会关系
        /// </summary>
        /// <param name="user">违法犯罪对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddFamilyRecord(BaneFamilyRecordModel model)
        {
            ModelState.Remove("fr_id");
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try
            {
                //添加之前判断身份证是否被占用
                bool Isback = operateContext.bllSession.Bane_FamilyRecord.AddFamilyRecordUser(model);
                if (Isback)
                    return operateContext.RedirectAjax(0, "添加数据成功~", "", "");
                return operateContext.RedirectAjax(1, "添加数据失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 3.3 编辑家庭成员&社会关系 + ActionResult EditCriminalRecord(BaneCriminalModel model)
        /// <summary>
        ///  编辑家庭成员&社会关系
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditFamilyRecord(BaneFamilyRecordModel model)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            bool IsBack = true;
            try
            {
                IsBack = operateContext.bllSession.Bane_FamilyRecord.EditFamilyRecordUser(model);
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
            if (IsBack)
                return operateContext.RedirectAjax(0, "编辑用户成功~", "", "");
            return operateContext.RedirectAjax(1, "编辑用户失败~", "", "");
        }
        #endregion

        #region 3.4 删除家庭成员&社会关系 +ActionResult DelCriminalBaneById()
        /// <summary>
        ///  删除家庭成员&社会关系
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelFamilyBaneById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            int delCount = operateContext.bllSession.Bane_FamilyRecord.Delete(s => s.fr_id == id);
            if (delCount > 0)
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion

        //****************************5.尿检记录--档案袋********************************
        #region 3.0 尿检记录页面首次进入 + ActionResult UrinalysisList()
        /// <summary>
        ///  尿检记录页面首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult UrinalysisList()
        {
            return View();
        }
        #endregion

        #region 3.1 尿检记录一栏数据 +ActionResult GetUrinalysisBaneData()
        /// <summary>
        /// 尿检记录一栏数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUrinalysisBaneData()
        {
            string user_identify = RequestHelper.GetStrByName("user_identify");
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]);
            if (string.IsNullOrEmpty(user_identify))
                return operateContext.RedirectAjax(1, "必传参数异常！", "", "");
            List<Bane_UrinalysisRecord> bane = operateContext.bllSession.Bane_UrinalysisRecord.Select(s => s.user_identify.Equals(user_identify), s => s.ur_id, page, rows, true);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.Bane_UrinalysisRecord.SelectCount(s => s.user_identify.Equals(user_identify)),
                rows = bane
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 3.2 添加尿检记录 + ActionResult AddUrinalysisRecord(BaneCriminalModel model)
        /// <summary>
        ///  添加尿检记录
        /// </summary>
        /// <param name="user">违法犯罪对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUrinalysisRecord(BaneUrinalysisRecordModel model)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try
            {
                //添加之前判断身份证是否被占用
                bool Isback = operateContext.bllSession.Bane_UrinalysisRecord.AddUrinalysisRecordUser(model);
                if (Isback)
                    return operateContext.RedirectAjax(0, "添加数据成功~", "", "");
                return operateContext.RedirectAjax(1, "添加数据失败~", "", "");
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
        }
        #endregion

        #region 3.3 编辑尿检记录 + ActionResult EditUrinalysisRecord(BaneUrinalysisRecordModel model)
        /// <summary>
        ///  编辑尿检记录
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUrinalysisRecord(BaneUrinalysisRecordModel model)
        {
            ModelState.Remove("ur_code");
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            bool IsBack = true;
            try
            {
                IsBack = operateContext.bllSession.Bane_UrinalysisRecord.EditUrinalysisRecordUser(model);
            }
            catch (Exception ex)
            {
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
            if (IsBack)
                return operateContext.RedirectAjax(0, "编辑用户成功~", "", "");
            return operateContext.RedirectAjax(1, "编辑用户失败~", "", "");
        }
        #endregion

        #region 3.4 删除尿检记录 +ActionResult DelUrinalysisBaneById()
        /// <summary>
        ///  删除尿检记录
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelUrinalysisBaneById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            int delCount = operateContext.bllSession.Bane_UrinalysisRecord.Delete(s => s.ur_id == id);
            if (delCount > 0)
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        }
        #endregion
    }
}
