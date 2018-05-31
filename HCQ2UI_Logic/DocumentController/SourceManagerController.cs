using HCQ2_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2UI_Logic.DocumentController
{
    /// <summary>
    ///  资源管理通用控制器
    /// </summary>
    public class SourceManagerController: BaseLogic
    {
        //**************************1.0 VR资源*****************************
        #region 1.0 默认首次进入VR资源界面 + ActionResult DocList()
        /// <summary>
        ///  1.0 默认首次进入VR资源界面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        [HttpGet]
        public ActionResult VRList()
        {
            SetUserID();
            return View();
        }
        #endregion

        //**************************2.0 待审核资源*****************************
        #region 2.0 默认首次进入待审核资源界面 + ActionResult DocList()
        /// <summary>
        ///  2.0 默认首次进入待审核资源界面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        [HttpGet]
        public ActionResult ApproveList()
        {
            SetUserID();
            return View();
        }
        #endregion

        //**************************3.0 我的资源*****************************
        #region 3.0 默认首次进入我的资源界面 + ActionResult MyFileList()
        /// <summary>
        ///  3.0 默认首次进入我的资源界面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        [HttpGet]
        public ActionResult MyFileList()
        {
            SetUserID();
            return View();
        }
        #endregion

        //**************************4.0 我的分享*****************************
        #region 4.0 默认首次进入我的资源界面 + ActionResult MyFileList()
        /// <summary>
        ///  4.0 默认首次进入我的我的分享界面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        [HttpGet]
        public ActionResult MyShareList()
        {
            SetUserID();
            return View();
        }
        #endregion

        //**************************5.0 收到的分享*****************************
        #region 5.0 默认首次进入收到的分享界面 + ActionResult MyFileList()
        /// <summary>
        ///  5.0 默认首次进入收到的分享界面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        [HttpGet]
        public ActionResult GetShareList()
        {
            SetUserID();
            return View();
        }
        #endregion

        //**************************6.0 已下架资源*****************************
        #region 6.0 默认首次进入已下架资源界面 + ActionResult MyFileList()
        /// <summary>
        ///  6.0 默认首次进入已下架资源界面
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        [HttpGet]
        public ActionResult RecycleList()
        {
            SetUserID();
            return View();
        }
        #endregion

        //设置返回参数
        private void SetUserID ()
        {
            List<T_Permissions> list = HCQ2UI_Helper.Session.SysPermissSession.PermissList.FindAll(s => s.per_type.Equals("docSystem"));
            ViewBag.isdocManager = (null == list || list.Count <= 0) ? false : true;
            ViewBag.userID = HCQ2UI_Helper.OperateContext.Current.Usr.user_id;
        }
    }
}
