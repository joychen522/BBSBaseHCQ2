using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HCQ2_Model.ViewModel;
using System.Web;
using System.IO;
using HCQ2_Model;
using HCQ2_Common;

namespace HCQ2UI_Logic
{
    public class NewsMessageController : BaseLogic
    {
        /// <summary>
        /// 新闻公告
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult Index()
        {
            //var data = operateContext.bllSession.T_ItemCode.GetByItemCode("NewsType");

            List<T_ItemCodeMenum> rList = new List<T_ItemCodeMenum>();
            T_ItemCodeMenum menu = new T_ItemCodeMenum();
            menu.code_name = "新闻";
            menu.code_value = "0001";
            rList.Add(menu);
            menu = new T_ItemCodeMenum();
            menu.code_name = "政策";
            menu.code_value = "0002";
            rList.Add(menu);
            menu = new T_ItemCodeMenum();
            menu.code_name = "焦点新闻";
            menu.code_value = "0003";
            rList.Add(menu);
            //menu = new T_ItemCodeMenum();
            //menu.code_name = "通知";
            //menu.code_value = "0004";
            //rList.Add(menu);
            ViewBag.NewsType = rList;
            return View();
        }

        /// <summary>
        /// 页面显示数据源
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMessage()
        {
            string keyword = RequestHelper.GetDeStrByName("txtSearchName");
            int page = Helper.ToInt(Request["page"]),
               rows = Helper.ToInt(Request["rows"]);
            keyword = (!string.IsNullOrEmpty(keyword)) ? HttpUtility.UrlDecode(keyword) : keyword;
            List<T_MessageNotice> list = operateContext.bllSession.T_MessageNotice.GetPageModle(keyword, page,rows);
            int count = (!string.IsNullOrEmpty(keyword)) ? operateContext.bllSession.T_MessageNotice.SelectCount(s => s.m_title.Contains(keyword)) : operateContext.bllSession.T_MessageNotice.SelectCount(s => (!string.IsNullOrEmpty(s.m_title)));
            TableModel tModel = new TableModel()
            {
                total = count,
                rows = list.Select(s => new { s.m_id, s.m_title, s.m_content, s.m_type, s.create_date, s.create_user_name }).ToList()
            };
            return Json(tModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存新闻公告
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult UpdateMess(FormCollection form)
        {
            string str = operateContext.bllSession.T_MessageNotice.UpdateMess(form) ? "ok" : "fin";
            return Content(str);
        }

        /// <summary>
        /// 删除新闻公告
        /// </summary>
        /// <param name="mess_id"></param>
        /// <returns></returns>
        public ActionResult DeleteMess(int mess_id)
        {
            T_MessageNotice notice = operateContext.bllSession.T_MessageNotice.Select(s => s.m_id == mess_id).FirstOrDefault();
            if(notice==null)
                return Content("fin");
            string img_url = (!string.IsNullOrEmpty(notice.m_image_src))? notice.m_image_src.Substring(0, notice.m_image_src.LastIndexOf("/")) :"";
            if (!string.IsNullOrEmpty(img_url))
                Directory.Delete(Server.MapPath(img_url), true);
            string str = operateContext.bllSession.T_MessageNotice.DeleteMess(mess_id) ? "ok" : "fin";
            return Content(str);
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsImage()
        {
            string returnStr = "";
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            if (hfc.Count > 0 && !string.IsNullOrEmpty(hfc[0].FileName))
            {
                Random r = new Random(10000000);
                string result = r.Next(10000000, 99999999).ToString();
                string timeStr = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString();
                string image = "~/Files/NewsImage/" + timeStr + "/" + result;
                if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(image)) == false)//如果不存在就创建file文件夹
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(image));
                hfc[0].SaveAs(System.Web.HttpContext.Current.Server.MapPath(image + "/" + hfc[0].FileName));
                returnStr = image + "/" + hfc[0].FileName;
            }
            return Content(returnStr);
        }

        /// <summary>
        /// 新闻公告详细信息
        /// </summary>
        /// <param name="mess_id"></param>
        /// <returns></returns>
        public ActionResult NewsDetail(int mess_id)
        {
            return View();
        }

        /// <summary>
        /// 获取详细新闻公告
        /// </summary>
        /// <param name="mess_id"></param>
        /// <returns></returns>
        public ActionResult GetNewsJson(int mess_id)
        {
            return Json(operateContext.bllSession.T_MessageNotice.GetByMessId(mess_id), JsonRequestBehavior.AllowGet);
        }
    }
}
