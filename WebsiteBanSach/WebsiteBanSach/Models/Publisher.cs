using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class Publisher
    {
        //Mã nhà xuất bản
        [Key]
        [Display(Name = "Mã nhà xuất bản")]
        public int ID { get; set; }

        //Tên nhà xuất bản
        [StringLength(250, ErrorMessage = "Tên nhà xuất bản không được dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Không được để trống tên nhà xuất bản")]
        [Display(Name = "Tên nhà xuất bản")]
        public string Name { get; set; }

        //tên thể loại (Name) được mã hóa thành tên không dấu và dấu cách được thay bằng dấu - . vd Tiểu thuyết => Tieu-thuyet
        //phục vụ cho tiềm kiếm sau này
        //[StringLength(250, ErrorMessage = "Tên loại dạng url không được dài hơn 250 ký tự")]
        [Display(Name = "Tên nhà xuất bản dạng url")]
        public string MetaTitle { get; set; }

        //Logo
        [StringLength(250, ErrorMessage = "Tên ảnh logo không được dài hơn 250 ký tự")]
        [Display(Name = "Ảnh Logo")]
        public string Logo { get; set; }

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

        //Publisher có nhiều Book
        public ICollection<Book> Books { get; set; }
    }
}