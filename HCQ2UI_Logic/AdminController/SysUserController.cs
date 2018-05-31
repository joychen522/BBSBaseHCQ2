using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using HCQ2_Common;
using HCQ2_Common.Bean;
using HCQ2_Common.Constant;
using HCQ2_Model;
using HCQ2_Model.ViewModel;
using HCQ2_Model.ViewModel.SysAdmin;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  系统设置>用户管理
    /// </summary>
    public class SysUserController:BaseLogic
    {
        //*********************************用户管理*****************************************
        #region 1.0 用户管理首次进入页面 +ActionResult List()
        /// <summary>
        ///  用户管理首次进入页面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Element]
        [HCQ2_Common.Attributes.Load]
        [HttpGet]
        public ActionResult List()
        {
            //获取当前用户 所属区域及子区域个数统计 系统用户没有区域
            int areaCount = operateContext.bllSession.T_OrgFolder.GetAreaCountByID(operateContext.Usr.user_id);
            if (areaCount == 0 || areaCount > 1)
            {
                //系统用户
                ViewBag.UserType = "systemUser";
                ViewBag.areaCode = "0";//区域代码
            }  
            else
            {
                //社工用户
                ViewBag.UserType = "socialUser";
                var query = operateContext.bllSession.T_Org_User.Select(s => s.user_id == operateContext.Usr.user_id);
                ViewBag.areaCode = query[0].UnitID;//区域代码
            }   
            return View();
        }
        #endregion

        #region 1.1 获取用户一栏数据 +ActionResult GetUserData()
        /// <summary>
        ///  获取用户一栏数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserData()
        {
            string keyword = Helper.ToString(Request["userName"]);//用户名
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]),
               folder_id = RequestHelper.GetIntByName("folder_id");//区域代码
            keyword = (!string.IsNullOrEmpty(keyword)) ? HttpUtility.UrlDecode(keyword) : keyword;
            List<UserModel> user= base.operateContext.bllSession.T_User.GetUserData(folder_id, keyword, page, rows);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_User.GetCountByData(keyword, folder_id),
                rows = user
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 1.2 添加用户 + ActionResult AddUser(UserModel user)
        /// <summary>
        ///  添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUser(UserModel user)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            try
            {
                string roles = Helper.ToString(Request["user_role"]);
                user.user_role = roles;
                //添加之前判断登录名是否被占用
                List<T_User> usr =
                    operateContext.bllSession.T_User.Select(
                        s => s.login_name.Equals(user.login_name, StringComparison.CurrentCultureIgnoreCase));
                if (null != usr && usr.Count > 0)
                    return operateContext.RedirectAjax(1, "添加用户失败，当前登录用户已存在，请重新设置登录名！", "", "");
                bool Isback = base.operateContext.bllSession.T_User.AddUser(user);
                if (Isback)
                    return operateContext.RedirectAjax(0, "添加用户成功~", "", "");
                return operateContext.RedirectAjax(1, "添加用户失败~", "", "");
            }
            catch (Exception ex){
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }    
        }
        #endregion

        #region 1.3 编辑用户数据 + ActionResult EditUser(UserModel user)
        /// <summary>
        ///  编辑用户数据
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(UserModel user)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            int user_id = HCQ2_Common.Helper.ToInt(Request["user_id"]);
            string roles = Helper.ToString(Request["user_role"]);
            user.user_role = roles;
            if (user_id<=0)
                return operateContext.RedirectAjax(1, "编辑用户主键值为空~", "", "");
            bool IsBack = true;
            //编辑之前判断登录名是否被占用
            List<T_User> usr =
                operateContext.bllSession.T_User.Select(
                    s =>
                        s.login_name.Equals(user.login_name, StringComparison.CurrentCultureIgnoreCase) &&
                        s.user_id != user.user_id);
            if (null != usr && usr.Count>0 && usr[0].user_id!=user.user_id)
                return operateContext.RedirectAjax(1, "编辑用户失败，登录名已被占用请重新设置登录名！", "", "");
            string selUnit = Helper.ToString(Request["selUnit"]);
            try { 
                IsBack = base.operateContext.bllSession.T_User.EditUser(user, user_id, selUnit);
            }
            catch(Exception ex) { 
                return operateContext.RedirectAjax(1, ex.Message, "", "");
            }
            if (IsBack)
                return operateContext.RedirectAjax(0, "编辑用户成功~", "", "");
            return operateContext.RedirectAjax(1, "编辑用户失败~", "", "");
        }
        #endregion

        #region 1.4 删除用户数据 +ActionResult DelUser(int id)
        /// <summary>
        ///  删除用户数据
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelUserById(int id)
        {
            if(id<=0)
                return operateContext.RedirectAjax(1, "需要删除的数据不存在~", "", "");
            if(operateContext.Usr.user_id==id)
                return operateContext.RedirectAjax(1, "不能删除当前登录者自己的账号~", "", "");
            List<T_User> user = operateContext.bllSession.T_User.Select(s => s.user_id == id);

            //HCQ2UI_Helper.UnitHelper.unitWork.Delete<T_User>(s => s.user_id == id);
            //HCQ2UI_Helper.UnitHelper.unitWork.Delete<T_UserRoleRelation>(s => s.user_id == id);
            //HCQ2UI_Helper.UnitHelper.unitWork.Delete<T_UserGroupRelation>(s => s.user_id == id);
            //HCQ2UI_Helper.UnitHelper.unitWork.Delete<T_Org_User>(s => s.user_id == id);
            //HCQ2UI_Helper.UnitHelper.unitWork.Save();

            int delCount = operateContext.bllSession.T_User.Delete(s => s.user_id == id);
            //1.删除用户对应的角色
            operateContext.bllSession.T_UserRoleRelation.Delete(s => s.user_id == id);
            //2.删除用户对应的组表
            operateContext.bllSession.T_UserGroupRelation.Delete(s => s.user_id == id);

            //3.判断原来是否有上传过头像文件，有则删除
            if (!string.IsNullOrEmpty(user[0].user_img))
                System.IO.File.Delete(Server.MapPath(user[0].user_img));
            //4.删除对应的组织结构关联关系
            operateContext.bllSession.T_Org_User.Delete(s => s.user_id == id);
            //return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            if (delCount > 0)
                return operateContext.RedirectAjax(0, "数据删除成功~", "", "");
            return operateContext.RedirectAjax(1, "数据删除失败~", "", "");
        } 
        #endregion

        #region 1.5 根据主键ID获取用户对象 + ActionResult GetEditDataById(string id)
        /// <summary>
        ///  根据主键ID获取用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetEditDataById(int id)
        {
            List<T_User> user = operateContext.bllSession.T_User.Select(s => s.user_id == id);
            return operateContext.RedirectAjax(0, "数据获取成功~", user, "");
        }
        #endregion

        #region 1.6 验证用户是否存在 +public ActionResult CheckUser()
        /// <summary>
        ///  验证用户是否存在
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUser()
        {
            string login_name = Helper.ToString(Request["login_name"]);
            if (string.IsNullOrEmpty(login_name))
                return operateContext.RedirectAjax(1, "登录名为空~", "", "");
            int count =
                operateContext.bllSession.T_User.SelectCount(s => s.login_name == HttpUtility.UrlDecode(login_name));
            if (count > 0)
                return operateContext.RedirectAjax(1, "登录名已存在请重新输入~", "", "");
            return operateContext.RedirectAjax(0, "登录名可以使用~", "", "");
        }
        #endregion

        #region 1.7 激活用户 +ActionResult ActUserById(int id)
        /// <summary>
        ///  激活用户
        /// </summary>
        /// <param name="id">受限表id</param>
        /// <returns></returns>
        public ActionResult ActUserById(int id)
        {
            if (id <= 0)
                return operateContext.RedirectAjax(1, "受限id为空~", "", "");
            int count = operateContext.bllSession.T_LimitUser.Delete(s => s.id == id);
            if(count<=0)
                return operateContext.RedirectAjax(1, "激活失败~", "", "");
            //写入操作日志表
            operateContext.bllSession.T_LogSeting.Add(new T_LogSeting() {folder_id=1,business_name="用户管理",table_name= "T_LimitUser",primary_key="",user_id= operateContext.Usr.user_id,log_note="激活用户" });
            return operateContext.RedirectAjax(0, "激活成功~", "", "");
        }
        #endregion

        #region 1.8 用户管理 获取单位树数据 + ActionResult GetUserUnitTreeData()
        /// <summary>
        ///  用户管理 获取单位树数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserUnitTreeData()
        {
            return operateContext.RedirectAjax(0, "成功", operateContext.bllSession.T_User.GetAreaData(operateContext.Usr.user_id), "");
        }
        #endregion

        #region 2.0 根据用户ID获取登录信息 + ActionResult GetLoginMessageData(int id)
        /// <summary>
        ///  根据用户ID获取登录信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLoginMessageData()
        {
            T_Login loginObj = operateContext.bllSession.T_Login.selectLoginById(operateContext.Usr.user_id);
            if(loginObj==null)
                return operateContext.RedirectAjax(1, "没有登录信息", "", "");
            return operateContext.RedirectAjax(0, "成功获取登录信息", loginObj, "");
        }
        #endregion

        #region  2.1 重置密码 + ActionResult ResetPassWord(UserModel user)
        /// <summary>
        ///  2.1 重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassWord(UserModel user)
        {
            if (!ModelState.IsValid)
                return operateContext.RedirectAjax(1, "数据验证失败~", "", "");
            int mark = operateContext.bllSession.T_User.Modify(new T_User { user_pwd = HCQ2_Common.Encrypt.EncryptHelper.Md5Encryption(user.user_pwd) }, s => s.user_id == user.user_id, "user_pwd");
            if (mark>0)
                return operateContext.RedirectAjax(0, "重置密码成功~", "", "");
            return operateContext.RedirectAjax(1, "重置密码失败~", "", "");
        }
        #endregion

        //*********************************用户--角色设置*****************************************
        #region 3.0 用户--角色设置页面首次跳转 +ActionResult UserRoleList()
        /// <summary>
        ///  用户--角色设置页面首次跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult UserRoleList()
        {
            return View();
        }
        #endregion

        #region 3.1 根据用户ID 获取对于角色数据 +ActionResult GetUserRoleData(int id)
        /// <summary>
        ///  根据用户ID 获取对于角色数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserRoleData(int id)
        {
            //ID对应角色
            List<T_UserRoleRelation> list = operateContext.bllSession.T_UserRoleRelation.GetUserRoleData(id);
            if (list == null || list.Count == 0)
                return operateContext.RedirectAjax(1, "获取角色信息为空~", "", "");
            return operateContext.RedirectAjax(0, "成功获取角色信息", list, "");
        }
        #endregion

        #region 3.2 获取所有角色 +ActionResult GetUserRoleData()
        /// <summary>
        ///  获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllRoleData()
        {
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            //所有角色
            List<T_Role> list = operateContext.bllSession.T_Role.Select<int>(s => (!string.IsNullOrEmpty(s.role_name)),
                s => s.role_id, page, rows, true);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_Role.SelectCount(null),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 3.3  保存用户--角色设置 + ActionResult SaveUserRoleData(FormCollection form, int id)
        /// <summary>
        ///  保存用户--角色设置
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUserRoleData(FormCollection form, int id)
        {
            string roles = Helper.ToString(form["roleData"]);
            try
            {
                bool back = operateContext.bllSession.T_UserRoleRelation.SaveUserRoleData(roles,id);
                //清理角色缓存
                SessionHelper.RemoveSession(CacheConstant.loginUserCacheRoles);
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

        //*********************************用户--分组设置*****************************************

        #region 4.0 用户--用户组设置页面首次跳转 +ActionResult UserGroupList()
        /// <summary>
        ///  用户--用户组设置页面首次跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult UserGroupList()
        {
            return View();
        }
        #endregion

        #region 4.1 根据用户ID 获取对于组数据 +ActionResult GetUserGroupData(int id)
        /// <summary>
        ///  根据用户ID 获取对于组数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserGroupData(int id)
        {
            //ID对应用户组
            List<T_UserGroupRelation> list = operateContext.bllSession.T_UserGroupRelation.GetUserGroupData(id);
            if (list == null || list.Count == 0)
                return operateContext.RedirectAjax(1, "获取角色信息为空~", "", "");
            return operateContext.RedirectAjax(0, "成功获取角色信息", list, "");
        }
        #endregion

        #region 4.2 获取所有组数据集合 +ActionResult GetAllGroupData()
        /// <summary>
        ///  获取所有组数据集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllGroupData()
        {
            int page = Helper.ToInt(Request["page"]);
            int rows = Helper.ToInt(Request["rows"]);
            //所有角色
            List<T_UserGroup> list = operateContext.bllSession.T_UserGroup.Select<int>(s => (!string.IsNullOrEmpty(s.group_name)),
                s => s.group_id, page, rows, true);
            TableModel tModel = new TableModel()
            {
                total = operateContext.bllSession.T_UserGroup.SelectCount(null),
                rows = list
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 4.3  保存用户--用户组设置 + ActionResult SaveUserGroupData(FormCollection form, int id)
        /// <summary>
        ///  保存用户--用户组设置
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUserGroupData(FormCollection form, int id)
        {
            string users = Helper.ToString(form["userData"]);
            try
            {
                bool back = operateContext.bllSession.T_UserGroupRelation.SaveUserGroupData(users, id);
                //清理角色缓存
                SessionHelper.RemoveSession(CacheConstant.loginUserCacheRoles);
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

        //*********************************用户--代管配置*****************************************
        #region 5. 0用户--代管配置首次进入+ActionResult UserProxy()
        /// <summary>
        ///  用户--代管配置首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult UserProxy()
        {
            return View();
        }
        #endregion

        #region 5.1 异步获取单位树zTree方式 +ActionResult GetUnitTreeData()
        /// <summary>
        ///  异步获取单位树zTree方式
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUnitTreeData(int id)
        {
            List<Dictionary<string, object>> list = operateContext.bllSession.T_UserUnitRelation.GetUserAreaAndPersonById(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 5.2 用户管理代管配置 +ActionResult GetProUnitTreeData()
        /// <summary>
        ///  用户管理代管配置
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProUnitTreeData(int id)
        {
            List<Dictionary<string, object>> list = operateContext.bllSession.T_UserUnitRelation.GetUserAreaAndPersonById(id,false);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region 5.2 保存用户--代管设置 +ActionResult SaveUserUnitData(FormCollection form, int id)
        /// <summary>
        ///  保存用户--代管设置
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUserUnitData(FormCollection form, int id)
        {
            string unitData = form["unitData"];
            bool isOk = operateContext.bllSession.T_UserUnitRelation.SaveUserUnitData(unitData, id);
            //清理角色缓存
            SessionHelper.RemoveSession(CacheConstant.loginUserPerminssUnitData);
            if (isOk)
                return operateContext.RedirectAjax(0, "保存数据成功~", "", "");
            return operateContext.RedirectAjax(1, "保存数据失败~", "", "");
        }
        #endregion
    }
}
