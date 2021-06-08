using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Models.DTO
{
    public class ProductExtend
    {
        public string img_path { get; private set; }
        public Product Product { get; private set; }
        public List<string> List_img_path { get; set; }
        public int Count { get; set; }

        public ProductExtend(Product pr)
        {
            this.Product = pr;
            GetListImgPath();
            try
            {
                this.img_path = List_img_path[0];
            }
            catch
            {
                this.img_path = "cafe.jpg";
            }
        }
        private void GetListImgPath()
        {
            List_img_path = new List<string>();
            foreach (var item in new ImageDAO().GetListImageByProId(Product.ProductID).ToList())
            {
                if (item.ImageLink != null)
                    this.List_img_path.Add(item.ImageLink.ToString());
                else 
                    List_img_path.Add("cafe.jpg");
            }
        }
        

    }
}