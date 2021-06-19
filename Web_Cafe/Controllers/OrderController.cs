using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DTO;

namespace Web_Cafe.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View((OrderDTO)Session["shopping_cart"]);
        }
        public ActionResult ShoppingCart_ImportInfo()
        {
            return View((OrderDTO)Session["shopping_cart"]);
        }
        [HttpPost]
        public ActionResult ShoppingCart_ImportInfo(OrderDTO o)
        {
            OrderDTO order = (OrderDTO)Session["shopping_cart"];
            order.FullName = o.FullName;
            order.Phone = o.Phone;
            order.Email = o.Email;
            order.DeliveryAddress = o.DeliveryAddress;
            order.OrderDescription = o.OrderDescription;
            Session["shopping_cart"] = order;
            return RedirectToAction("ShoppingCart_Confirmed", "Order");
        }
        public ActionResult ShoppingCart_Confirmed()
        {
            return View((OrderDTO)Session["shopping_cart"]);
        }
        public ActionResult ShoppingCart_Completed()
        {

            Session["shopping_cart"] = null;
            return View();
        }


        public ActionResult MiniCart()
        {
            OrderDTO order = (OrderDTO)Session["shopping_cart"];
            if (order == null)
                order = new OrderDTO();
            return PartialView(order);
        }
        public ActionResult AddItem(int id)
        {
            OrderDTO order = (OrderDTO)Session["shopping_cart"];
            if (order == null)
                order = new OrderDTO();
            ItemDTO item = new ItemDTO(id, 1);
            order.addItem(item);
            Session["shopping_cart"] = order;
            return Redirect(Session["url"].ToString());
        }
        public ActionResult DeleteItem(int id)
        {
            OrderDTO order = (OrderDTO)Session["shopping_cart"];
            order.deleteItem(id);
            Session["shopping_cart"] = order;
            return RedirectToAction("Index", "Order");
        }
        public ActionResult UpdateItem(int id, int quantity)
        {
            OrderDTO order = (OrderDTO)Session["shopping_cart"];
            order.updateItem(id, quantity);
            Session["shopping_cart"] = order;
            return RedirectToAction("Index", "Order");
        }
    }
}