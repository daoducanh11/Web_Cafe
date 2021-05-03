namespace Web_Cafe.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public int ImageID { get; set; }

        [StringLength(200)]
        public string ImageLink { get; set; }

        public int? ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
