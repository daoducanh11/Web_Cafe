using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Cafe.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("", "Admin/AdminLogin/Index");
            //return RedirectToAction("Index", "AdminLogin", "Admin");
        }
    }
}