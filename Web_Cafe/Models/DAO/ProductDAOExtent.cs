using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Models.DTO;

namespace Web_Cafe.Models.DAO
{
    public class ProductDAOExtent : ProductDAO
    {
        public List<ProductExtend> ListProductExtend { get; private set; }
        public List<ProductExtend> ListProductSales { get; }
        public ProductDAOExtent() : base()
        {
            this.ListProductExtend = new List<ProductExtend>();
            var listproduct = this.ListProduct().ToList();
            foreach (var item in listproduct.Take(8))
            {
                ListProductExtend.Add(new ProductExtend(item));
            }
            this.ListProductSales = new List<ProductExtend>();
            foreach (var item in listproduct)
            {
                try
                {
                    var time = (DateTime)item.EndTime;
                    if (time != null)
                        if (DateTime.Compare(time, DateTime.Now) > 0)
                            if (ListProductSales.Count < 8)
                                ListProductSales.Add(new ProductExtend(item));
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
            foreach (var item in this.ListProduct().ToList())
            {
                if (item.ProductID != productID)
                    if (item.CategoryID == categoryID)
                    {
                        if (listResult.Count < 4)
                        {
                            listResult.Add(new ProductExtend(item));
                            if (listResult.Count == 4) break;
                        }
                    }
            }
            return listResult;

        }


    }
}