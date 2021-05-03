namespace Web_Cafe.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhotoLibrary")]
    public partial class PhotoLibrary
    {
        [Key]
        public int LibraryID { get; set; }

        [StringLength(200)]
        public string ImageLink { get; set; }

        [StringLength(50)]
        public string LibraryType { get; set; }

        [StringLength(50)]
        public string LibraryStatus { get; set; }

        public int AdminID { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
