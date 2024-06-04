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
    [Table("NhaXuatBan")]
    public class Publisher
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên nhà xuất bản")]
        [DisplayName("Tên nhà xuất bản")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "SĐT không được bỏ trống")]
        [RegularExpression(@"^0(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$",ErrorMessage ="Số điện thoại không hợp lệ")]
        [Display(Name="SĐT")]
        public string PhoneNumber { get; set; }
    }
}
