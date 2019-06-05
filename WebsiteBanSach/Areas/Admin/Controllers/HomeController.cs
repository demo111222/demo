using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        // GET: Admin/Home
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public ActionResult Index()
        {
            DateTime a = DateTime.Now;
            int temp = GetIso8601WeekOfYear(a);

            //khách
            //tuần này
            ViewBag.userThisWeek = data.Users.Where(c => c.UserGroupID == "Member");
            ViewBag.usergroup = data.UserGroups.ToList();

            //tuần trước 
            ViewBag.userLastWeek = data.Users.Where(c => c.UserGroupID == "Member");

            //khách end

            //số đơn hàng tới tuần này cần update
            ViewBag.orderlist_needupdateThisWeek = data.Orders.Where(c => c.CreatedDate == c.ModifiedDate).ToList();
            //tuần trước
            ViewBag.orderlist_needupdateLastWeek = data.Orders.Where(c => c.CreatedDate == c.ModifiedDate ).ToList();


            //doanhthu
            //danh sách đơn hàng đã hoàng thành thuộc tuần hiện tại
            ViewBag.orderThisWeek = data.Orders.Where(c => c.CheckoutStatus == true ).ToList();
            //danh sách đơn hàng đã hoàng thành thuộc tuần hiện tại end
            //danh sách đơn hàng đã hoàng thành thuộc tuần trước
            ViewBag.orderLastWeek = data.Orders.Where(c => c.CheckoutStatus == true ).ToList();
            //danh sách đơn hàng đã hoàng thành thuộc tuần trước end

            ViewBag.Orderdetail = data.OrderDetails.ToList();
            //doanhthu end
            var test = data.Books.Max(c => c.CreatedDate);
            ViewBag.Booklatest = data.Books.Where(c => c.CreatedDate == test).ToList();
            //tuần này
            ViewBag.BookSOThisWeek = data.Books.Where(c => c.Quantity == 0 ).ToList();
            //tuần trước
            ViewBag.BookSOLastWeek = data.Books.Where(c => c.Quantity == 0 ).ToList();
            return View();
        }

        //Đăng xuất
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //Đăng xuất End
    }
}