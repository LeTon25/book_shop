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

        [Required(ErrorMessage ="Vui lòng nhập tên tác giả")]
        [DisplayName("Tác giả")]
        public string Author { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập giá tiền")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá tiền không được âm")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập số lượng")]
        [DisplayName("Số lượng")]
        [Range(0,int.MaxValue,ErrorMessage ="Số lượng phải từ {0} trở lên")]
        public int Stock { get; set; }
        [Required]
        [DisplayName("Nhà xuất bản")]
        public int PublisherID { get;set; }
        [DisplayName("Bộ sách")]
        public int? CollectionID { get; set; }
        [ForeignKey("PublisherID")]
        [ValidateNever]
        public Publisher? Publisher { get; set; }   
        
        [ForeignKey("CollectionID")]
        [ValidateNever]
        public Collection? Collection { get; set; }  
        
        [ValidateNever]
        [DisplayName("Hình ảnh")]
        public List<ProductImage>   ProductImages { get; set; }

        [ValidateNever]
        [DisplayName("Thể loại")]        
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
