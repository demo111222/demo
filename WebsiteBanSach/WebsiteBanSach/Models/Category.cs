using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class Category
    {
        //mã thể loại
        [Key]
        [Display(Name ="Mã thể loại")]
        public int ID { get; set; }

        //tên thể loại
        [StringLength(250, ErrorMessage = "Tên loại không được dài hơn 250 ký tự")]
        [Required(ErrorMessage ="Không được để trống tên loại")]
        [Display(Name = "Tên thể loại")]
        public string Name { get; set; }

        //tên thể loại (Name) được mã hóa thành tên không dấu và dấu cách được thay bằng dấu - . vd Tiểu thuyết => Tieu-thuyet
        //phục vụ ch
        //[StringLength(250, ErrorMessage = "Tên loại dạng url không được dài hơn 250 ký tự")]
        [Display(Name = "Tên thể loại dạng url")]
        public string MetaTitle { get; set; }

        //Mô tả
        [StringLength(900, ErrorMessage = "Mô tả không được dài hơn 900 ký tự")]
        [Required(ErrorMessage = "Không được để trống mô tả")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        //tạo ngày
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //tạo bởi
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        //Ngày cập nhật gần nhất
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày cập nhật")]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        //Người cập nhật
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }

        //Category có nhiều Book
        public ICollection<Book> Books { get; set; }
    }
}