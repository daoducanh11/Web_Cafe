using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: Admin/AdminUser
        public ActionResult Index(string keywords, int pageNum = 1, int pageSize = 9)
        {
            UserDAO dao = new UserDAO();
            ViewBag.keywords = keywords;
            if (keywords == null)
                keywords = "";
            return View(dao.listSearchUserByName(keywords, pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            UserDAO dao = new UserDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            return View(dao.listSearchUserByName("", pageNum, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User u)
        {
            UserDAO dao = new UserDAO();
            dao.InsertUser(u);
            return RedirectToAction("Index", "AdminUser");
        }
        public ActionResult Edit(int id)
        {
            if (Session["username"] == null)
                return Redirect("~/Admin/AdminLogin/Index");
            User u = new User();
            u = new UserDAO().FindUserByID(id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(int id, User tmp)
        {
            if (ModelState.IsValid)
            {
                User u = new User();
                u.UserID = id;
                u.FullName = tmp.FullName;
                u.Username = tmp.Username;
                u.Email = tmp.Email;
                u.Phone = tmp.Phone;
                u.UserAddress = tmp.UserAddress;
                UserDAO dao = new UserDAO();
                dao.UpdatetUser(u);
                return RedirectToAction("Index", "AdminUser");
            }
            return PartialView();
        }
        public ActionResult EditPassword(int id)
        {
            if (Session["username"] == null)
                return Redirect("~/Admin/AdminLogin/Index");
            return View(id);
        }
        [HttpPost]
        public ActionResult EditPassword(int id, string password)
        {
            if (ModelState.IsValid)
            {
                UserDAO dao = new UserDAO();
                dao.ChangePassword(id, password);
                return RedirectToAction("Index", "AdminUser");
            }
            return PartialView();
        }
        public ActionResult ListOrder(int id, string startTime, string endTime)
        {
            ViewBag.userId = id;
            if (startTime == null || startTime == "")
                startTime = new DateTime(2020, 11, 19).ToString("yyyy-MM-dd HH:mm:ss");
            if (endTime == null || endTime == "")
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.startTime = startTime;
            ViewBag.endTime = endTime;
            OrderDAO dao = new OrderDAO();
            return View(dao.lstOrderById(id, startTime, endTime, 1, 9));
        }
        public ActionResult Delete(int id)
        {
            UserDAO dao = new UserDAO();
            dao.Delete(id);
            return Redirect("~/Admin/AdminUser/Index");
        }
    }
}