using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Common;
using Web_Cafe.Models.DAO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [IsUserLoginAttribute]//kiểm tra đăng nhập: nếu chưa đăng nhập thì ko vào được
        public ActionResult Index()//thông tin khách hàng
        {
            return View((User)Session["user"]);
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            if(new UserDAOExtend().CheckPassword(u.UserID, u.Password))
            {
                UserDAO dao = new UserDAO();
                dao.UpdatetUser(u);
                return View(u);
            }
            ModelState.AddModelError("", "Mật khẩu không đúng!");
            return View(u);
        }
        [IsUserLoginAttribute]
        public ActionResult EditPassword()
        {
            return View((User)Session["user"]);
        }
        [HttpPost]
        public ActionResult EditPassword(string passwordOld, string passwordNew)
        {
            User u = (User)Session["user"];
            if (new UserDAOExtend().CheckPassword(u.UserID, passwordOld))
            {
                UserDAO dao = new UserDAO();
                dao.ChangePassword(u.UserID, passwordOld);
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", "Mật khẩu không đúng!");
            ViewBag.passwordOld = passwordOld;
            ViewBag.passwordNew = passwordNew;
            return View((User)Session["user"]);
        }
        [IsUserLoginAttribute]
        public ActionResult OrderHistory()
        {
            User u = (User)Session["user"];
            ViewBag.fullName = u.FullName;
            return View(new OrderDAO().lstOrderHistoryById(u.UserID, 1, 6));
        }
        public ActionResult Login()
        {
            if (Session["user"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Login(User u)
        {
            UserDAOExtend dao = new UserDAOExtend();
            User us = dao.Login(u.Username, u.Password);
            if (us != null)
            {
                Session["user"] = us;
                Session["fullName"] = us.FullName;
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            return View(u);
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login", "User");
        }

        public ActionResult Register()
        {
            if (Session["user"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Register(User u)
        {
            UserDAO dao = new UserDAO();
            dao.InsertUser(u);
            return RedirectToAction("Login", "User");
        }
    }
}