using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HCQ2UI_Logic
{
    [HCQ2_Common.Attributes.Skip]
    public class ErrorController:BaseLogic
    {
        public ActionResult error()
        {
            return View("Error");
        }
    }
}
