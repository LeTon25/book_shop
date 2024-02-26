using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("HinhAnhSP")]
    public class ProductImage
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name ="Hình ảnh")]
        public string ImageUrl { get; set; }

        public int ProductID { get; set; }
        [ValidateNever]
        [ForeignKey("ProductID")]
        public Product Product { get; set; }    
    }
}
