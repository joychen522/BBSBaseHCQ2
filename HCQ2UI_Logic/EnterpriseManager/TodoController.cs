using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HCQ2_Model;
using HCQ2_Model.ViewModel;
using System.Web;

namespace HCQ2UI_Logic
{
    public class TodoController : BaseLogic
    {

        /// <summary>
        /// 待办事宜
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult Index()
        {
            List<Dictionary<string, object>> rList = new List<Dictionary<string, object>>();
            Dictionary<string, object> rDic = new Dictionary<string, object>();

            rDic.Add("text", "已接收");
            rDic.Add("code_name", "已接收");
            rDic.Add("code_value", "0001");
            rDic.Add("code_id", 1);
            rDic.Add("code_pid", 0);
            rList.Add(rDic);

            rDic = new Dictionary<string, object>();
            rDic.Add("text", "已发送");
            rDic.Add("code_name", "已发送");
            rDic.Add("code_value", "0002");
            rDic.Add("code_id", 2);
            rDic.Add("code_pid", 0);
            rList.Add(rDic);

            //var data = operateContext.bllSession.T_ItemCode.GetByItemCode("TodoList");
            //var menu = operateContext.bllSession.T_ItemCodeMenum.GetByItemId(data.item_id);
            //var tree = operateContext.bllSession.T_ItemCodeMenum.GetMenuTree(menu);
            ViewBag.TreeItemCode = HCQ2_Common.JsonHelper.objectToJsonStr(rList);

            var userTree = operateContext.bllSession.T_TodoList.GetUserTree();
            ViewBag.TreeUser = HCQ2_Common.JsonHelper.objectToJsonStr(userTree);

            return View();
        }

        /// <summary>
        /// 获取页面显示的数据源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTodoSoure(FormCollection form)
        {
            TableModel modle = operateContext.bllSession.T_TodoList.GetUserAllTodoList(form);
            return Json(modle, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult AddTodo(FormCollection form)
        {
            string str = operateContext.bllSession.T_TodoList.NewTodo(form) ? "ok" : "find";
            return Content(str);
        }

        /// <summary>
        /// 把待办事宜标记为删除
        /// </summary>
        /// <param name="to_id"></param>
        /// <returns></returns>
        public ActionResult DeleteTodo(int to_id)
        {
            string str = operateContext.bllSession.T_TodoList.DeleteTodo(to_id) ? "ok" : "find";
            return Content(str);
        }

        /// <summary>
        /// 查看待办事宜内容
        /// </summary>
        /// <param name="to_id"></param>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult TodoDetail(int to_id)
        {
            ViewBag.Content = operateContext.bllSession.T_TodoList.GetByToId(to_id);
            return View();
        }

        /// <summary>
        /// 获取具体对象
        /// </summary>
        /// <param name="to_id"></param>
        /// <returns></returns>
        public ActionResult GetTodoJson(int to_id)
        {
            var data = operateContext.bllSession.T_TodoList.GetByToId(to_id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            string fileName = System.IO.Path.GetFileName(file.FileName);
            string filePath = Server.MapPath("~/Upload/");
            if (!System.IO.Directory.Exists(filePath))
                System.IO.Directory.CreateDirectory(filePath);
            string filePhysicalPath = filePath + fileName;
            string pic = "", error = "";
            try
            {
                file.SaveAs(filePhysicalPath);
                pic = "/Upload/" + fileName;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Content(pic);
        }

        /// <summary>
        /// 回复待办事宜
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult ReContent(FormCollection form)
        {
            string str = operateContext.bllSession.T_TodoList.ReTodoContent(form) ? "ok" : "fin";
            return Content(str);
        }
    }
}
