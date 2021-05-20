using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_Cafe.Areas.Admin.Controllers
{
    public class AdminOrderController : Controller
    {
        // GET: Admin/AdminOrder
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}