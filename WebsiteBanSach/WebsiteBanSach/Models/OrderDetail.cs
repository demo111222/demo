using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class OrderDetail
    {
        //Chi tiết Đơn đạt hàng

        //mã Đơn đặt hàng
        [Key]
        [Column(Order = 1)]
        [Display(Name = "Mã đơn đặt hàng")]
        public int OrderID { get; set; }

        //mã quyền
        [Key]
        [Column(Order = 2)]
        [Display(Name = "Mã sách")]
        public int BookID { get; set; }

        //số lượng
        [Required(ErrorMessage ="Không được để trống số lượng")]
        [Display(Name = "Số lượng")]
        public int Number { get; set; }

        //Gía
        [Required(ErrorMessage = "Không được để trống giá")]
        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        //Khai báo khóa ngoại
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
    }
}