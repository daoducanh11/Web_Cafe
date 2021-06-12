using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Areas.Admin.Models.DTO;

namespace Web_Cafe.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            Session["proName"] = "";
            if(Session["username"] != null)
                return RedirectToAction("Index", "AdminHome");
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginDTO model)
        {
            //byte[] temp = ASCIIEncoding.ASCII.GetBytes(model.Password);
            //byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            //string hasPass = "";
            //foreach (byte item in hasData)
            //{
            //    hasPass += item;
            //}
            LoginDAO lo = new LoginDAO();
            Session["idAdmin"] = lo.Login(model.Username, model.Password);
            if (Convert.ToInt32(Session["idAdmin"]) > 0)
            {
                Session["username"] = model.Username;
                Session["isAdmin"] = lo.IsAdmin(model.Username, model.Password);
                return RedirectToAction("Index", "AdminHome");
                //return RedirectToAction("Index", "AdminHome", new { Area = "Admin" });
                //return Redirect("~/Admin/AdminHome/Index");
            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            return View(model);
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index", "AdminLogin");
        }

        public ActionResult CountOrder()
        {
            return PartialView(new OrderDAO().OrderCount());
        }
    }
}