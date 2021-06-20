using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Controllers
{
    public class AdminOrderController : Controller
    {
        // GET: Admin/AdminOrder
        public ActionResult Index(string proName, string fullName, string status, int pageNum = 1, int pageSize = 6)
        {
            Session["cartAdmin"] = null;
            ViewBag.proName = proName;
            ViewBag.fullName = fullName;
            ViewBag.status = status;
            return View(new OrderDAO().GetListOrder(proName, fullName, status, pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 6)
        {
            OrderDAO dao = new OrderDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            return View(dao.GetListOrder(ViewBag.proName, ViewBag.fullName, ViewBag.status, pageNum, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Order o, string idUs, string phoneUs, string fullName, string phone, string email)
        {
            o.Daytime = DateTime.Now;
            o.OrderStatus = "Mới";
            if (idUs == "" || (idUs != "" && phoneUs != phone))
            {
                //add User
                User u = new User();
                u.FullName = fullName;
                u.Phone = phone;
                u.Email = email;
                u.UserAddress = o.DeliveryAddress;
                UserDAO dao = new UserDAO();
                o.UserID = dao.InsertUser(u);
                CreateOrder(o);
            }
            else
            {
                o.UserID = Convert.ToInt32(idUs);
                CreateOrder(o);
            }
            return RedirectToAction("Index", "AdminOrder");
        }
        public void CreateOrder(Order o)
        {
            OrderDTO order = (OrderDTO)Session["cartAdmin"];
            //add Order
            o.Amount = order.getTotalMoney();
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
        }
        public JsonResult ListProduct(string key)
        {
            var data = new ProductDAO().lstSearchProByNameInOrder(key);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddItem(int id)
        {
            OrderDTO order = (OrderDTO)Session["cartAdmin"];
            if (order == null)
                order = new OrderDTO();
            ItemDTO item = new ItemDTO(id, 1);
            order.addItem(item);
            Session["cartAdmin"] = order;
            return Json(new
            {
                data = new ResultSum
                {
                    id = id,
                    value = order.getSum(id, 1),
                    totalMoney = order.getTotalMoney()
                },
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int id)
        {
            OrderDTO order = (OrderDTO)Session["cartAdmin"];
            order.deleteItem(id);
            Session["cartAdmin"] = order;
            return Json(new
            {
                data = new ResultSum
                {
                    totalMoney = order.getTotalMoney()
                },
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateItem(int id, int quantity)
        {
            OrderDTO order = (OrderDTO)Session["cartAdmin"];
            order.updateItem(id, quantity);
            Session["cartAdmin"] = order;
            return Json(new
            {
                data = new ResultSum
                {
                    id = id,
                    value = order.getSum(id, quantity),
                    totalMoney = order.getTotalMoney()
                },
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListFullName(string key)
        {
            var data = new UserDAO().listSearchUserByNameInOrder(key);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            Session["cartAdmin"] = new OrderDTO().FindOrderByID(id);
            OrderDTO o = (OrderDTO)Session["cartAdmin"];
            Session["userID"] = o.UserID;
            return View(o);
        }
        [HttpPost]
        public ActionResult Edit(int id, OrderDTO o, string idUs, string status)
        {
            o.OrderID = id;
            o.OrderStatus = status;
            UserDAO dao = new UserDAO();
            if (idUs == "" || Convert.ToInt32(idUs) == o.UserID)
            {
                User u = dao.FindUserByID(Convert.ToInt32(o.UserID));
                u.FullName = o.FullName;
                u.Phone = o.Phone;
                u.Email = o.Email;
                u.UserAddress = o.DeliveryAddress;
                dao.UpdatetUser(u);
            }
            else if(Convert.ToInt32(idUs) != o.UserID)
            {
                //add User
                User u = new User();
                u.FullName = o.FullName;
                u.Phone = o.Phone;
                u.Email = o.Email;
                u.UserAddress = o.DeliveryAddress;
                o.UserID = dao.InsertUser(u);
            }
            OrderDTO order = (OrderDTO)Session["cartAdmin"];
            o.Amount = order.getTotalMoney();
            //update Order
            OrderDAO daoOrder = new OrderDAO();
            daoOrder.UpdatetOrder(o);
            //update Item
            ItemDAO daoItem = new ItemDAO();
            foreach (ItemDTO item in order.listItem)
            {
                Item i = new ItemDAO().FindItem(item.ProductID, id);
                if (i == null)
                {
                    i = new Item();
                    i.ProductID = item.ProductID;
                    i.OrderID = id;
                    i.Quantity = item.Quantity;
                    i.ItemPrice = item.Price;
                    daoItem.InsertItem(i);
                }
                else
                {
                    i.Quantity = item.Quantity;
                    i.ItemPrice = item.Price;
                    daoItem.UpdatetItem(i);
                }
            }
            return RedirectToAction("Index", "AdminOrder");
        }

        public ActionResult Delete(int id)
        {
            OrderDAO dao = new OrderDAO();
            dao.Delete(id);
            return Redirect("~/Admin/AdminOrder/Index");
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("Invoice", new { id = id });
        }
        public ActionResult Invoice(int id)
        {
            OrderDTO o = new OrderDTO().FindOrderByID(id);
            return View(o);
        }
    }
    public class ResultSum
    {
        public int id { get; set; }
        public double value { get; set; }
        public double totalMoney { get; set; }
    }
}