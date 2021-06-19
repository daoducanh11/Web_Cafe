using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Models.DAO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
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
            Session["user"] = dao.Login(u.Username, u.Password);
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
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
            return View();
        }
    }
}