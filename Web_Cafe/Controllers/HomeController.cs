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
        public ActionResult Index(int pageNum = 1, int pageSize = 8)
        {
            ProductDAOExtent dao = new ProductDAOExtent();

            ViewBag.ListProductHotExtend = dao.ListProductHotExtend();
            ViewBag.ListProductSales = dao.GetListProductSales();

            return View(dao.GetListProductExtend(pageNum, pageSize));
        }

        public ActionResult SearchProduct(string keywordProduct = "", string id = "", int pageNum = 1, int pageSize = 6)
        {
            ViewBag.keywordProduct = keywordProduct;
            ViewBag.id = id;
            if (id != "")
            {
                ProductDAOExtent dao = new ProductDAOExtent();
                CategoryDAO cdao = new CategoryDAO();
                ViewBag.ListCategory = cdao.ListCate();
                ViewBag.CName = cdao.ListCate().ToList().Where(i => i.CategoryID.ToString().Contains(id)).FirstOrDefault().CateName;
                return View(dao.GetProductExtendByCateID(id, pageNum, pageSize));
            }
            else
            {
                ProductDAOExtent dao = new ProductDAOExtent();
                CategoryDAO cdao = new CategoryDAO();
                ViewBag.ListCategory = cdao.ListCate();
                ViewBag.CName = "Tìm kiếm ";
                return View(dao.GetProductExtendByName(keywordProduct,pageNum,pageSize));
            }
        }


        public ActionResult News()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MenuChild()
        {
            return PartialView(new CategoryDAO().ListCate());
        }
    }
}