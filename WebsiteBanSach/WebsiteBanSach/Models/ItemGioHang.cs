using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class ItemGioHang
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public decimal ToTal { get; set; }
        public string Image { get; set; }
        public ItemGioHang(int iMaSach)
        {
            using (BookStoreDB db = new BookStoreDB())
            {
                this.ID = iMaSach;
                Book sach = db.Books.Single(n => n.ID == iMaSach);
                this.Name = sach.Name;
                this.Image = sach.Image;
                this.Price = sach.Price;
                this.Number = 1;
                this.Quantity = 1;
                this.PromotionPrice = sach.PromotionPrice;
                this.ToTal = PromotionPrice * Number;

            }
        }

        public ItemGioHang(int iMaSach, int sl)
        {
            using (BookStoreDB db = new BookStoreDB())
            {
                this.ID = iMaSach;
                Book sach = db.Books.Single(n => n.ID == iMaSach);
                this.Name = sach.Name;
                this.Image = sach.Image;
                this.Price = sach.Price;
                this.Number = sl;
                this.Quantity = sl;
                this.PromotionPrice = sach.PromotionPrice;
                this.ToTal = PromotionPrice * Number;
            }
        }

        public ItemGioHang()
        {

        }
    }
}