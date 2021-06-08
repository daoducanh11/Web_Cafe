namespace Web_Cafe.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Items = new HashSet<Item>();
        }

        public int OrderID { get; set; }

        public double? Amount { get; set; }

        public DateTime? Daytime { get; set; }

        [StringLength(200)]
        public string DeliveryAddress { get; set; }

        [StringLength(200)]
        public string OrderDescription { get; set; }

        [StringLength(50)]
        public string OrderStatus { get; set; }

        public int? UserID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }

        public virtual User User { get; set; }
    }
}
