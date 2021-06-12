namespace Web_Cafe.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class News
    {
        public int NewsID { get; set; }

        [StringLength(100)]
        public string NewsTitle { get; set; }

        [StringLength(200)]
        public string ImageLink { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string NewsDescription { get; set; }

        public DateTime? DayTime { get; set; }

        public int? AdminID { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
