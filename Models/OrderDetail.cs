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
    [Table("ChiTietDonHang")]
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; } 
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        [ValidateNever]
        public Order Order { get; set; }    

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        [ValidateNever]
        public Product Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}
