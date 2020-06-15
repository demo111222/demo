using Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        BookStoreDB db = new BookStoreDB();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            try
            {
                string sTaikhoan = f["txtTaikhoan"].ToString();
                string sMatkhau = f["txtMatkhau"].ToString();
                var usr = db.Users.SingleOrDefault(n => n.UserName == sTaikhoan && n.Password == sMatkhau);
                if (usr != null)
                {
                    Session["User"] = usr;
                    Session["ID"] = usr.ID.ToString();
                    Session["UserName"] = usr.UserName.ToString();
                    Session["Name"] = usr.Name.ToString();
                    Session["Avatar"] = usr.Avatar;
                    Session["UserGroup"] = usr.UserGroupID;
                    Session["Phone"] = usr.Phone;
                    Session["Email"] = usr.Email;
                    Session["Taikhoan"] = usr;
                    if (Session["UserGroup"].ToString() == "Admin" || Session["UserGroup"].ToString() == "Mod")
                    {
                        return RedirectToAction("Index", "Admin/Home");/* D:\DACS\WebsiteBanSach\WebsiteBanSach\Areas\Admin\Views\Home\Index.cshtml*/
                    }
                    else
                    {
                        RedirectToAction("Index", "Home/");
                    }
                }
            }
            catch
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View();
            }
            return RedirectToAction("Index");
            //string sTaikhoan = f["txtTaikhoan"].ToString();
            //string sMatkhau = f["txtMatkhau"].ToString();

            //User user = db.Users.SingleOrDefault(n => n.UserName == sTaikhoan && n.Password == sMatkhau);
            //if (user != null)
            //{
            //    Session["Taikhoan"] = user;
            //    return RedirectToAction("Index");
            //    //return Content("<script>window.location.reload();</script>");
            //}

            //return Content("Tài khoản hoặc mật khẩu không đúng !");
        }


        public ActionResult DangXuat()
        {
            Session.Clear();
            //Session["Taikhoan"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult DangNhap()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Dangky1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangky1(User user)
        {
            return View();
        }

        //tao mat khau random
        private int RandomNumber(int min, int max) //random số
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        /// <summary>
        /// Tạo ra một chuỗi ngẫu nhiên với độ dài cho trước
        /// </summary>
        /// <param name="size">Kích thước của chuỗi </param>
        /// <param name="lowerCase">Nếu đúng, tạo ra chuỗi chữ thường</param>
        /// <returns>Random string</returns>
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            builder.Append(RandomString(1, true));
            return builder.ToString();
        }
        //tao mat khau random end

        public ActionResult QuenMatKhau()
        {
            return View();
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult QuenMatKhau(string email)
        {
            try
            {
                //kiểm tra Email

                if (email.Length > 250)
                {
                    ViewBag.Error = "Email không được dài hơn 250 ký tự";
                    return View();
                }
                if (email == null)
                {
                    ViewBag.Error = "Email không được để trống";
                    return View();
                }
                else
                {
                    char[] array = email.ToCharArray();
                    if (email.Contains("@") == false || email.Contains("@.") == true || email.Contains(".@") == true || email.Contains(".com") == false || array[0].ToString() == "@")
                    {
                        ViewBag.Error = "Email không đúng định dạng. định dạng đúng vd: minh@gmail.com";
                        return View();
                    }
                }
                //kiểm tra Email END
                var userss = db.Users.Single(c => c.Email == email);
                if(userss!=null)
                {
                    var oldpass = userss.Password;
                    string tentaikhoan = userss.UserName;
                    if (ModelState.IsValid)
                    {
                        string pass = GetPassword();
                        userss.Password = pass;
                        if (userss.ModifiedBy != null)
                        {
                            userss.ModifiedBy = "Hệ thống";
                        }
                        if (userss.ModifiedDate == null || userss.ModifiedDate != null)
                        {
                            userss.ModifiedDate = DateTime.Now;
                        }
                        UpdateModel(userss);
                        db.SaveChanges();
                        if (oldpass != userss.Password)
                        {
                            //gửi mail xác nhận đặt hàng
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/changepasssuccess.html"));//mail template

                            //thay đổi nội dung mail
                            content = content.Replace("{{tentaikhoan}}", userss.UserName);
                            content = content.Replace("{{newpass}}", pass);

                            var mailCustomer = userss.Email.ToString(); //mail của khách

                            new MailHelper().SendMail(mailCustomer, "Mật khẩu được khôi phục thành công", content); // gửi mail cho khách
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Không có tài khoản với email trên";
                    return View();
                }
                ViewBag.Message = "Đã gửi yêu cầu khôi phục mật khẩu thành công";
                return View();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        //[HttpGet]
        //public ActionResult DangKy()
        //{
        //    return PartialView();
        //}

        //[HttpPost]
        //public ActionResult DangKy(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ViewBag.ThongBao = "Thêm Thành Công";
        //        db.Users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ViewBag.ThongBao = "Thêm Thất bại";
        //    }

        //    return View();
        //}

        public ActionResult GioiThieu()
        {
            ViewBag.listTheLoai = db.Categories.ToList().OrderBy(c => c.Name);
            return View();
        }

        public ActionResult LienHe()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        ////tao mot doi tuong chua toan bo csdl
        //BookStoreDB db = new BookStoreDB();

        ////Trang chủ
        //public ActionResult Index()
        //{
        //    return View();
        //}

        ////Trang giới thiệu
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        ////Trang thông tin liên hệ
        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        ////Đăng ký
        public ActionResult DangKy()
        {
            return PartialView();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DangKy(User user, HttpPostedFileBase fileUpload, string confirmpassword)
        {
            //Khởi tạo mặc định nhóm người dùng là member
            if (user.UserGroupID == null)
            {
                user.UserGroupID = "Member";
            }
            //Khởi tạo mặc định nếu không chọn avatar thì hệ thống sẽ tự tạo avatar mặc định
            if (fileUpload == null)
            {
                user.Avatar = "user.png";
            }
            //CreateDate ngày tạo tài khoản mặc đinh ngày hiện tại
            if (user.CreatedDate == null)
            {
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
            }
            //CreateBy tài khoản tạo bởi mặc đinh là Tên tài khoản của người tạo
            if (user.CreatedBy == null && user.UserGroupID == "Member")
            {
                user.CreatedBy = user.UserName;
                user.ModifiedBy = user.UserName;
            }

            //Nếu để trống các ô
            if (user == null)
            {
                ViewBag.Error = "Không được để trống các ô";
                return View();
            }
            if (user.UserName.ToString() == null)
            {
                ViewBag.Error = "Tên đăng nhập không được để trống";
                return View();
            }
            if (user.Password == null)
            {
                ViewBag.Error = "Mật khẩu không được để trống";
                return View();
            }

            //kiểm tra xác nhận mật khẩu
            string st1 = confirmpassword;
            if (st1 == null || user.Password != st1)
            {
                ViewBag.Error = "Nhập lại mật khẩu không chính xác";
                return View();
            }

            //Địa chỉ
            if (user.Address == null)
            {
                ViewBag.Error = "Địa chỉ dùng để giao hàng xin vui lòng không để trống";
                return View();
            }

            //Số điện thoại
            if (user.Phone == null)
            {
                ViewBag.Error = "Địa chỉ dùng để liên lạc xin vui lòng không để trống";
                return View();
            }

            //Email
            if (user.Email == null)
            {
                ViewBag.Error = "Email dùng để liên lạc xin vui lòng không để trống";
                return View();
            }
            else
            {
                char[] array = user.Email.ToCharArray();
                if (user.Email.Contains("@") == false || user.Email.Contains("@.") == true || user.Email.Contains(".@") == true || user.Email.Contains(".com") == false || array[0].ToString() == "@")
                {
                    ViewBag.Error = "Email không đúng định dạng. định dạng đúng vd: minh@gmail.com";
                    return View();
                }
                var emailList = db.Users.ToList();
                foreach(var u in emailList)
                {
                    if(user.Email==u.Email)
                    {
                        ViewBag.Error = "Email đã được dùng hãy chọn email khác";
                    }
                }
                
            }

            //Các thông số của Model User được thỏa mãn tiến hành lưu vào csdl
            if (ModelState.IsValid)
            {
                //Tên đăng nhập không được tạo trùng nhau
                var countUserName = db.Users.Where(u => u.UserName == user.UserName).Count();
                if (countUserName >= 1)
                {
                    ViewBag.Error = "Tên đăng nhập trên đã được sử dụng vui lòng chọn tên đăng nhập khác";
                    return View();
                }

                //Avatar
                if (user.Avatar != "user.png")
                {
                    //Luu ten file, luu y bo sung thu vien using System.IO; để sử dụng
                    var filename = Path.GetFileName(fileUpload.FileName);
                    //LUU DUONG DAN CUA FILE
                    var path1 = Path.Combine(Server.MapPath("~/Areas/Admin/Content/build/images/"), filename);
                    var path2 = Path.Combine(Server.MapPath("~/img/"), filename);
                    var path3 = Path.Combine(Server.MapPath("~/Content/HinhAnh/"), filename);
                    if (System.IO.File.Exists(path1) || System.IO.File.Exists(path2) || System.IO.File.Exists(path3))
                    {
                        ViewBag.Error = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {
                        //LUU HINH ANH VAO DUONG DAN
                        fileUpload.SaveAs(path1);
                        fileUpload.SaveAs(path2);
                        fileUpload.SaveAs(path3);
                    }
                    user.Avatar = filename;
                }
                //lưu csdl
                db.Users.Add(user);
                db.SaveChanges();
            }
            else
            {
                ViewBag.ThongBao = "Thêm Thất bại";
            }
            //load lại và hiện thông báo
            ModelState.Clear();
            ViewBag.Message = user.Name + " đã đăng ký tài khoản thành công";
            return PartialView();
        }
        //Đăng ký END

        ////Đăng nhập
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(User user)
        //{
        //    try
        //    { 
        //        var usr = db.Users.Single(u => u.UserName == user.UserName && u.Password == user.Password);
        //        if(usr!=null)
        //        {
        //            Session["User"] = usr;
        //            Session["ID"] = usr.ID.ToString();
        //            Session["UserName"] = usr.UserName.ToString();
        //            Session["Name"] = usr.Name.ToString();
        //            Session["Avatar"] = usr.Avatar;
        //            Session["UserGroup"] = usr.UserGroupID;
        //            Session["Phone"] = usr.Phone;
        //            Session["Email"] = usr.Email;
        //            if (Session["UserGroup"].ToString() == "Admin" || Session["UserGroup"].ToString() == "Mod")
        //            {
        //                return RedirectToAction("Index","Admin/Home");/* D:\DACS\WebsiteBanSach\WebsiteBanSach\Areas\Admin\Views\Home\Index.cshtml*/
        //            }
        //            else
        //            {
        //                RedirectToAction("Index","Home/");
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không chính xác";
        //        return View();
        //    }
        //    return RedirectToAction("Index");
        //}
        ////Đăng nhập END

        ////Đăng xuất
        //public ActionResult LogOut()
        //{
        //    Session.Clear();
        //    return RedirectToAction("Index","Home/");
        //}
        ////Đăng xuất END
        ///

        public class SitemapNode
        {
            public SitemapFrequency? Frequency { get; set; }
            public DateTime? LastModified { get; set; }
            public double? Priority { get; set; }
            public string Url { get; set; }
        }

        public enum SitemapFrequency
        {
            Never,
            Yearly,
            Monthly,
            Weekly,
            Daily,
            Hourly,
            Always
        }
        public IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            var a = db.Books.ToList();
            List<SitemapNode> nodes = new List<SitemapNode>();
            nodes.Add(
                new SitemapNode()
                {
                    Url = Url.Action("Index", "Home", null, Request.Url.Scheme),
                    Priority = 1
                });
            nodes.Add(
               new SitemapNode()
               {
                   Url = Url.Action("GioiThieu", "Home", null, Request.Url.Scheme),
                   Priority = 0.9
               });
            nodes.Add(
               new SitemapNode()
               {
                   Url = Url.Action("LienHe", "Home", null, Request.Url.Scheme),
                   Priority = 0.9
               });
            foreach (var product in a)
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = Url.Action("CTSP", "SanPham", new { id = product.ID }, Request.Url.Scheme),
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
            }
            return nodes;
        }
        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");
            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                xmlns + "url",
                new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                sitemapNode.LastModified == null ? null : new XElement(
                xmlns + "lastmod",
                sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                sitemapNode.Frequency == null ? null : new XElement(
                xmlns + "changefreq",
                sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                sitemapNode.Priority == null ? null : new XElement(
                xmlns + "priority",
                sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }
            XDocument document = new XDocument(root);
            return document.ToString();
        }

        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            var sitemapNodes = GetSitemapNodes();
            string xml = GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }
    }
}