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
    [Table("Sach")]
    public class Product
    { 
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên cuốn sách")]
        [StringLength(100,MinimumLength = 1,ErrorMessage ="Tên sách phải từ {1} đến {0} ký tự")]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }
        [DisplayName("Mô tả của sách")]
        [Required(ErrorMessage ="Vui lòng nhập mô tả")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập ISBN của sách")]
        public string ISBN { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên tác giả")]
        [DisplayName("Tác giả")]
        public string Author { get; set; }

        [Required(ErrorMessage ="Vui lòng lựa chọn thể loại sách")]
        [DisplayName("Thể loại sách")]
        public int CategoryId { get;set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập giá tiền")]
        public decimal Price { get; set; }
        
    }
}
