namespace assignment3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("tblbook")]
    public partial class tblbook
    {
        [Key]
        public int Bookid { get; set; }

        [StringLength(50)]

        [Required(ErrorMessage ="Bookname is required")]
        public string Bookname { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Publishername is required")]
        public string Publishername { get; set; }

        [StringLength(50)]
        
        [Required(ErrorMessage = "Bookedition is required")]
        public string Bookedition { get; set; }
        //[DisplayName("Upload File")]
        public string ContentType { get; set; }
        public byte[] Image { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Timestamp { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase ImageFile { get; set; } 
    }
}

