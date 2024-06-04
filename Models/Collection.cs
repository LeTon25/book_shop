using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    [Table("BoSuuTap")]
    public class Collection
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập của bộ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Tên bộ phải từ {1} đến {0} ký tự")]
        [DisplayName("Tên bộ sách")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Vui lọng chọn ảnh cho bộ sách")]
        [DisplayName("Hình ảnh của bộ")]
        [ValidateNever]
        public string ImageUrl { get; set; }
        [ValidateNever]
        [DisplayName("Thể loại")]
        public ICollection<Product> Products { get; set; }
    }
}
