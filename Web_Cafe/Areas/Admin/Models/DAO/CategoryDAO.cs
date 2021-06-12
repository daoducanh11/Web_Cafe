using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class CategoryDAO
    {
        Web_CafeModel db;
        public CategoryDAO()
        {
            db = new Web_CafeModel();
        }
        public List<Category> ListCate()
        {
            return db.Categories.ToList();
        }
        public List<Category> ListCateInProduct()
        {
            return (from s in db.Categories where s.CateStatus != "Không hoạt động" select s).ToList();
        }
        public Category GetById(int id)
        {
            return db.Categories.Single(i => i.CategoryID == id);
        }
        public int InsertCategory(Category cate)
        {
            db.Categories.Add(cate);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return cate.CategoryID;
        }
        public int UpdatetCategory(Category cateTmp)
        {
            Category cate = db.Categories.Find(cateTmp.CategoryID);
            if (cate != null)
            {
                cate.CateName = cateTmp.CateName;
                cate.CateStatus = cateTmp.CateStatus;
                db.SaveChanges();//luu vao o dia
            }
            return cate.CategoryID;
        }
        public int Delete(int id)
        {
            Category cate = db.Categories.Find(id);
            if (cate != null)
            {
                db.Categories.Remove(cate);
                return db.SaveChanges();
            }
            return -1;
        }
    }
}