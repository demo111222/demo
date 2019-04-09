using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class User
    {
        //mã người dùng
        [Key]
        [Display(Name = "Mã người dùng")]
        public int ID { get; set; }

        //Tên người dùng
        [StringLength(50, ErrorMessage = "Tên người dùng không được dài hơn 50 ký tự")]
        [Required(ErrorMessage = "Không được để trống tên người dùng")]
        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }

        //Mật khẩu
        [StringLength(12, ErrorMessage = "Mật khẩu không được dài hơn 12 ký tự")]
        [Required(ErrorMessage = "Không được để trống mật khẩu")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        //nhóm người dùng
        [Display(Name = "Nhóm người dùng")]
        public String UserGroupID { get; set; }

        //Họ tên người dùng
        [StringLength(250, ErrorMessage = "Họ tên không được dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Không được để trống họ tên")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        //Ảnh
        [StringLength(250, ErrorMessage = "Tên ảnh không được dài hơn 250 ký tự")]
        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        //Địa chỉ
        [StringLength(250, ErrorMessage = "Địa chỉ không được dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Không được để trống địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        //Số điện thoại
        [StringLength(250, ErrorMessage = "Số điện thoại không được dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Không được để trống số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        //Email
        [StringLength(250, ErrorMessage = "Địa chỉ mail không được dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Không được để trống địa chỉ mail")]
        [Display(Name = "Địa chỉ mail")]
        public string Email { get; set; }

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

        //Khai báo khóa ngoại
        public virtual UserGroup UserGroup { get; set; }

        //User có nhiều Order
        public ICollection<Order> Orders { get; set; }
    }
}