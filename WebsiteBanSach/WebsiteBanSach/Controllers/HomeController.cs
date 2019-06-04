using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        //tao mot doi tuong chua toan bo csdl
        BookStoreDB db = new BookStoreDB();
        
        //Trang chủ
        public ActionResult Index()
        {
            return View();
        }

        //Trang giới thiệu
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //Trang thông tin liên hệ
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Đăng ký
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Register(User user, HttpPostedFileBase fileUpload,string confirmpassword)
        {
            //Khởi tạo mặc định nhóm người dùng là member
            if(user.UserGroupID==null)
            {
                user.UserGroupID = "Member";
            }
            //Khởi tạo mặc định nếu không chọn avatar thì hệ thống sẽ tự tạo avatar mặc định
            if (fileUpload == null)
            {
                user.Avatar = "user.png";
            }
            //CreateDate ngày tạo tài khoản mặc đinh ngày hiện tại
            if(user.CreatedDate==null)
            {
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate= DateTime.Now;
            }
            //CreateBy tài khoản tạo bởi mặc đinh là Tên tài khoản của người tạo
            if (user.CreatedBy == null && user.UserGroupID == "Member")
            {
                user.CreatedBy = user.UserName;
                user.ModifiedBy = user.UserName;
            }

            //Nếu để trống các ô
            if(user==null)
            {
                ViewBag.Error = "Không được để trống các ô";
                return View();
            }
            if(user.UserName.ToString() == null )
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
            if(st1==null || user.Password != st1)
            {
                ViewBag.Error = "Nhập lại mật khẩu không chính xác";
                return View();
            }
            
            //Địa chỉ
            if(user.Address==null)
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
            }

            //Các thông số của Model User được thỏa mãn tiến hành lưu vào csdl
            if (ModelState.IsValid)
            {
                //Tên đăng nhập không được tạo trùng nhau
                var countUserName = db.Users.Where(u=>u.UserName==user.UserName).Count();
                if(countUserName>=1)
                {
                    ViewBag.Error = "Tên đăng nhập trên đã được sử dụng vui lòng chọn tên đăng nhập khác";
                    return View();
                }

                //Avatar
                if(user.Avatar!= "user.png")
                {
                    //Luu ten file, luu y bo sung thu vien using System.IO; để sử dụng
                    var filename = Path.GetFileName(fileUpload.FileName);
                    //LUU DUONG DAN CUA FILE
                    var path1 = Path.Combine(Server.MapPath("~/Areas/Admin/Content/build/images/"), filename);
                    var path2 = Path.Combine(Server.MapPath("~/img/"), filename);
                    if (System.IO.File.Exists(path1) || System.IO.File.Exists(path2))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //LUU HINH ANH VAO DUONG DAN
                        fileUpload.SaveAs(path1);
                        fileUpload.SaveAs(path2);
                    }
                    user.Avatar = filename;
                }
                //lưu csdl
                db.Users.Add(user);
                db.SaveChanges();
            }
            //load lại và hiện thông báo
            ModelState.Clear();
            ViewBag.Message = user.Name + " đã đăng ký tài khoản thành công";
            return View();
        }
        //Đăng ký END

        //Đăng nhập
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            { 
                var usr = db.Users.Single(u => u.UserName == user.UserName && u.Password == user.Password);
                if(usr!=null)
                {
                    Session["User"] = usr;
                    Session["ID"] = usr.ID.ToString();
                    Session["UserName"] = usr.UserName.ToString();
                    Session["Name"] = usr.Name.ToString();
                    Session["Avatar"] = usr.Avatar;
                    Session["UserGroup"] = usr.UserGroupID;
                    Session["Phone"] = usr.Phone;
                    Session["Email"] = usr.Email;
                    if (Session["UserGroup"].ToString() == "Admin" || Session["UserGroup"].ToString() == "Mod")
                    {
                        return RedirectToAction("Index","Admin/Home");/* D:\DACS\WebsiteBanSach\WebsiteBanSach\Areas\Admin\Views\Home\Index.cshtml*/
                    }
                    else
                    {
                        RedirectToAction("Index","Home/");
                    }
                }
            }
            catch
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View();
            }
            return RedirectToAction("Index");
        }
        //Đăng nhập END

        //Đăng xuất
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index","Home/");
        }
        //Đăng xuất END
    }
}