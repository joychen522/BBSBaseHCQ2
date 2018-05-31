using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2UI_Logic
{
    public class SoftVersionController : BaseLogic
    {

        /// <summary>
        /// 版本更新说明
        /// </summary>
        /// <returns></returns>
        [HCQ2_Common.Attributes.Load]
        public ActionResult Index()
        {
            return View();
        }
    }
}
