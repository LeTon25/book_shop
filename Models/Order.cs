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
    [Table("DonHang")]
    public class Order
    {
        public int ID { get; set; }
        [ValidateNever] 
        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationUserID")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }    
        public DateTime OrderDate { get; set; } 
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber {  get; set; }    
        public string? Carrier { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập SĐT")]  
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên đường")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên thành phố")]
        public string City { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập huyện")]
        public string State { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mã bưu điện")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        public string Name { get; set; }
        
    }
}
