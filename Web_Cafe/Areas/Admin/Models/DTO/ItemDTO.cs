using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DTO
{
    public class ItemDTO
    {
        public int ProductID { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public string ImageLink { get; set; }
        public string ProName { get; set; }
        public ItemDTO()
        {
            
        }
        public ItemDTO(int id, int quantity)
        {
            this.ProductID = id;
            this.Quantity = quantity;
            Product pro = new ProductDAO().FindProductByID(id);
            if (pro.ProStatus == "Khuyến mãi" && pro.StartTime < DateTime.Now && pro.EndTime < DateTime.Now)
                this.Price = Convert.ToDouble(pro.PromotionalPrice);
            else
                this.Price = Convert.ToInt32(pro.Price);
        }
    }
}