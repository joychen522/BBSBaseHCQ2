using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2UI_Logic
{
    public class ItemCodeController : BaseLogic
    {
        private HCQ2_IBLL.IBLLSession bll;

        [HCQ2_Common.Attributes.Load]
        [HCQ2_Common.Attributes.Element]
        public ActionResult Index()
        {
            bll = operateContext.bllSession;
            ViewBag.ItemCodeTree = bll.T_ItemCode.GetItemTreeJson(bll.T_ItemCode.GetItemCode());
            return View();
        }

        public ActionResult GetItemCodeTree()
        {
            bll = operateContext.bllSession;
            return Content(bll.T_ItemCode.GetItemTreeJson(bll.T_ItemCode.GetItemCode()));
        }

        public ActionResult GetItemCodeView(FormCollection form)
        {
            bll = operateContext.bllSession;
            return Content(bll.T_ItemCode.ReturnPageJson(form));
        }

        public ActionResult AddItemCode(FormCollection form)
        {
            string result = "find";
            bll = operateContext.bllSession;
            if (bll.T_ItemCode.AddItemCode(form))
                result = "ok";
            return Content(result);
        }

        public ActionResult Delete(FormCollection form)
        {
            string result = "find";
            bll = operateContext.bllSession;
            if (bll.T_ItemCode.DeleteItemCode(form))
                result = "ok";
            return Content(result);
        }

        public ActionResult Edit(FormCollection form)
        {
            string result = "find";
            bll = operateContext.bllSession;
            if (bll.T_ItemCode.EditItemCode(form))
                result = "ok";
            return Content(result);
        }

        public ActionResult VlidataItemCode(string item_code)
        {
            string result = "find";
            bll = operateContext.bllSession;
            if (bll.T_ItemCode.ValidataItemCode(item_code))
                result = "ok";
            return Content(result);
        }
    }
}
