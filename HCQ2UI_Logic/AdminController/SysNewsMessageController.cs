using HCQ2_Common;
using HCQ2_Common.Upload;
using HCQ2_Model;
using HCQ2_Model.UploadModel;
using HCQ2_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCQ2UI_Logic.AdminController
{
    /// <summary>
    ///  新闻政策控制器
    /// </summary>
    public class SysNewsMessageController: BaseLogic
    {
        #region 1.0 元素管理首次进去页面跳转 +ActionResult ElementList()
        /// <summary>
        ///  元素管理首次进去页面跳转
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Element]
        [HCQ2_Common.Attributes.Load]
        public ActionResult NewsList()
        {
            return View();
        }
        #endregion

        #region 1.1 获取一栏数据 +ActionResult GetNewsData()
        /// <summary>
        ///  获取一栏数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNewsData()
        {
            string keyword = RequestHelper.GetDeStrByName("newsName");
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]);
            keyword = (!string.IsNullOrEmpty(keyword)) ? HttpUtility.UrlDecode(keyword) : keyword;
            TableModel tModel = operateContext.bllSession.T_MessageNotice.GetNewsListByParams(keyword,page,rows);
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1.2 添加新闻 + ActionResult AddNews()
        /// <summary>
        ///  添加新闻
        /// </summary>
        /// <param name="model">新闻对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNews(T_MessageNotice model)
        {
            bool mark = operateContext.bllSession.T_MessageNotice.AddNews(model);
            if (!mark)
                return operateContext.RedirectAjax(1, "添加新闻失败~", "", "");
            return operateContext.RedirectAjax(0, "添加试题成功~", "", "");
        }
        #endregion

        #region 1.3 编辑新闻对象 + ActionResult EditNews()
        /// <summary>
        ///  编辑新闻对象
        /// </summary>
        /// <param name="model">新闻对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditNews(T_MessageNotice model)
        {
            bool mark = operateContext.bllSession.T_MessageNotice.EditNews(model);
            if (!mark)
                return operateContext.RedirectAjax(1, "编辑新闻失败~", "", "");
            return operateContext.RedirectAjax(0, "编辑新闻成功~", "", "");
        }
        #endregion

        #region 1.4 删除新闻对象 + ActionResult DeleteNews(int id)
        /// <summary>
        ///  删除新闻对象
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteNews(int id)
        {
            T_MessageNotice notice = operateContext.bllSession.T_MessageNotice.Select(s => s.m_id == id).FirstOrDefault();
            if (notice == null)
                return operateContext.RedirectAjax(1, "需要删除的新闻对象不存在~", "", "");
            string[] str = notice.focus_imgage.Split('/');
            if (str.Length > 4)
            {
                string url = "~";
                for (int i = 4; i < str.Length - 1; i++)
                    url += "/" + str[i];
                if (!string.IsNullOrEmpty(url) && Directory.Exists(Server.MapPath(url)))
                    Directory.Delete(Server.MapPath(url), true);
            }
            int mark = operateContext.bllSession.T_MessageNotice.Delete(s=>s.m_id==id);
            if (mark<=0)
                return operateContext.RedirectAjax(1, "删除新闻失败~", "", "");
            return operateContext.RedirectAjax(0, "删除新闻成功~", "", "");
        }
        #endregion

        #region 2.0 上传图片 + ActionResult UploadNewsImg()
        /// <summary>
        ///  上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadNewsImg()
        {
            int id = RequestHelper.GetIntByName("id");
            Result result = new Result();
            result.avatarUrls = new List<string>();
            result.success = false;
            result.msg = "图片上传成功！";
            // 取服务器时间+8位随机码作为部分文件名，确保文件名无重复。
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmssff") + uploadHelper.CreateRandomCode(8);

            #region 处理上传文件夹

            #region 编辑删除文件
            if(id>0)
            {
                T_MessageNotice notice = operateContext.bllSession.T_MessageNotice.Select(s => s.m_id == id).FirstOrDefault();
                string focus_imgage_url = notice?.focus_imgage;
                if(!string.IsNullOrEmpty(focus_imgage_url))
                {
                    string[] str = focus_imgage_url.Split('/');
                    string url = "~";
                    for (int i = 4; i < str.Length - 1; i++)
                        url += "/" + str[i];
                    if (!string.IsNullOrEmpty(url) && Directory.Exists(Server.MapPath(url)))
                        Directory.Delete(Server.MapPath(url), true);
                }
            }
            #endregion

            string path = Server.MapPath("~/UpFile/SysNewsImg/" + operateContext.Usr.user_id + "/" + fileName);
            if (!Directory.Exists(path.ToString()))
                Directory.CreateDirectory(path.ToString());//文件夹不存在则创建
            #endregion

            #region 处理头像图片
            result.sourceUrl = string.Format("http://" + request.Url.Host + ":" + request.Url.Port + request.ApplicationPath + "/UpFile/SysNewsImg/"+ operateContext.Usr.user_id + "/{0}", fileName);
            HttpPostedFileBase file = Request.Files["__source"];//__avatar1
            //默认的 file 域名称：__avatar1,2,3...，可在插件配置参数中自定义，参数名：avatar_field_names
            string[] avatars = new string[3] { "__avatar1", "__avatar2", "__avatar3" };
            int avatars_length = avatars.Length;
            string name = string.Empty;
            for (int i = 0; i < avatars_length; i++)
            {
                switch (i)
                {
                    case 0: name = "focus_imgage"; break;
                    case 1: name = "messDetail_imgage"; break;
                    case 2: name = "messList_imgage"; break;
                }
                file = Request.Files[avatars[i]];
                if (file == null)
                    break;
                string virtualPath = string.Format("~/UpFile/SysNewsImg/"+ operateContext.Usr.user_id + "/{0}/csharp_{1}.jpg", fileName, name);
                result.avatarUrls.Add("http://" + request.Url.Host + ":" + request.Url.Port + request.ApplicationPath + virtualPath.Replace("~", ""));
                file.SaveAs(Server.MapPath(virtualPath));
            }
            #endregion
            result.success = true;
            result.msg = "Success!";
            //返回图片的保存结果（返回内容为json字符串，可自行构造，该处使用Newtonsoft.Json构造）
            return Json(result);
        }
        #endregion
    }
}
