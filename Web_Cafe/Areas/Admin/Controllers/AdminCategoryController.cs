using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Controllers
{
    public class AdminCategoryController : Controller
    {
        // GET: Admin/AdminCategory
        public ActionResult Index()
        {
            CategoryDAO dao = new CategoryDAO();
            return View(dao.ListCate());
        }
        public ActionResult Create()
        {
            if (Session["username"] == null)
                return Redirect("~/Admin/AdminLogin/Index");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category cateTemp, string cateStatus)
        {
            if (ModelState.IsValid)
            {
                Category cate = new Category();
                cate.CateName = cateTemp.CateName;
                cate.CateStatus = cateStatus;
                CategoryDAO dao = new CategoryDAO();
                dao.InsertCategory(cate);
                return RedirectToAction("Index", "AdminCategory");
            }
            return PartialView();
        }
        public ActionResult Edit(int id)
        {
            if (Session["username"] == null)
                return Redirect("~/Admin/AdminLogin/Index");
            Category cate = new Category();
            CategoryDAO dao = new CategoryDAO();
            cate = dao.GetById(id);
            return View(cate);
        }
        [HttpPost]
        public ActionResult Edit(int id, string CateName, string cateStatus)
        {
            if (ModelState.IsValid)
            {
                Category cate = new Category();
                cate.CategoryID = id;
                cate.CateName = CateName;
                cate.CateStatus = cateStatus;
                CategoryDAO dao = new CategoryDAO();
                dao.UpdatetCategory(cate);
                return RedirectToAction("Index", "AdminCategory");
            }
            return PartialView();
        }
        public ActionResult Delete(int id)
        {
            CategoryDAO dao = new CategoryDAO();
            dao.Delete(id);
            return RedirectToAction("Index", "AdminCategory");
        }
    }
}