using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class ReportAndStatiticController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        // GET: Admin/ReportAndStatitic
        public ActionResult Profit()
        {
            //ngày hiện tại
            DateTime current = DateTime.Now;
            var year = current.Year.ToString();
            var t1 = 1;
            var t2 = 2;
            var t3 = 3;
            var t4 = 4;
            var t5 = 6;
            var t6 = 7;
            var t7 = 8;
            var t8 = 8;
            var t9 = 9;
            var t10 = 10;
            var t11 = 11;
            var t12 = 12;
            //Danh sách tháng
            //tháng 1
            ViewBag.Order1 = data.Orders.Where(n=>n.CreatedDate.Year== current.Year && n.CreatedDate.Month==t1).ToList();
            //tháng 2
            ViewBag.Order2 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t2).ToList();
            //tháng 3
            ViewBag.Order3 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t3).ToList();
            //tháng 4
            ViewBag.Order4 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t4).ToList();
            //tháng 5
            ViewBag.Order5 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t5).ToList();
            //tháng 6
            ViewBag.Order6 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t6).ToList();
            //tháng 7
            ViewBag.Order7 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t7).ToList();
            //tháng 8
            ViewBag.Order8 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t8).ToList();
            //tháng 9
            ViewBag.Order9 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t9).ToList();
            //tháng 10
            ViewBag.Order10 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t10).ToList();
            //tháng 11
            ViewBag.Order11 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t11).ToList();
            //tháng 12
            ViewBag.Order12 = data.Orders.Where(n => n.CreatedDate.Year == current.Year && n.CreatedDate.Month == t12).ToList();
            //chi tiết đơn đặt hàng
            ViewBag.Orderdetail = data.OrderDetails.ToList();
            return View();
        }

        // 
        public ActionResult Product()
        {
            //thể loại
            ViewBag.Category = data.Categories.ToList();
            //Sach
            ViewBag.Book = data.Books.ToList();

            //ngày hiện tại
            DateTime current = DateTime.Now;
            
            //dơn hàng
            ViewBag.Order = data.Orders.Where(n => n.CreatedDate.Year == current.Year).ToList();
            //Chi tiết đơn hàng
            ViewBag.OrderDetail = data.OrderDetails.ToList();

            //tác giả
            ViewBag.Author = data.Authors.ToList();
            //Nhà xuất bản
            ViewBag.Publisher = data.Publishers.ToList();
            return View();
        }

        // GET: Admin/ReportAndStatitic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ReportAndStatitic/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/ReportAndStatitic/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/ReportAndStatitic/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/ReportAndStatitic/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/ReportAndStatitic/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
