using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;

using Web_Cafe.Models.DAO;
using Web_Cafe.Models.DTO;

namespace Web_Cafe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductDAOExtent dao = new ProductDAOExtent();
            ViewBag.ListProductHotExtend = dao.ListProductHotExtend();
            ViewBag.ListProductSales = dao.GetListProductSales();
            Session["url"] = Request.Url;
            //if (Session["shopping_cart"] == null)
            //    Session["shopping_cart"] = dao.GetListProductSales();
            return View(dao.GetListProductExtend());
        }
        //[HttpPost]
        //public JavaScriptResult AddProductToShoppingCart(int ProductID)
        //{
        //    if (Session["shopping_cart"] == null)
        //        Session["shopping_cart"] = new List<ProductExtend>();
        //    var product = new ProductDAOExtent().GetProductExtendById(ProductID);
        //    var ListShoppingCart = (List<ProductExtend>)Session["shopping_cart"];
        //    ListShoppingCart.Add(product);
        //    Session["shopping_cart"] = ListShoppingCart;
        //    return JavaScript(alert("Hello this is an alert"));
        //}
        public ActionResult SearchProduct(string keywordProduct = "", string id = "", int pageNum = 1, int pageSize = 6)
        {
            Session["url"] = Request.Url;
            ViewBag.keywordProduct = keywordProduct;
            ViewBag.id = id;
            ProductDAOExtent dao = new ProductDAOExtent();
            CategoryDAO cdao = new CategoryDAO();
            ViewBag.ListCategory = cdao.ListCateInProduct();
            if (id != "")
            {
                ViewBag.CName = cdao.ListCateInProduct().ToList().Where(i => i.CategoryID.ToString().Contains(id)).FirstOrDefault().CateName;
            }
            else
            {
                id = "0";
                ViewBag.CName = "Tìm kiếm";
            }
            return View(dao.GetProductExtendByName(keywordProduct, Convert.ToInt32(id), pageNum, pageSize));
        }

        public ActionResult ShoppingCart_Completed()
        {
            return View();
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
            return PartialView(new CategoryDAO().ListCateInProduct());
        }
    }
}