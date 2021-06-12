using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Common;

namespace Web_Cafe.Areas.Admin.Controllers
{
    [IsLogin]
    public class AdminAccountController : Controller
    {
        // GET: Admin/AdminAccount
        [AuthorizationAdmin]
        public ActionResult Index(string keywords, string adminType, int pageNum = 1, int pageSize = 6)
        {
            if (keywords == null)
                keywords = "";
            ViewBag.keywords = keywords;
            if (adminType == null)
                adminType = "";
            ViewBag.adminType = adminType;
            return View(new AccountDAO().listSearchAccount(keywords, adminType, pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Create(string username, string password, string adminType)
        {
            Web_Cafe.Models.Entities.Admin u = new Web_Cafe.Models.Entities.Admin();
            u.Username = username;
            u.Password = password;
            u.AdminType = adminType;
            AccountDAO dao = new AccountDAO();
            dao.InsertAccount(u);
            return RedirectToAction("Index", "AdminAccount");
        }

        [AuthorizationAdmin]
        public ActionResult Edit(int id)
        {
            Web_Cafe.Models.Entities.Admin u = new Web_Cafe.Models.Entities.Admin();
            u = new AccountDAO().FindAccountByID(id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(int id, string passwordEdit, string adminTypeEdit)
        {
            if (ModelState.IsValid)
            {
                AccountDAO dao = new AccountDAO();
                dao.UpdatetAccount(id, passwordEdit, adminTypeEdit);
                return RedirectToAction("Index", "AdminAccount");
            }
            return PartialView();
        }

        public ActionResult Profile()
        {
            Web_Cafe.Models.Entities.Admin u = new Web_Cafe.Models.Entities.Admin();
            u = new AccountDAO().FindAccountByID(Convert.ToInt32(Session["idAdmin"]));
            return View(u);
        }
        [HttpPost]
        public ActionResult Profile(string usernameProfile, string passwordEditProfile)
        {
            if (ModelState.IsValid)
            {
                AccountDAO dao = new AccountDAO();
                if(dao.UpdatetAccountProfile(Convert.ToInt32(Session["idAdmin"]), usernameProfile, passwordEditProfile))
                {
                    Session["username"] = usernameProfile;
                    return RedirectToAction("Index", "AdminHome");
                }    
            }
            ModelState.AddModelError("", "Lỗi!");
            return View();
        }
        public JsonResult CheckPass(string passwordProfile)
        {
            bool res = false;
            if (new AccountDAO().CheckPass(Convert.ToInt32(Session["idAdmin"]), passwordProfile))
                res = true;
            return Json(new
            {
                status = res
            }, JsonRequestBehavior.AllowGet) ;
        }
    }
}