using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class Order
    {
        //mã đơn đặt hàng
        [Key]
        [Display(Name ="Mã đơn đặt hàng")]
        public int ID { get; set; }
        
        //tạo ngày
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //ngày giao
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày giao")]
        public DateTime DeliveryDate { get; set; }

        //tạo bởi
        [Display(Name = "Người tạo")]
        public int UserID { get; set; }

        //Ngày cập nhật gần nhất
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày cập nhật")]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        //Người cập nhật
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }

        //Trạng thái giao hàng
        [Display(Name = "Trạng thái giao hàng")]
        public bool? DeliveryStatus { get; set; }

        //Trạng thái thanh toán
        [Display(Name = "Trạng thái thanh toán")]
        public bool? CheckoutStatus { get; set; }

        //Khai báo khóa ngoại
        public virtual User User { get; set; }

        //Order có nhiều Book và ngược lại
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}