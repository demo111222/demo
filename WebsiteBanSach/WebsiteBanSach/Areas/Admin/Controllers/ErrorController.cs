using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Admin/Error
        public ActionResult Er404()
        {
            return View();
        }
        public ActionResult Er403()
        {
            return View();
        }
        public ActionResult ErOrderFalse()
        {
            return View();
        }
    }
}