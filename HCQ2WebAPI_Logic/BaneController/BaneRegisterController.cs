using HCQ2_Common.Constant;
using HCQ2_Model.BaneUser.APP.Params;
using HCQ2UI_Helper;
using HCQ2WebAPI_Logic.BaseAPIController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using static HCQ2_Model.BaneUser.BaneLogParam;
using HCQ2_Model;

namespace HCQ2WebAPI_Logic.BaneController
{
    /// <summary>
    ///  禁毒人员注册控制器
    /// </summary>
    [HCQ2_Common.Attributes.SkipApi]
    public class BaneRegisterController:BaseWeiXinApiLogic
    {
        #region ✔1.0 禁毒人员注册 + object BaneReg(BaneRegModel bane)
        /// <summary>
        ///  禁毒人员注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object BaneReg(BaneRegModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            BaneRegisterType type = operateContext.bllSession.Bane_User.BaneRegister(bane);
            if(type == BaneRegisterType.EXCEPTION)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "此用户不属于系统禁毒人员，请核对信息~", null);
            else if(type== BaneRegisterType.OK)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "注册成功~", null);
            else if(type == BaneRegisterType.FINASH)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "此用户已经注册过~", null);
            return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "注册失败~", null);
        }
        #endregion

        #region ✔1.1 获取人员信息 + object BaneReg(BaneRegModel bane)
        /// <summary>
        ///  获取人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object BaneDataByID(BaseBaneModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "参数验证失败", null);
            //人员注册对象
            Bane_User user = operateContext.bllSession.Bane_User.Select(s => s.user_guid == bane.userid).FirstOrDefault();
            if(user==null)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "查无此人，请核对信息是否有误~", "");
            BaneRegModel userModel = new BaneRegModel
            {
                user_identify = user.user_identify,
                user_phone = user.user_mobile,
                user_pwd = ""
            };
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "成功获取数据", userModel);
        }
        #endregion

        #region ✔1.2 修改密码 + object BaneReg(BaneRegModel bane)
        /// <summary>
        ///  修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifyBaneData(ModifyBaneDataModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(
                    WebResultCode.Exception, "参数验证失败", null);
            //数据库旧密码
            string userPwd = operateContext.bllSession.Bane_User.Select(s => s.user_guid == bane.userid).FirstOrDefault()?.user_pwd;
            if(userPwd!=HCQ2_Common.Encrypt.EncryptHelper.Md5Encryption(bane.old_pwd))
                return OperateContext.Current.RedirectWebApi(WebResultCode.Error, "原始密码错误~","");
            operateContext.bllSession.Bane_User.Modify(new Bane_User { user_pwd = HCQ2_Common.Encrypt.EncryptHelper.Md5Encryption(bane.new_pwd) }, s => s.user_guid == bane.userid, "user_pwd");
            return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "密码修改成功", "");
        }
        #endregion

        #region ✔1.3 禁毒人员登录 + object BaneLogin(BaneLoginModel bane)
        /// <summary>
        ///  禁毒人员登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public object BaneLogin(BaneLoginModel bane)
        {
            if (!ModelState.IsValid)
                return OperateContext.Current.RedirectWebApi(WebResultCode.Exception, "参数验证失败", null);
            string pwd = HCQ2_Common.Encrypt.EncryptHelper.Md5Encryption(bane.user_pwd);
            Bane_User user = operateContext.bllSession.Bane_User.Select(s => s.user_identify == bane.user_identify && s.user_pwd == pwd).FirstOrDefault();
            string guid = user?.user_guid;
            if (!string.IsNullOrEmpty(guid))
                return OperateContext.Current.RedirectWebApi(WebResultCode.Ok, "登录成功", guid);
            return OperateContext.Current.RedirectWebApi(WebResultCode.Error, "身份证或密码错误，请核对信息", null);
        }
        #endregion
    }
}
