using System;
using System.Web.Mvc;

namespace FXTF.Web.Admin.Controllers.MVCController
{
    public class FXTFController : Controller
    {
        public ActionResult FXTF()
        {
            var SesUserID = Convert.ToInt64(Session["SesUserID"]);
            if (SesUserID > 0)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }
    }
}
