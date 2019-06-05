using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using Common;
using System.IO;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        //danh sách khách
        public ActionResult Users()
        {
            var u = data.Users.ToList();
            //xác nhận quyền
            var t = "VIEW_USER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }

        //danh sach nhan vien
        public ActionResult Employee()
        {
            var u = data.Users.ToList();
            //xác nhận quyền
            var t = "VIEW_USER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }

        //danh sách Chua phan nhom
        public ActionResult StandbyAccount()
        {
            var u = data.Users.ToList();
            //xác nhận quyền
            var t = "VIEW_USER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }

        //Hồ sơ user
        public ActionResult UserProfile(int id)
        {
            var u = data.Users.Single(c => c.ID == id);
            //MEMBER khach hang
            ViewBag.Order = data.Orders.Where(N => N.UserID == id).ToList();
            ViewBag.Order1 = data.Orders.Where(N => N.UserID == id).ToList().OrderByDescending(n=>n.CreatedDate);
            ViewBag.OrderDetail = data.OrderDetails.ToList();
            ViewBag.User = data.Users.Where(c => c.ID == id).ToList();

            //Admin
            //create
            ViewBag.Book = data.Books.Where(N => N.CreatedBy == u.UserName).ToList().OrderByDescending(n => n.CreatedDate);
            ViewBag.Category = data.Categories.Where(N => N.CreatedBy == u.UserName).ToList().OrderByDescending(n => n.CreatedDate);
            ViewBag.Publisher = data.Publishers.Where(N => N.CreatedBy == u.UserName).ToList().OrderByDescending(n => n.CreatedDate);
            ViewBag.Author = data.Authors.Where(N => N.CreatedBy == u.UserName).ToList().OrderByDescending(n => n.CreatedDate);
            //modified
            ViewBag.Order2 = data.Orders.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            ViewBag.Book1 = data.Books.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            ViewBag.Category1 = data.Categories.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            ViewBag.Publisher1 = data.Publishers.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            ViewBag.Author1 = data.Authors.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            //ADMIN END

            //xác nhận quyền
            var t = "VIEW_USER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);

            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }

        //sửa mật khẩu tài khoản
        public ActionResult EditPassword(int id)
        {
            //biến dùng trong việc gửi mail
            var t0 = Session["UserName"].ToString();
            var t2 = Session["Phone"].ToString();
            var t3 = Session["Email"].ToString();

            var u = data.Users.Single(n=>n.ID==id);

            if(u.UserName==t0)
            {
                //xác nhận quyền sửa nếu tài khoản được sửa là bản thân
                var t = "EDIT_USER_SELF";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            }
            else
            {
                //xác nhận quyền tài khoản được sửa là của người khác
                var t = "EDIT_USER";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            }
            

            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult EditPassword(int id,User u, string confirmpassword, string Password)
        {
            try
            {
                u = data.Users.Single(n => n.ID == id);
                if (u == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                else
                {
                    if(u.Password==null)
                    {
                        ViewBag.Error = "Mật khẩu không được để trống";
                        return View();
                    }

                    string str1 = confirmpassword;
                    string str2 = Password;
                    if (str1==null)
                    {
                        ViewBag.Error = "Nhập lại mật khẩu không được để trống";
                        return View();
                    }

                    

                    var v = data.Users.Single(n=>n.ID==id);
                    if (u.Gender.ToString()==null || u.Gender==true ||u.Gender==false)
                    {
                        u.Gender = v.Gender;
                    }

                    if(ModelState.IsValid)
                    {
                        if (str1 != str2)
                        {
                            ViewBag.Error = "Nhập lại mật khẩu không giống với mật khẩu mới";
                            return View();
                        }
                        
                        if(str1==u.Password || str2== u.Password)
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
                        data.SaveChanges();

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
                return RedirectToAction("UserProfile",new { id=u.ID});
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //sửa mật khẩu tài khoản END

        //Sửa hồ sơ
        public ActionResult EditProfile(int id)
        {
            ViewBag.List = new SelectList(data.UserGroups.Where(c => c.ID != "Admin" && c.ID != "Member").ToList().OrderBy(n => n.Name), "ID", "Name");
            //biến dùng trong việc gửi mail
            var t0 = Session["UserName"].ToString();

            var u = data.Users.Single(n => n.ID == id);

            if (u.UserName == t0)
            {
                //xác nhận quyền sửa nếu tài khoản được sửa là bản thân
                var t = "EDIT_USER_SELF";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            }
            else
            {
                //xác nhận quyền tài khoản được sửa là của người khác
                var t = "EDIT_USER";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            }

            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult EditProfile(int id, User u,HttpPostedFileBase fileUpload)
        {
            try
            {
                u = data.Users.Single(n => n.ID == id);
                if (u == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }

                var t = data.Users.Single(c => c.ID == id);
                //Kiểm tra học tên
                if(u.Name==null)
                {
                    ViewBag.Error = "Họ tên bạn không để trống";
                    return View();
                }
                if(u.Name.Length>250)
                {
                    ViewBag.Error = "Họ tên không được dài hơn 250 ký tự";
                    return View();
                }
                //Kiểm tra học tên end

                //Kiểm mật khẩu
                if(u.Password!=null || u.Password==null)
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
                            if (System.IO.File.Exists(path1) || System.IO.File.Exists(path2))
                            {
                                ViewBag.Error = "Hình ảnh đã tồn tại";
                                return View();
                            }
                            else
                            {
                                //LUU HINH ANH VAO DUONG DAN
                                fileUpload.SaveAs(path1);
                                fileUpload.SaveAs(path2);
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
                            data.SaveChanges();
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

                        return RedirectToAction("UserProfile",new { id=u.ID});
                    }
                }
                else
                {
                    var s = data.Users.Single(c => c.ID == id);
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
                        data.SaveChanges();

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
                    return RedirectToAction("UserProfile", new { id = u.ID });
                }
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
        //Sửa hồ sơ END

        //Xóa tài khoản và tất cả các đơn hàng có liên quan
        public ActionResult Delete(int id)
        {
            var u = data.Users.Single(c => c.ID == id);

            ViewBag.Order = data.Orders.Where(N => N.UserID == id).ToList();
            ViewBag.Order1 = data.Orders.Where(N => N.UserID == id).ToList().OrderByDescending(n => n.CreatedDate);
            ViewBag.OrderDetail = data.OrderDetails.ToList();
            ViewBag.User = data.Users.Where(c => c.ID == id).ToList();

            //Admin
            //create
            ViewBag.Book = data.Books.Where(N => N.CreatedBy == u.UserName).ToList().OrderByDescending(n => n.CreatedDate);
            ViewBag.Category = data.Categories.Where(N => N.CreatedBy == u.UserName).ToList().OrderByDescending(n => n.CreatedDate);
            ViewBag.Publisher = data.Publishers.Where(N => N.CreatedBy == u.UserName).ToList().OrderByDescending(n => n.CreatedDate);
            ViewBag.Author = data.Authors.Where(N => N.CreatedBy == u.UserName).ToList().OrderByDescending(n => n.CreatedDate);
            //modified
            ViewBag.Order2 = data.Orders.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            ViewBag.Book1 = data.Books.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            ViewBag.Category1 = data.Categories.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            ViewBag.Publisher1 = data.Publishers.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            ViewBag.Author1 = data.Authors.Where(N => N.ModifiedBy == u.UserName).ToList().OrderByDescending(n => n.ModifiedDate);
            //ADMIN END

            //xác nhận quyền xóa
            var t = "DELETE_USER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);

            if (u == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(u);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var bo = data.Users.Single(p => p.ID == id);
            if (bo == null)
            {
                return RedirectToAction("Er404", "loi/");
            }

            // biến lưu email của tài khoản bị xóa
            string email = bo.Email.ToString();
            // biến lưu tên tài khoản của tài khoản bị xóa
            string username = bo.UserName.ToString();
            // biến lưu HỌ TÊN của tài khoản bị xóa
            string name = bo.Name.ToString();

            //kiểm tra và xóa các đơn hàng nếu có

            //biến danh sách đơn hàng
            var od = data.Orders.Where(c => c.UserID == id).ToList();

            //Đếm số đơn hàng của người dùng đó
            var count = data.Orders.Where(c => c.UserID == id).Count();
            if(count>=1)
            {
                //vòng lặp xóa các đơn hàng của khách
                foreach(var order in od)
                {
                    //biến danh sách chi tiết đơn hàng
                    var odd = data.OrderDetails.Where(C => C.OrderID == order.ID).ToList();
                    //vòng lặp xóa chi tiết đơn hàng
                    foreach(var detail in odd)
                    {
                        data.OrderDetails.Remove(detail);
                        //data.SaveChanges();
                    }
                    data.Orders.Remove(order);
                    //data.SaveChanges();
                }
            }
            //kiểm tra và xóa các đơn hàng nếu có end

            //xóa user
            data.Users.Remove(bo);
            data.SaveChanges();

            //gửi mail vào sau khi thay đổi thành công dữ liệu
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/delete_user_allref.html"));

            content = content.Replace("{{UserName}}", username);
            content = content.Replace("{{Name}}", name);
            content = content.Replace("{{ModifiedBy}}", Session["UserName"].ToString());

            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(email, "Xóa tài khoản thành công", content);
            new MailHelper().SendMail(toEmail, "Thay tài khoản thành công", content);
            //gửi mail vào sau khi thay đổi thành công dữ liệu end

            return RedirectToAction("Users");
        }
        //Xóa tài khoản và tất cả các đơn hàng có liên quan end

        //create new tai khoan thuoc nhóm standbyaccount
        public ActionResult Create()
        {
            //xác nhận quyền THÊM
            var t = "ADD_USER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(User user, HttpPostedFileBase fileUpload, string confirmpassword)
        {
            //định dạng email

            //Khởi tạo mặc định nhóm người dùng là member
            if (user.UserGroupID == null)
            {
                user.UserGroupID = "StandbyAccount";
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
                if(user.Email.Contains("@")==false || user.Email.Contains("@.") == true || user.Email.Contains(".@") == true || user.Email.Contains(".com") == false || array[0].ToString()=="@")
                {
                    ViewBag.Error = "Email không đúng định dạng. định dạng đúng vd: minh@gmail.com";
                    return View();
                }
            }

            //Các thông số của Model User được thỏa mãn tiến hành lưu vào csdl
            if (ModelState.IsValid)
            {
                //Tên đăng nhập không được tạo trùng nhau
                var countUserName = data.Users.Where(u => u.UserName == user.UserName).Count();
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
                data.Users.Add(user);
                data.SaveChanges();
            }
            //load lại và hiện thông báo
            ModelState.Clear();
            ViewBag.Message = user.Name + " đã đăng ký tài khoản thành công";
            return View();
        }
        //create new tai khoan thuoc nhóm standbyaccount end
    }
}