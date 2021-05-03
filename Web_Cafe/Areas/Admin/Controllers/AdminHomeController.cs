using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        static List<int> arrImgDelete = new List<int>();
        // GET: Admin/AdminHome
        public ActionResult Index(int pageNum = 1, int pageSize = 4)
        {
            ProductDAO dao = new ProductDAO();
            return View(dao.lstJoin(pageNum, pageSize));
        }
        public ActionResult Create()
        {
            CategoryDAO dao = new CategoryDAO();
            return View(dao.ListCate());
        }
        [HttpPost]
        public ActionResult Create(string proName, string proHighlight, string proDescription,
            string price, string promoPrice, string timePromo, string categoryId, string proStatus,
            IEnumerable<HttpPostedFileBase> filesImg)
        {
            //Product pro = new Product();
            //pro.ProName = proName;
            //pro.Highlight = proHighlight;
            //pro.ProDescription = proDescription;
            //double pri;
            //if (double.TryParse(price, out pri))
            //    pro.Price = pri;
            //else
            //    pro.Price = 0;
            //if (double.TryParse(promoPrice, out pri))
            //    pro.PromotionalPrice = pri;
            //else
            //    pro.PromotionalPrice = 0;
            //string[] arr = timePromo.Split('-');
            //pro.StartTime = Convert.ToDateTime(arr[0]);
            //pro.EndTime = Convert.ToDateTime(arr[1]);
            //pro.ProStatus = proStatus;
            //pro.CategoryID = Int32.Parse(categoryId);
            //ProductDAO dao = new ProductDAO();
            //Image img = new Image();
            //img.ProductID = dao.InsertProduct(pro);

            //SaveImage(img, filesImg);
            //image
            //ImageDAO daoImage = new ImageDAO();
            //string fName = "";
            //try
            //{
            //    //loop through all the files
            //    foreach (var file in filesImg)
            //    {
            //        //Save file content goes here
            //        fName = file.FileName;
            //        if (file != null && file.ContentLength > 0)
            //        {
            //            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\", Server.MapPath(@"\")));
            //            string pathString = System.IO.Path.Combine(originalDirectory.ToString());
            //            var path = string.Format("{0}{1}", pathString, file.FileName);
            //            file.SaveAs(path);
            //            img.ImageLink = file.FileName;
            //            daoImage.InsertImage(img);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            return RedirectToAction("Index", "AdminHome");
        }
        public void SaveImage(Image img, IEnumerable<HttpPostedFileBase> filesImg)
        {
            if(filesImg != null)
            {
                ImageDAO daoImage = new ImageDAO();
                string fName = "";
                try
                {
                    //loop through all the files
                    foreach (var file in filesImg)
                    {
                        //Save file content goes here
                        fName = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\", Server.MapPath(@"\")));
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString());
                            var path = string.Format("{0}{1}", pathString, file.FileName);
                            file.SaveAs(path);
                            img.ImageLink = file.FileName;
                            daoImage.InsertImage(img);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
    
        }
        public ActionResult Edit(int id)
        {
            Session["id"] = id;
 
            ProductDAO dao = new ProductDAO();
            Product pro = new Product();
            pro = dao.FindProductByID(id);

            CategoryDAO daoCate = new CategoryDAO();
            ViewBag.cate = daoCate.ListCate();

            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(string proName, string proHighlight, string proDescription,
            string price, string promoPrice, string timePromo, string categoryId, string proStatus,
            IEnumerable<HttpPostedFileBase> filesImg)
        {
            Product pro = new Product();
            pro.ProductID = Convert.ToInt32(Session["id"].ToString());
            pro.ProName = proName;
            pro.Highlight = proHighlight;
            pro.ProDescription = proDescription;

            double pri;
            if (double.TryParse(price, out pri))
                pro.Price = pri;
            else
                pro.Price = 0;
            if (double.TryParse(promoPrice, out pri))
                pro.PromotionalPrice = pri;
            else
                pro.PromotionalPrice = 0;
            string[] arr = timePromo.Split('-');

            DateTime dt;
            if (DateTime.TryParse(arr[0], out dt))
                pro.StartTime = dt;
            else
                pro.StartTime = null;
            if (DateTime.TryParse(arr[1], out dt))
                pro.EndTime = dt;
            else
                pro.EndTime = null;
            pro.ProStatus = proStatus;
            pro.CategoryID = Int32.Parse(categoryId);
            ProductDAO dao = new ProductDAO();
            Image img = new Image();
            img.ProductID = dao.InsertProduct(pro);

            SaveImage(img, filesImg);
            foreach (int item in arrImgDelete)
            {
                ImageDAO daoImg = new ImageDAO();
                daoImg.DeleteImage(item);
            }
            return RedirectToAction("Index", "AdminHome");
        }
        public ActionResult GetImage()
        {
            //Get the images list from repository
            //var ImageList = new List<ImageDTO>
            //{
            //    new ImageDTO {ImageID = 1,
            //    ImageLink = "/Images/1.jpg",
            //    Path = "/Images/1.jpg"},
            //    new ImageDTO {ImageID = 2,
            //    ImageLink = "/Images/2.jpg",
            //    Path = "/Images/2.jpg"}
            //}.ToList();
            ImageDAO dao = new ImageDAO();
            var ImageList = dao.GetListImageDtoByProId(Convert.ToInt32(Session["id"].ToString()));
            return Json(new { Data = ImageList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ActionDeleteImg()
        {
            arrImgDelete.Add(Convert.ToInt32(Request.Form["imgId"]));
            return Json("Xử lý thành công", JsonRequestBehavior.AllowGet);
        }
    }
}