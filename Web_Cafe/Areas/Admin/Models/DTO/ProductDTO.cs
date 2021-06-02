using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cafe.Areas.Admin.Models.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProName { get; set; }
        public string Highlight { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public double? PromotionalPrice { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string ProStatus { get; set; }
        public int? IdCate { get; set; }
        public string CateName { get; set; }
        public string ImageLink { get; set; }
        public int Count { get; set; }
    }
}