using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCQ2_Model.UploadModel;
using HCQ2_Common.Upload;
using System.IO;
using HCQ2_Model;

namespace HCQ2UI_Logic
{
    /// <summary>
    ///  主页面控制器
    /// </summary>
    public class MainController:BaseLogic
    {
        #region 1.0 框架页首次加载 + ActionResult Index()
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ownHead = (!string.IsNullOrEmpty(operateContext.Usr.user_img))
                ? operateContext.Usr.user_img.Replace("~", Request.ApplicationPath)
                : null;
            ViewBag.myUser = operateContext.Usr;
            return View("Index");
        }
        #endregion

        #region 2.0 修改头像首次进入 + ActionResult HeadImgList()
        /// <summary>
        ///  2.0 修改头像首次进入
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult HeadImgList()
        {
            return View();
        }
        #endregion

        #region 2.1 上传头像 + ActionResult UploadImg()
        /// <summary>
        ///  2.1 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImg()
        {
            Result result = new Result();
            result.avatarUrls = new List<string>();
            result.success = false;
            result.msg = "Failure!";
            // 取服务器时间+8位随机码作为部分文件名，确保文件名无重复。
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmssff") + uploadHelper.CreateRandomCode(8);

            #region 处理上传文件夹
            string path = Server.MapPath("~/UpFile/userImg");
            if (!Directory.Exists(path.ToString()))
                Directory.CreateDirectory(path.ToString());//文件夹不存在则创建
            #endregion

            #region 处理原始图片

            // 默认的 file 域名称是__source，可在插件配置参数中自定义。参数名：src_field_name
            HttpPostedFileBase file = Request.Files["__source"];//__avatar1
                                                                // 如果在插件中定义可以上传原始图片的话，可在此处理，否则可以忽略。
            if (file != null)
            {
                //原始图片的文件名，如果是本地或网络图片为原始文件名、如果是摄像头拍照则为 *FromWebcam.jpg
                string sourceFileName = file.FileName;
                //原始文件的扩展名
                string sourceExtendName = sourceFileName.Substring(sourceFileName.LastIndexOf('.') + 1);
                //当前头像基于原图的初始化参数（只有上传原图时才会发送该数据，且发送的方式为POST），用于修改头像时保证界面的视图跟保存头像时一致，提升用户体验度。
                //修改头像时设置默认加载的原图url为当前原图url+该参数即可，可直接附加到原图url中储存，不影响图片呈现。
                string initParams = Request.Form["__initParams"];
                result.sourceUrl = string.Format("~/UpFile/userImg/csharp_source_{0}.{1}", fileName, sourceExtendName);
                file.SaveAs(Server.MapPath(result.sourceUrl));
                result.sourceUrl += initParams;
                /*
				 * 可在此将 result.sourceUrl 储存到数据库，如果有需要的话。
				 * Save to database...
				 */

            }

            #endregion

            #region 处理头像图片

            //默认的 file 域名称：__avatar1,2,3...，可在插件配置参数中自定义，参数名：avatar_field_names
            string[] avatars = new string[3] { "__avatar1", "__avatar2", "__avatar3" };
            int avatar_number = 1;
            int avatars_length = avatars.Length;
            for (int i = 0; i < avatars_length; i++)
            {
                file = Request.Files[avatars[i]];
                if (file == null)
                    break;
                string virtualPath = string.Format("~/UpFile/userImg/csharp_avatar{0}_{1}.jpg", avatar_number, fileName);
                result.avatarUrls.Add(virtualPath);
                file.SaveAs(Server.MapPath(virtualPath));
                /*
				 *	可在此将 virtualPath 储存到数据库，如果有需要的话。
				 *	Save to database...
				 */
                //判断原来是否有上传过文件，有则删除
                if (!string.IsNullOrEmpty(operateContext.Usr.user_img))
                    System.IO.File.Delete(Server.MapPath(operateContext.Usr.user_img));
                operateContext.Usr.user_img = result.avatarUrls[0];
                operateContext.bllSession.T_User.Modify(operateContext.Usr, s => s.user_id == operateContext.Usr.user_id,
                    "user_img");
                avatar_number++;
            }

            #endregion

            //upload_url中传递的额外的参数，如果定义的method为get请将下面的context.Request.Form换为context.Request.QueryString

            result.success = true;
            result.msg = "Success!";
            //返回图片的保存结果（返回内容为json字符串，可自行构造，该处使用Newtonsoft.Json构造）
            return Json(result);
        }
        #endregion

        #region 2.2 获取当前登录用户对象
        /// <summary>
        ///  2.2 获取当前登录用户对象
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserInfo()
        {
            return operateContext.RedirectAjax(0, "", operateContext.Usr, "");
        }
        #endregion
    }
}
