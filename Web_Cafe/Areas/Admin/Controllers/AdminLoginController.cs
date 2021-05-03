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
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginDTO model)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(model.Password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            LoginDAO lo = new LoginDAO();
            if (lo.Login(model.Username, hasPass))
            {
                Session["username"] = model.Username;
                return RedirectToAction("Index", "AdminHome");
            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            return View(model);
        }
    }
}