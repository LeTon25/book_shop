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
    [Table("TheLoaiSach")]
    public class ProductCategory
    {
        [Key]
        public int ID { get; set; }
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        [ValidateNever]
        public Product Product { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
