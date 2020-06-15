using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebsiteBanSach.Models;
using System.Net;
using PagedList;

namespace quanlisach.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        BookStoreDB db = new BookStoreDB();

        //danh muc san pham
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var lstBook = db.Books.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize);
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Publishers = db.Publishers.ToList();
            return View(lstBook);
        }
        //danh muc san pham end

        public ActionResult SanPham()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SanPhamPartial()
        {
            return PartialView();
        }

        public ActionResult CTSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.SingleOrDefault(n => n.ID == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        public ActionResult SanPham(int? IdCategories,int? page)
        {
            if (IdCategories == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstBook = db.Books.Where(n => n.ID == IdCategories);
            if (lstBook.Count() == 0)
            {
                return HttpNotFound();
            }
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(lstBook.OrderBy(n=>n.ID).ToPagedList(pageNumber,pageSize));
        }

        //phan phia dưới sau khi tìm kiếm
        public ActionResult LaySPCungLoai(int maloai, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var lstBook = db.Books.Where(n => n.CategoryID == maloai).ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize);
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Publishers = db.Publishers.ToList();
            return View(lstBook);
        }
        //phan phia dưới sau khi tìm kiếm end

        //lay danh sách sản phẩm cùng loại với id của loại bất kỳ
        public ActionResult BookFromOneCate(int id,int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var lstBook = db.Books.Where(a=>a.CategoryID==id).ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize);
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Publishers = db.Publishers.ToList();
            return View(lstBook);
        }
        //lay danh sách sản phẩm cùng loại với id của tác giả bất kỳ end

        //lay danh sách sản phẩm cùng tác giả với id của loại bất kỳ
        public ActionResult BookFromOneAu(int id, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var lstBook = db.Books.Where(a => a.AuthorID == id).ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize);
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Publishers = db.Publishers.ToList();
            return View(lstBook);
        }
        //lay danh sách sản phẩm cùng tác giả với id của tác giả bất kỳ end

        //lay danh sách sản phẩm cùng tác giả với id của loại bất kỳ
        public ActionResult BookFromOnePub(int id, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var lstBook = db.Books.Where(a => a.PublisherID == id).ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize);
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Publishers = db.Publishers.ToList();
            return View(lstBook);
        }
        //lay danh sách sản phẩm cùng tác giả với id của tác giả bất kỳ end
    }
}