using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.ViewModels
{
    public class ViewOrder
    {
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}