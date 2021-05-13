using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;

using Web_Cafe.Models.DAO;


namespace Web_Cafe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.ListProductExtend = new ProductDAOExtent().ListProductExtend;
            ViewBag.ListProductSales = new ProductDAOExtent().ListProductSales;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}