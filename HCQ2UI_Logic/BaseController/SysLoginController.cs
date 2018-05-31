using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HCQ2_Model.JsonData;
using HCQ2_Common;
using HCQ2_Common.Bean;
using HCQ2_Common.Validate;
using HCQ2_Model.ViewModel;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  登录控制器
    /// </summary>
    public class SysLoginController : BaseLogic
    {
        #region 1.0 进入登录界面 + ActionResult Login()
        /// <summary>
        ///  1.0 进入登录界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HCQ2_Common.Attributes.Skip]
        public ActionResult Login()
        {
            operateContext.ClearLogin();
            Session.Abandon();
            Session.RemoveAll();
            ViewBag.requestIP = Request.Url;
            return View("Login");
        }
        #endregion

        #region 1.1 登录页面登录处理 + ActionResult Login(HCQ2_Model.ViewModel.LoginUser user)
        /// <summary>
        ///  1.1 登录页面登录处理 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [HCQ2_Common.Attributes.Skip]
        //[ValidateAntiForgeryToken]//用于阻止伪造请求
        public ActionResult Login(LoginUser user)
        {
            string code = Helper.ToString(Session["validateCode"]);
           SessionHelper.RemoveSession("validateCode");//移除session
            if (user.ReCode != code)
                return operateContext.RedirectAjax(1, "验证码错误~", null, "");
            LoginResultModel rModel = operateContext.Login(user);
            if (rModel.Status)
                return operateContext.RedirectAjax(0, "登录成功~", null, "Main/Index");
            return operateContext.RedirectAjax(1, (!string.IsNullOrEmpty(rModel.Message)) ? rModel.Message : rModel.Msg.ToString(), null, "");
        }
        #endregion

        #region 1.2 创建生成验证码 + ActionResult CreateValidateCode()
        /// <summary>
        ///  1.2 创建生成验证码
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Skip]
        public ActionResult CreateValidateCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        #endregion

        [HCQ2_Common.Attributes.Skip]
        [HCQ2_Common.Attributes.Load]
        public ActionResult Text()
        {
            return View();
        }
    }
}
