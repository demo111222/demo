using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class HoSoController : Controller
    {
        // GET: SanPham
        BookStoreDB db = new BookStoreDB();
        // GET: HoSo
        public ActionResult HoSo(int id)
        {
            //xác nhận quyền
            var t = "EDIT_USER_SELF";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = db.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            if(t1!="Member")//không là khách hàng
            {
                return RedirectToAction("Index", "Admin/Home");
            }
            if (ViewBag.Permission == null)//nếu ko có quyền trên sẽ bị đá ra trang lỗi
            {
                return RedirectToAction("Er403", "Admin/loi/");
            }
            //xác nhận quyền END
            var hoso = db.Users.Single(c => c.ID == id);
            ViewBag.Order = db.Orders.Where(c => c.UserID == id && c.CheckoutStatus==null).ToList();
            ViewBag.Order_true = db.Orders.Where(c => c.UserID == id && c.CheckoutStatus == true).ToList();
            ViewBag.Order_false = db.Orders.Where(c => c.UserID == id && c.CheckoutStatus == false).ToList();
            ViewBag.OrderDetail = db.OrderDetails.ToList();

            ViewBag.Authors = db.Authors.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Publishers = db.Publishers.ToList();
            if (hoso==null)
            {
                return RedirectToAction("Er404", "Admin/loi/");
            }
            return View(hoso);
        }

        [ChildActionOnly]
        public ActionResult SuaHoSo(int id)
        {
            var a = db.Users.Single(c => c.ID == id);
            if (a == null)
            {
                return RedirectToAction("Er404", "Admin/loi/");
            }
            return PartialView("SuaHoSo", a);
        }

        [ChildActionOnly]
        public ActionResult SuaMatKhau(int id)
        {
            var a = db.Users.Single(c => c.ID == id);
            if (a == null)
            {
                return RedirectToAction("Er404", "Admin/loi/");
            }
            return PartialView("SuaMatKhau", a);
        }

        //Sửa hồ sơ
        public ActionResult EditProfile(int id)
        {
            ViewBag.List = new SelectList(db.UserGroups.Where(c => c.ID != "Admin" && c.ID != "Member").ToList().OrderBy(n => n.Name), "ID", "Name");
            //biến dùng trong việc gửi mail
            var t0 = Session["UserName"].ToString();

            var u = db.Users.Single(n => n.ID == id);

            if (u.UserName == t0)
            {
                //xác nhận quyền sửa nếu tài khoản được sửa là bản thân
                var t = "EDIT_USER_SELF";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = db.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            }
            else
            {
                //xác nhận quyền tài khoản được sửa là của người khác
                var t = "EDIT_USER";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = db.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            }

            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditProfile(int id, User u, HttpPostedFileBase fileUpload)
        {
            try
            {
                u = db.Users.Single(n => n.ID == id);
                if (u == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }

                var t = db.Users.Single(c => c.ID == id);
                //Kiểm tra học tên
                if (u.Name == null)
                {
                    ViewBag.Error = "Họ tên bạn không để trống";
                    return View();
                }
                if (u.Name.Length > 250)
                {
                    ViewBag.Error = "Họ tên không được dài hơn 250 ký tự";
                    return View();
                }
                //Kiểm tra học tên end

                //Kiểm mật khẩu
                if (u.Password != null || u.Password == null)
                {
                    u.Password = t.Password;
                }
                //Kiểm mật khẩu END

                //Kiểm tên đăng nhập
                if (u.UserName != null || u.UserName == null)
                {
                    u.UserName = t.UserName;
                }
                //Kiểm tên đăng nhập END

                //kiểm tra phone
                if (u.Phone == null)
                {
                    ViewBag.Error = "Số điện thoại không để trống";
                    return View();
                }
                if (u.Phone.Length > 250)
                {
                    ViewBag.Error = "Số điện thoại không được dài hơn 250 số";
                    return View();
                }
                //kiểm tra phone END

                //kiểm tra địa chỉ
                if (u.Address == null)
                {
                    ViewBag.Error = "Địa chỉ không được để trống";
                    return View();
                }
                if (u.Address.Length > 250)
                {
                    ViewBag.Error = "Địa chỉ không được dài hơn 250 ký tự";
                    return View();
                }
                //kiểm tra địa chỉ END

                //kiểm tra Email

                if (u.Email.Length > 250)
                {
                    ViewBag.Error = "Email không được dài hơn 250 ký tự";
                    return View();
                }
                if (u.Email == null)
                {
                    ViewBag.Error = "Email không được để trống";
                    return View();
                }
                else
                {
                    char[] array = u.Email.ToCharArray();
                    if (u.Email.Contains("@") == false || u.Email.Contains("@.") == true || u.Email.Contains(".@") == true || u.Email.Contains(".com") == false || array[0].ToString() == "@")
                    {
                        ViewBag.Error = "Email không đúng định dạng. định dạng đúng vd: minh@gmail.com";
                        return View();
                    }
                    var emailList = db.Users.ToList();
                    foreach (var ut in emailList)
                    {
                        if (u.Email == ut.Email)
                        {
                            ViewBag.Error = "Email đã được dùng hãy chọn email khác";
                        }
                    }
                }
                //kiểm tra Email END

                //Kiểm tra ảnh Avatar
                if (fileUpload != null)
                {
                    var filename_logo = Path.GetFileName(fileUpload.FileName);

                    //Name logo length
                    if (filename_logo.ToString().Length > 250)
                    {
                        ViewBag.Error = "Tên ảnh không được vượt quá 250 ký tự";
                        return View();
                    }
                    else
                    {
                        if (ModelState.IsValid)
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
                            u.Avatar = filename;
                            if (u.ModifiedBy != null)
                            {
                                u.ModifiedBy = Session["UserName"].ToString();
                            }
                            if (u.ModifiedDate == null || u.ModifiedDate != null)
                            {
                                u.ModifiedDate = DateTime.Now;
                            }
                            UpdateModel(u);
                            db.SaveChanges();
                        }

                        //gửi mail vào sau khi thay đổi thành công dữ liệu
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/newprofile.html"));

                        content = content.Replace("{{UserName}}", u.UserName);
                        content = content.Replace("{{Name}}", u.Name);
                        content = content.Replace("{{ModifiedBy}}", Session["UserName"].ToString());

                        var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                        new MailHelper().SendMail(u.Email, "Thay đổi hồ sơ tài khoản thành công", content);
                        new MailHelper().SendMail(toEmail, "Thay đổi hồ sơ tài khoản thành công", content);

                        //gửi mail vào sau khi thay đổi thành công dữ liệu end

                        return RedirectToAction("HoSo", new { id = u.ID });
                    }
                }
                else
                {
                    var s = db.Users.Single(c => c.ID == id);
                    u.Avatar = s.Avatar;
                    if (ModelState.IsValid)
                    {
                        if (u.ModifiedBy != null)
                        {
                            u.ModifiedBy = Session["UserName"].ToString();
                        }
                        if (u.ModifiedDate == null || u.ModifiedDate != null)
                        {
                            u.ModifiedDate = DateTime.Now;
                        }
                        UpdateModel(u);
                        db.SaveChanges();

                        //gửi mail vào sau khi thay đổi thành công dữ liệu
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/newprofile.html"));

                        content = content.Replace("{{UserName}}", u.UserName);
                        content = content.Replace("{{Name}}", u.Name);
                        content = content.Replace("{{ModifiedBy}}", Session["UserName"].ToString());

                        var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                        new MailHelper().SendMail(u.Email, "Thay đổi hồ sơ tài khoản thành công", content);
                        new MailHelper().SendMail(toEmail, "Thay đổi hồ sơ tài khoản thành công", content);

                        //gửi mail vào sau khi thay đổi thành công dữ liệu end
                    }
                    return RedirectToAction("HoSo", new { id = u.ID });
                }
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //Sửa hồ sơ END

        //sửa mật khẩu tài khoản
        public ActionResult EditPassword(int id)
        {
            //biến dùng trong việc gửi mail
            var t0 = Session["UserName"].ToString();
            var t2 = Session["Phone"].ToString();
            var t3 = Session["Email"].ToString();

            var u = db.Users.Single(n => n.ID == id);

            if (u.UserName == t0)
            {
                //xác nhận quyền sửa nếu tài khoản được sửa là bản thân
                var t = "EDIT_USER_SELF";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = db.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            }
            else
            {
                //xác nhận quyền tài khoản được sửa là của người khác
                var t = "EDIT_USER";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = db.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            }


            if (u == null)
            {
                return RedirectToAction("Er404", "Admin/loi/");
            }
            return View(u);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditPassword(int id, User u, string confirmpassword, string Password)
        {
            try
            {
                u = db.Users.Single(n => n.ID == id);
                if (u == null)
                {
                    return RedirectToAction("Er404", "Admin/loi/");
                }
                else
                {
                    if (u.Password == null)
                    {
                        ViewBag.Error = "Mật khẩu không được để trống";
                        return View();
                    }

                    string str1 = confirmpassword;
                    string str2 = Password;
                    if (str1 == null)
                    {
                        ViewBag.Error = "Nhập lại mật khẩu không được để trống";
                        return View();
                    }



                    var v = db.Users.Single(n => n.ID == id);
                    if (u.Gender.ToString() == null || u.Gender == true || u.Gender == false)
                    {
                        u.Gender = v.Gender;
                    }

                    if (ModelState.IsValid)
                    {
                        if (str1 != str2)
                        {
                            ViewBag.Error = "Nhập lại mật khẩu không giống với mật khẩu mới";
                            return View();
                        }

                        if (str1 == u.Password || str2 == u.Password)
                        {
                            ViewBag.Error = "Mật khẩu được nhập là mật khẩu hiện tại vui lòng xài mật khẩu khác";
                            return View();
                        }
                        if (u.ModifiedBy != null)
                        {
                            u.ModifiedBy = Session["UserName"].ToString();
                        }
                        if (u.ModifiedDate == null || u.ModifiedDate != null)
                        {
                            u.ModifiedDate = DateTime.Now;
                        }
                        UpdateModel(u);
                        db.SaveChanges();

                        //gửi mail vào sau khi thay đổi thành công dữ liệu
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/newpassword_member.html"));

                        content = content.Replace("{{UserName}}", u.UserName);
                        content = content.Replace("{{Password}}", u.Password);
                        content = content.Replace("{{Name}}", u.Name);

                        var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                        new MailHelper().SendMail(u.Email, "Thay đổi mật khẩu thành công", content);
                        new MailHelper().SendMail(toEmail, "Thay đổi mật khẩu thành công", content);
                        //gửi mail vào sau khi thay đổi thành công dữ liệu end
                    }
                }
                return RedirectToAction("HoSo", new { id = u.ID });
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //sửa mật khẩu tài khoản END

        //Chi tiet don hang
        public ActionResult Chitietdonhang(int id)
        {
            //xác nhận quyền
            var t = "VIEW_ORDER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = db.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            if (t1 != "Member")//không là khách hàng
            {
                return RedirectToAction("Index", "Admin/Home");
            }
            if (ViewBag.Permission == null)//nếu ko có quyền trên sẽ bị đá ra trang lỗi
            {
                return RedirectToAction("Er403", "Admin/loi/");
            }
            //xác nhận quyền END
            int Manguoidung = int.Parse(Session["ID"].ToString());
            ViewBag.User = db.Users.Single(c => c.ID == Manguoidung);
            ViewBag.Detail = db.OrderDetails.Where(c => c.OrderID == id).ToList();
            ViewBag.oder = db.Orders.Single(c => c.ID == id);
            ViewBag.listuser = db.Users.ToList();
            var o = db.Orders.Single(n => n.ID == id);
            if (o == null)
            {
                return RedirectToAction("Er404", "Admin/loi/");
            }
            return View(o);
        }
        //Chi tiet don hang end
    }
}
