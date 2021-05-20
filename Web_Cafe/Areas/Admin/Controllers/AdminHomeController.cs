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
        public ActionResult Index(string keywords, string categoryId, string minPrice, string maxPrice, int pageNum = 1, int pageSize = 9)
        {
            //if(Session["username"] == null)
            //return RedirectToAction("Index", "AdminLogin");
            int proId;
            int cateId = Convert.ToInt32(categoryId);
            ProductDAO dao = new ProductDAO();
            CategoryDAO daoCate = new CategoryDAO();
            ViewBag.keywords = keywords;
            ViewBag.categoryId = cateId;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.cate = daoCate.ListCate();
            double min, max;
            if (!double.TryParse(minPrice, out min))
                min = 0;
            if (!double.TryParse(maxPrice, out max))
                max = 10000000;
            if (keywords == null)
                keywords = "";
            if (keywords == "" && cateId == 0 && min == 0 && max == 10000000)
                return View(dao.lstJoin(pageNum, pageSize));
            if (int.TryParse(keywords, out proId))
                return View(dao.listSearchProById(proId, cateId, min, max, pageNum, pageSize));
            return View(dao.listSearchProByName(keywords, cateId, min, max, pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            ProductDAO dao = new ProductDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            CategoryDAO daoCate = new CategoryDAO();
            ViewBag.cate = daoCate.ListCate();
            return View(dao.lstJoin(pageNum, pageSize));
        }
        public ActionResult ShowImage(int id)
        {
            ImageDAO dao = new ImageDAO();
            var img = dao.GetListImageDtoByProId(id);
            ViewBag.ListImg = img;
            return PartialView();
        }

        public ActionResult Create()
        {
            CategoryDAO dao = new CategoryDAO();
            ViewBag.cate = dao.ListCate();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product proTmp, string timePromo, string categoryId, string proStatus,
            IEnumerable<HttpPostedFileBase> filesImg)
        {
            //if(proName == "")
            //{
            //    ModelState.AddModelError("", "Tên sản phẩm không được để trống!");
            //    CategoryDAO d = new CategoryDAO();
            //    return View(d.ListCate());
            //}
            if (ModelState.IsValid)
            {
                if (Session["proName"].ToString() == proTmp.ProName)
                    return RedirectToAction("Index", "AdminHome");
                Product pro = new Product();
                pro.ProName = proTmp.ProName;
                pro.Highlight = proTmp.Highlight;
                pro.ProDescription = proTmp.ProDescription;
                pro.Price = proTmp.Price;
                pro.PromotionalPrice = proTmp.PromotionalPrice;
                string[] arr = timePromo.Split('-');
                pro.StartTime = Convert.ToDateTime(arr[0]);
                pro.EndTime = Convert.ToDateTime(arr[1]);
                pro.ProStatus = proStatus;
                pro.CategoryID = Int32.Parse(categoryId);
                
                Session["proName"] = pro.ProName;

                ProductDAO dao = new ProductDAO();
                Image img = new Image();
                img.ProductID = dao.InsertProduct(pro);

                SaveImage(img, filesImg);

                return RedirectToAction("Index", "AdminHome");    
            }
            CategoryDAO daoCate = new CategoryDAO();
            ViewBag.cate = daoCate.ListCate();
            return View(proTmp);
        }
        
        public ActionResult Edit(int id)
        {
            Session["id"] = id;
            ProductDAO dao = new ProductDAO();
            Product pro = new Product();
            pro = dao.FindProductByID(id);
            if (pro.StartTime == null)
                pro.StartTime = DateTime.Now;
            if (pro.EndTime == null)
                pro.EndTime = DateTime.Now;
            CategoryDAO daoCate = new CategoryDAO();
            ViewBag.cate = daoCate.ListCate();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(Product proTmp, string timePromo, string categoryId, string proStatus,
            IEnumerable<HttpPostedFileBase> filesImg)
        {
            Product pro = new Product();
            pro.ProductID = Convert.ToInt32(Session["id"].ToString());
            pro.ProName = proTmp.ProName;
            pro.Highlight = proTmp.Highlight;
            pro.ProDescription = proTmp.ProDescription;
            pro.Price = proTmp.Price;
            pro.PromotionalPrice = proTmp.PromotionalPrice;
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
            if (ModelState.IsValid)
            {
                //if (Session["proName"].ToString() == proTmp.ProName)
                    //return RedirectToAction("Index", "AdminHome");
                Image img = new Image();
                img.ProductID = dao.UpdatetProduct(pro);

                SaveImage(img, filesImg);

                foreach (int item in arrImgDelete)
                {
                    ImageDAO daoImg = new ImageDAO();
                    daoImg.DeleteImage(item);
                }
                return RedirectToAction("Index", "AdminHome");
            }
            CategoryDAO daoCate = new CategoryDAO();
            ViewBag.cate = daoCate.ListCate();
            return View(pro);
        }

        public ActionResult Delete(int id)
        {
            ProductDAO dao = new ProductDAO();
            dao.Delete(id);
            return Redirect("~/Admin/AdminHome/Index");
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
        public void SaveImage(Image img, IEnumerable<HttpPostedFileBase> filesImg)
        {
            if (filesImg != null)
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

    }
}