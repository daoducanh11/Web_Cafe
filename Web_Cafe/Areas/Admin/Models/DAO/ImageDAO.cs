using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class ImageDAO
    {
        Web_CafeModel db;
        public ImageDAO()
        {
            db = new Web_CafeModel();
        }
        public IQueryable<Image> Images
        {
            get { return db.Images; }
        }
        //public IQueryable<Image> ListImage()
        //{
        //    var res = (from s in db.Images select s);
        //    return res;
        //}
        //public IEnumerable<ProductDTO> lstJoin(int pageNum, int pageSize)
        //{
        //    var lst = db.Database.SqlQuery<ProductDTO>("SELECT ProductID, ProName, Price, " +
        //        " PromotionalPrice, ProStatus, CateName " +
        //        " FROM Product P, Category C " +
        //        " WHERE P.CategoryID = C.CategoryID "
        //        ).ToPagedList<ProductDTO>(pageNum, pageSize);
        //    return lst;
        //}
        public int InsertImage(Image img)
        {
            db.Images.Add(img);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return img.ImageID;
        }
        public void DeleteImage(int id)
        {
            Image img = db.Images.Find(id);
            if (img != null)
            {
                db.Images.Remove(img);
                db.SaveChanges();
            }
        }
        public IQueryable<Image> GetListImageByProId(int id)
        {
            var res = (from s in db.Images where s.ProductID == id select s);
            return res;
        }
        public List<ImageDTO> GetListImageDtoByProId(int id)
        {
            var lst = db.Database.SqlQuery<ImageDTO>(" SELECT ImageID, ImageLink " +
                " FROM Images " +
                " WHERE ProductID = " + id
                ).ToList<ImageDTO>();
            foreach(var item in lst)
            {
                item.ImageLink = "/Images/" + item.ImageLink;
            }
            return lst;
        }
    }
}