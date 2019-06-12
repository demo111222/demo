using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using PagedList;


namespace WebsiteBanSach.Controllers
{
    
    public class TimKiemController : Controller
    {
        BookStoreDB db = new BookStoreDB();
        // GET: TimKiem
        
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            //menu left
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Publishers = db.Publishers.ToList();
            //menu left end

            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            List<Book> sanphammoi = new List<Book>();
            if (db.Books.ToList().Count >= 0)
            {
                sanphammoi = db.Books.OrderByDescending(n => n.ModifiedDate).Take(10).ToList();
            }
            else
            {
                sanphammoi = db.Books.OrderByDescending(n => n.ModifiedDate).ToList();
            }

            ViewBag.SanPhamMoi = sanphammoi;
            int ma;
            int gia;
            bool kiemtra = int.TryParse(sTuKhoa, out ma);
            bool kiemtra1 = int.TryParse(sTuKhoa, out gia);
            List<Book> lstSach = new List<Book>();
            if (kiemtra == false)
            {
                lstSach = db.Books.Where(n => n.Name.Contains(sTuKhoa) || n.Author.Name.Contains(sTuKhoa)).ToList();
            }
            else if(kiemtra1 == true)
            {
                lstSach = db.Books.Where(n => n.Name.Contains(sTuKhoa) || n.Author.Name.Contains(sTuKhoa) || n.Price == gia).ToList();
            }
            else 
                lstSach = db.Books.Where(n => n.Name.Contains(sTuKhoa) || n.Author.Name.Contains(sTuKhoa) || n.ID == ma ).ToList();

            return View(lstSach.OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize));
        }
    }
}