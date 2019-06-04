using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class AuthorsController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        // GET: Admin/Authors
        public ActionResult Authors()
        {
            //xác nhận quyền
            var t = "VIEW_AUTHOR";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);

            var au = data.Authors.ToList().OrderBy(n => n.ID);
            if (au == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(au);
        }

        public ActionResult Details(int id)
        {
            //xác nhận quyền
            var t = "VIEW_AUTHOR";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var pub = data.Authors.Single(p => p.ID == id);
            if (pub == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(pub);
        }

        public ActionResult Create()
        {
            //xác nhận quyền
            var t = "ADD_AUTHOR";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            return View();
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Create(Author au,HttpPostedFileBase fileUpload)
        {
            try
            {
                var count = data.Authors.Where(c => c.Name == au.Name).Count();
                if (count != 0)
                {
                    ViewBag.Error = "Tác giả đã tồn tại";
                    return View();
                }


                //tất cả null
                if (au.Name == null)
                {
                    ViewBag.Error = "Tên tác giả không được để trống";
                    return View();
                }

                //Name length
                if (au.Name.ToString().Length > 250)
                {
                    ViewBag.Error = "Tên tác giả không được vượt quá 250 ký tự";
                    return View();
                }

                //mo ta
                if (au.Description ==  null)
                {
                    ViewBag.Error = "Mô tả không được để trống";
                    return View();
                }
                if (au.Description.ToString().Length > 900)
                {
                    ViewBag.Error = "Mô tả không được vượt quá 900 ký tự";
                    return View();
                }

                //kiem tra duong dan file
                if (fileUpload == null)
                {
                    ViewBag.Error = "Vui lòng chọn ảnh";
                    return View();
                }

                var filename_logo = Path.GetFileName(fileUpload.FileName);
                //Name logo length
                if (filename_logo.ToString().Length > 250)
                {
                    ViewBag.Error = "Tên ảnh không được vượt quá 250 ký tự";
                    return View();
                }

                if (au.CreatedDate == null)
                {
                    au.CreatedDate = DateTime.Now;
                }
                if (au.CreatedBy == null)
                {
                    au.CreatedBy = Session["UserName"].ToString();
                }
                if (au.ModifiedDate == null)
                {
                    au.ModifiedDate = DateTime.Now;
                }
                if (au.ModifiedBy == null)
                {
                    au.ModifiedBy = Session["UserName"].ToString();
                }
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
                    au.Logo = filename;
                    data.Authors.Add(au);
                    data.SaveChanges();
                    //gửi mail vào sau khi thay đổi thành công dữ liệu
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/add_au.html"));

                    content = content.Replace("{{auName}} ", au.Name);
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    new MailHelper().SendMail(toEmail, "Tác giả đã được thêm thành công", content);
                    //gửi mail vào sau khi thay đổi thành công dữ liệu END
                }
                return RedirectToAction("Authors");
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        public ActionResult Edit(int id)
        {
            //xác nhận quyền
            var t = "EDIT_AUTHOR";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var au = data.Authors.Single(p => p.ID == id);
            if (au == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(au);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, Author au, HttpPostedFileBase fileUpload)
        {
            try
            {
                au = data.Authors.Single(p => p.ID == id);
                if (au == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                if (au.Name.ToString() == null)
                {
                    ViewBag.Error = "Không được để trống tên nhà xuất bản";
                    return View();
                }
                if (au.Name.ToString().Length > 250)
                {
                    ViewBag.Error = "Tên tác giả không được vượt quá 250 ký tự";
                    return View();
                }
                if (au.Description.ToString() == null)
                {
                    ViewBag.Error = "Không được để trống mô tả";
                    return View();
                }
                if (au.Description.ToString().Length > 900)
                {
                    ViewBag.Error = "Mô tả không được vượt quá 900 ký tự";
                    return View();
                }
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
                            au.Logo = filename;
                            if (au.ModifiedBy != null)
                            {
                                au.ModifiedBy = Session["UserName"].ToString();
                            }
                            if (au.ModifiedDate == null || au.ModifiedDate != null)
                            {
                                au.ModifiedDate = DateTime.Now;
                            }
                            UpdateModel(au);
                            data.SaveChanges();
                            //gửi mail vào sau khi thay đổi thành công dữ liệu
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/edit_au.html"));

                            content = content.Replace("{{auName}} ", au.Name);
                            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                            new MailHelper().SendMail(toEmail, "Tác giả đã được cập nhật thành công", content);
                            //gửi mail vào sau khi thay đổi thành công dữ liệu END
                        }
                        return RedirectToAction("Authors");
                    }
                }
                else
                {
                    var s = data.Authors.Single(c => c.ID == id);
                    au.Logo = s.Logo;
                    if(ModelState.IsValid)
                    {
                        if (au.ModifiedBy != null)
                        {
                            au.ModifiedBy = Session["UserName"].ToString();
                        }
                        if (au.ModifiedDate == null || au.ModifiedDate != null)
                        {
                            au.ModifiedDate = DateTime.Now;
                        }
                        UpdateModel(au);
                        data.SaveChanges();
                    }
                    return RedirectToAction("Authors");
                }
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        public ActionResult Delete(int id)
        {
            //xác nhận quyền
            var t = "DELETE_AUTHOR";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var au = data.Authors.Single(p => p.ID == id);
            if (au == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(au);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var au = data.Authors.Single(p => p.ID == id);
            var name = au.Name;
            var t = Session["UserName"].ToString();
            //kiem tra neu co sách đang mang tên tác giả này
            var listAu = data.Books.Where(a => a.AuthorID == id);
            if(listAu!=null)
            {
                ViewBag.Error = "Bạn không thể xóa tác giả này nếu vẫn có các quyển sách đang có tên tác giả này";
                return RedirectToAction("Delete", new { id = au.ID});
            }
            if (au == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            data.Authors.Remove(au);
            data.SaveChanges();
            //gửi mail vào sau khi thay đổi thành công dữ liệu
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/delete_au.html"));

            content = content.Replace("{{auName}} ", au.Name);
            content = content.Replace("{{By}} ", t);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(toEmail, "Tác giả đã được xóa thành công", content);
            //gửi mail vào sau khi thay đổi thành công dữ liệu END
            return RedirectToAction("Authors");
        }
    }
}