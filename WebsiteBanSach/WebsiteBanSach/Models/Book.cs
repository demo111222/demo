using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class Book
    {
        //mã sách
        [Key]
        [Display(Name="Mã sách")]
        public int ID { get; set; }

        //tên sách
        [StringLength(250, ErrorMessage = "Tên sách không được dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Không được để trống tên sách")]
        [Display(Name = "Tên sách")]
        public string Name { get; set; }

        //tên thể loại (Name) được mã hóa thành tên không dấu và dấu cách được thay bằng dấu - . vd Tiểu thuyết => Tieu-thuyet
        //phục vụ ch
        //[StringLength(250, ErrorMessage = "Tên loại dạng url không được dài hơn 250 ký tự")]
        [Display(Name = "Tên sách dạng url")]
        public string MetaTitle { get; set; }

        //mô tả
        [StringLength(900, ErrorMessage = "Mô tả không được dài hơn 900 ký tự")]
        [Required(ErrorMessage = "Không được để trống mô tả")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        //ảnh
        [StringLength(250, ErrorMessage = "Tên ảnh không được dài hơn 250 ký tự")]
        //[Required(ErrorMessage = "Không được để trống ảnh")]
        [Display(Name = "Ảnh sách")]
        public string Image { get; set; }

        //Gía
        [Required(ErrorMessage = "Không được để trống giá")]
        [Display(Name = "Giá sách")]
        public decimal Price { get; set; }

        //Gía khuyến mãi
        [Display(Name = "Giá sách đã khuyến mãi")]
        public decimal PromotionPrice { get; set; }

        //Số lượng tồn kho
        [Required(ErrorMessage = "Không được để trống số lượng tồn")]
        [Display(Name = "Số lượng tồn")]
        public decimal Quantity { get; set; }

        //Thể loại
        [Display(Name = "Thể loại")]
        public int CategoryID { get; set; }

        //Tác giả
        [Display(Name = "Tác giả")]
        public int AuthorID { get; set; }

        //Nhà xuất bản
        [Display(Name = "Nhà xuất bản")]
        public int PublisherID { get; set; }

        //Chi tiết
        [StringLength(900, ErrorMessage = "Chi tiết không được dài hơn 900 ký tự")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }

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

        //Trang thái
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        //Lượt xem
        [Display(Name = "Lượt xem")]
        public int ViewCount { get; set; }

        //khai báo khóa ngoại
        public virtual Category Category { get; set; }
        public virtual Author Author { get; set; }
        public virtual Publisher Publisher { get; set; }

        //Book có trong nhiều Order và ngược lại
        public ICollection<OrderDetail> OrderDetails { get; set; }

        //phương thức khởi tạo
        public Book()
        {
            this.Price = 0;
            this.PromotionPrice = 0;
            this.ViewCount = 0;
            this.Quantity = 0;
            
        }
    }
}