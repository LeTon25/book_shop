using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("TheLoai")]
    public class Category
    {
        [Key]
        [DisplayName("Mã thể loại")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thể loại")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Tên thể loại phải từ {1} đến {0} ký tự")]
        [DisplayName("Tên thể loại")]
        public string Name { get; set; }

        [ValidateNever]
        [DisplayName("Thể loại")]
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
