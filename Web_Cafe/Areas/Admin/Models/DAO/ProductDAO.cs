using PagedList;
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

        public IEnumerable<ProductDTO> lstSearchProById(int proId, int categoryId, double minPrice, double maxPrice, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("lstSearchProById " +
                proId + ", " + categoryId + ", " + minPrice + ", " + maxPrice
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchProByName(string proName, int categoryId, double minPrice, double maxPrice, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("lstSearchProByName " +
                "N'" + proName + "', " + categoryId + ", " + minPrice + ", " + maxPrice
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchProByNameUnique(string proName, int categoryId, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("lstSearchProByNameUnique " +
                "N'" + proName + "', " + categoryId
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchProByCateId(int categoryId, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("SELECT ProductID, ProName, Price, " +
                " PromotionalPrice, ProStatus, CateName " +
                " FROM Product P, Category C " +
                " WHERE P.CategoryID = " + categoryId +" AND P.CategoryID = C.CategoryID "
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }

        public int UpdatetProduct(Product proTmp)
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
        public int InsertProduct(Product pro)
        {
            db.Products.Add(pro);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return pro.ProductID;
        }
        public Product FindProductByID(int id)
        {
            return db.Products.Find(id);
        }
        public int Delete(int id)
        {
            Product pro = db.Products.Find(id);
            if (pro != null)
            {
                db.Products.Remove(pro);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        #region UserCode
        public IEnumerable<ProductDTO> lstPro()
        {
            var lst = db.Database.SqlQuery<ProductDTO>("SELECT TOP 8 ProductID, ProName, Price, " +
                " PromotionalPrice, ProStatus, CateName " +
                " FROM Product P, Category C " +
                " WHERE P.CategoryID = C.CategoryID "
                ).ToList<ProductDTO>();
            foreach (var item in lst)
            {
                var res = db.Database.SqlQuery<ImageDTO>(" SELECT TOP 1 ImageLink " +
                " FROM Images " +
                " WHERE ProductID = " + item.ProductID
                ).ToList<ImageDTO>();
                if(res.Count() > 0)
                    item.ImageLink = res[0].ImageLink;
            }
            return lst;
        }
        #endregion
    }
}