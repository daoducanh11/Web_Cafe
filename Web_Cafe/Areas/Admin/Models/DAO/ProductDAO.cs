﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class ProductDAO
    {
        Web_CafeModel db;
        public ProductDAO()
        {
            db = new Web_CafeModel();
        }
        public IQueryable<Product> Products
        {
            get { return db.Products; }
        }
        public IQueryable<Product> ListProduct()
        {
            var res = (from s in db.Products select s);
            return res;
        }
        public IEnumerable<ProductDTO> lstJoin(int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("SELECT ProductID, ProName, Price, " +
                " PromotionalPrice, ProStatus, CateName " +
                " FROM Product P, Category C " +
                " WHERE P.CategoryID = C.CategoryID "
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public int InsertProduct(Product proTmp)
        {
            Product pro = db.Products.Find(proTmp.ProductID);
            if (pro != null)
            {
                pro.ProName = proTmp.ProName;
                pro.Highlight = proTmp.Highlight;
                pro.ProDescription = proTmp.ProDescription;
                pro.Price = proTmp.Price;
                pro.PromotionalPrice = proTmp.PromotionalPrice;
                pro.ProName = proTmp.ProName;
                pro.StartTime = proTmp.StartTime;
                pro.EndTime = proTmp.EndTime;
                pro.ProStatus = proTmp.ProStatus;
                pro.CategoryID = proTmp.CategoryID;
                db.SaveChanges();//luu vao o dia
            }
            return pro.ProductID;
        }
        public int UpdatetProduct(Product pro)
        {
            db.Products.Add(pro);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return pro.ProductID;
        }
        public Product FindProductByID(int id)
        {
            return db.Products.Find(id);
        }
    }
}