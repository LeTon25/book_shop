using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("CongTy")]
    public class Company
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("Tên công ty")]
        public string Name { get; set; }
        [DisplayName("Tên đường")]
        public string? StreetAddress { get; set; }
        [DisplayName("Thành phố")]  
        public string? City { get; set; }
        [DisplayName("Huyện")]   
        public string? State { get; set; }
        [DisplayName("Mã bưu điện")] 
        public string? PostalCode { get; set;}
        [DisplayName("SĐT")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
    }
}
