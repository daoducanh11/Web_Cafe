using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Models.DAO
{
    public class ProductDAOExtent : ProductDAO
    {
        public List<ProductExtend> ListProductExtend { get; private set; }
        public List<ProductExtend> ListProductSales { get; }
        
        Web_CafeModel db;
        
        public ProductDAOExtent() : base()
        {
            db = new Web_CafeModel();
            this.ListProductExtend = new List<ProductExtend>();
            var listproduct = (from s in db.Products where s.ProStatus != "Không hoạt động" select s);
            foreach (var item in listproduct.Take(8))
            {
                ListProductExtend.Add(new ProductExtend(item));
            }

            this.ListProductSales = new List<ProductExtend>();
            foreach (var item in listproduct)
            {
                try
                {
                    if (ListProductSales.Count < 8 && item.ProStatus == "Khuyến mãi")
                    {
                        if (item.StartTime <= DateTime.Now && item.EndTime > DateTime.Now)
                            ListProductSales.Add(new ProductExtend(item));
                    }
                }
                catch
                {

                }
            }
        }
        public ProductExtend GetProductExtendById(int id)
        {
            foreach (var item in ListProductExtend)
            {
                if (item.Product.ProductID == id)
                {
                    return item;
                }
            }
            foreach (var item in ListProductSales)
            {
                if (item.Product.ProductID == id)
                {
                    return item;
                }

            }
            return null;
        }
        public List<ProductExtend> GetListProductByIDCategory(int categoryID, int productID)
        {
            var listResult = new List<ProductExtend>();
            var lst = (from s in db.Products where s.CategoryID == categoryID select s);
            foreach (var item in lst)
            {
                if (item.ProductID != productID)
                    if (item.CategoryID == categoryID)
                    {
                        if (listResult.Count < 8)
                        {
                            listResult.Add(new ProductExtend(item));
                            if (listResult.Count == 8) 
                                break;
                        }
                    }
            }
            return listResult;
        }

        public List<ProductDTO> ListProductHotExtend()
        {
            var lst = db.Database.SqlQuery<ProductDTO>(" SELECT TOP 8 I.ProductID, P.ProName, P.Price, " +
                " P.PromotionalPrice, P.ProStatus, P.StartTime, P.EndTime, COUNT(I.ProductID) [Count] " +
                " FROM Item I, Product P " +
                " WHERE I.ProductID = P.ProductID AND P.ProStatus != N'Không hoạt động' " +
                " GROUP BY I.ProductID, P.ProName, P.Price, P.PromotionalPrice, P.ProStatus, P.StartTime, P.EndTime " +
                " ORDER BY COUNT(I.ProductID) DESC "
                ).ToList<ProductDTO>();
            foreach (var item in lst)
            {
                var res = db.Database.SqlQuery<ImageDTO>(" SELECT TOP 1 ImageLink " +
                    " FROM Images " +
                    " WHERE ProductID = " + item.ProductID
                    ).ToList<ImageDTO>();
                if (res.Count() > 0)
                    item.ImageLink = res[0].ImageLink;
            }
            return lst;
        }

    }
}