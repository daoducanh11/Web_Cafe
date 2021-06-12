using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Controllers
{
    public class AdminNewsController : Controller
    {
        // GET: Admin/AdminNews
        public ActionResult Index(string keywords, string startTime, string endTime, int pageNum = 1, int pageSize = 9)
        {
            if (startTime == null || startTime == "")
                startTime = new DateTime(2020, 11, 19).ToString("yyyy-MM-dd HH:mm:ss");
            if (endTime == null || endTime == "")
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (keywords == null)
                keywords = "";
            ViewBag.startTime = startTime;
            ViewBag.endTime = endTime;
            ViewBag.keywords = keywords;
            return View(new NewsDAO().lstNews(keywords, startTime, endTime, pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            NewsDAO dao = new NewsDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            return View(dao.lstNews(ViewBag.keywords, ViewBag.startTime, ViewBag.endTime, pageNum, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(News tmp, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                News news = new News();
                news.NewsTitle = tmp.NewsTitle;
                news.NewsDescription = tmp.NewsDescription;
                news.DayTime = DateTime.Now;
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/"), System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    news.ImageLink = photo.FileName;
                }
                NewsDAO dao = new NewsDAO();
                dao.InsertNews(news);
                return RedirectToAction("Index", "AdminNews");
            }
            return View(tmp);
        }

        public ActionResult Edit(int id)
        {
            return View(new NewsDAO().FindNewsByID(id));
        }
        [HttpPost]
        public ActionResult Edit(News tmp, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/"), System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    tmp.ImageLink = photo.FileName;
                }
                NewsDAO dao = new NewsDAO();
                dao.InsertNews(tmp);
                return RedirectToAction("Index", "AdminNews");
            }
            return View(tmp);
        }

        public ActionResult Delete(int id)
        {
            NewsDAO dao = new NewsDAO();
            dao.Delete(id);
            return Redirect("~/Admin/AdminNews/Index");
        }
    }
}