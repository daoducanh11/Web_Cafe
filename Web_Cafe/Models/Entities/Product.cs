namespace Web_Cafe.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Images = new HashSet<Image>();
            Items = new HashSet<Item>();
        }

        public int ProductID { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage = "B?n ch?a nh?p tên s?n ph?m!")]
        [Required]
        public string ProName { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Highlight { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string ProDescription { get; set; }

        [Range(0, 10000000)]
        public double? Price { get; set; }

        [Range(0, 10000000)]
        public double? PromotionalPrice { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [StringLength(50)]
        public string ProStatus { get; set; }

        public int? CategoryID { get; set; }

        public int? AdminID { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }
    }
}
