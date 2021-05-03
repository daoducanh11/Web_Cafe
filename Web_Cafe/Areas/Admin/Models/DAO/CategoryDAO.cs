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
        //public IQueryable<Category> Categories
        //{
        //    get { return db.Categories; }
        //}
        //public IQueryable<Category> ListCategory()
        //{
        //    var res = (from s in db.Categories select s);
        //    return res;
        //}
        public List<Category> ListCate()
        {
            return db.Categories.ToList();
        }
        public Category GetById(int id)
        {
            return db.Categories.Single(i => i.CategoryID == id);
        }
    }
}