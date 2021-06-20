using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Common;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [IsCartAttribute]//kiểm tra: nếu có sp trong giỏ hàng mới vào được trang này
        public ActionResult Index()//hiển thị sp có trong giỏ hàng
        {
            Session["url"] = Request.Url;//lưu đường dẫn trang hiện tại(phục vụ cho lúc xóa sp)
            Session["OrderIndex"] = 1;
            return View((OrderDTO)Session["shopping_cart"]);
        }
        public ActionResult ShoppingCart_ImportInfo()//điền thông tin mua hàng
        {
            if (Session["OrderIndex"] == null)
                RedirectToAction("Index", "Order");
            Session["OrderShoppingCart_ImportInfo"] = 1;
            OrderDTO order = (OrderDTO)Session["shopping_cart"];
            User u = (User)Session["user"];
            if(u != null)
            {
                order.FullName = u.FullName;
                order.Phone = u.Phone;
                order.Email = u.Email;
                order.DeliveryAddress = u.UserAddress;
            }
            return View(order);
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
        public ActionResult ShoppingCart_Confirmed()//Xác nhận mua hàng
        {
            if (Session["OrderShoppingCart_ImportInfo"] == null)
                RedirectToAction("Index", "Order");
            Session["OrderShoppingCart_Confirmed"] = 1;
            return View((OrderDTO)Session["shopping_cart"]);
        }
        public ActionResult ShoppingCart_Completed()
        {
            if (Session["OrderShoppingCart_Confirmed"] == null)
                RedirectToAction("Index", "Order");
            OrderDTO order = (OrderDTO)Session["shopping_cart"];
            Order o = new Order();
            if (Session["user"] == null)
            {
                //add User
                User u = new User();
                u.FullName = order.FullName;
                u.Phone = order.Phone;
                u.Email = order.Email;
                u.UserAddress = order.DeliveryAddress;
                UserDAO dao = new UserDAO();
                o.UserID = dao.InsertUser(u);
            }
            else
            {
                User us = (User)Session["user"];
                o.UserID = us.UserID;
            }
            o.Daytime = DateTime.Now;
            o.OrderStatus = "Mới";
            o.Amount = order.getTotalMoney();
            o.DeliveryAddress = order.DeliveryAddress;
            o.OrderDescription = order.OrderDescription;
            int oId = new OrderDAO().InsertOrder(o);
            //add Item
            ItemDAO daoItem = new ItemDAO();
            foreach (ItemDTO item in order.listItem)
            {
                Item i = new Item();
                i.ProductID = item.ProductID;
                i.OrderID = oId;
                i.Quantity = item.Quantity;
                i.ItemPrice = item.Price;
                daoItem.InsertItem(i);
            }
            //refresh lại giỏ hàng
            Session["shopping_cart"] = null;
            Session["OrderIndex"] = null;
            Session["OrderShoppingCart_ImportInfo"] = null;
            Session["OrderShoppingCart_Confirmed"] = null;
            return View();
        }

        public ActionResult MiniCart()//biểu tượng giỏ hàng ở phần header
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
            return Redirect(Session["url"].ToString());
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