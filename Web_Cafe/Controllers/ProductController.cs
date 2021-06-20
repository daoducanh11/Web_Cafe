using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Models.DAO;

namespace Web_Cafe.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            Session["url"] = Request.Url;
            var proExtend = new ProductDAOExtent();
            var item = proExtend.GetProductExtendById(id);
            ViewBag.ListProducExtendCategory = proExtend.GetListProductByIDCategory(item.Product.CategoryID.HasValue ? item.Product.CategoryID.Value : 1, id);
            return View(item);
        }

    }
}