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
    public class PublishersController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        // GET: Admin/Publishers
        public ActionResult Publishers()
        {
            //xác nhận quyền
            var t = "VIEW_PUBLISHER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var pub = data.Publishers.ToList();
            if (pub == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(pub);
        }

        public ActionResult Details(int id)
        {
            //xác nhận quyền
            var t = "VIEW_PUBLISHER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var pub = data.Publishers.Single(p=>p.ID==id);
            if (pub == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(pub);
        }

        // GET: Admin/Publishers/Create
        public ActionResult Create()
        {
            //xác nhận quyền
            var t = "ADD_PUBLISHER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t && n.RoleID == t);
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Publisher pub,HttpPostedFileBase fileUpload)
        {
            try
            {
                var count = data.Publishers.Where(c => c.Name == pub.Name).Count();
                if (count != 0)
                {
                    ViewBag.Error = "Nhà xuất bản đã tồn tại";
                    return View();
                }
                //tất cả null

                if (pub.Name == null)
                {
                    ViewBag.Error = "Tên nhà xuất bản không được để trống";
                    return View();
                }
                //Name length
                if (pub.Name.ToString().Length > 250)
                {
                    ViewBag.Error = "Tên nhà xuất bản không được vượt quá 250 ký tự";
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
                    ViewBag.Error = "Tên ảnh logo không được vượt quá 250 ký tự";
                    return View();
                }

                if (pub.CreatedDate == null)
                {
                    pub.CreatedDate = DateTime.Now;
                }
                if (pub.CreatedBy == null)
                {
                    pub.CreatedBy = Session["UserName"].ToString();
                }
                if (pub.ModifiedDate == null)
                {
                    pub.ModifiedDate = DateTime.Now;
                }
                if (pub.ModifiedBy == null)
                {
                    pub.ModifiedBy = Session["UserName"].ToString();
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
                    pub.Logo = filename;
                    data.Publishers.Add(pub);
                    data.SaveChanges();
                    //gửi mail vào sau khi thay đổi thành công dữ liệu
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/add_pub.html"));

                    content = content.Replace("{{puName}} ", pub.Name);
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    new MailHelper().SendMail(toEmail, "Nhà xuất bản đã được thêm thành công", content);
                    //gửi mail vào sau khi thay đổi thành công dữ liệu END
                }
                return RedirectToAction("Publishers");
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        public ActionResult Edit(int id)
        {
            //xác nhận quyền
            var t = "EDIT_PUBLISHER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var pub = data.Publishers.Single(p => p.ID == id);
            if(pub==null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(pub);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id,Publisher pub, HttpPostedFileBase fileUpload)
        {
            try
            {
                pub = data.Publishers.Single(p => p.ID == id);
                if (pub == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                if (pub.Name.ToString() == null)
                {
                    ViewBag.Error = "Không được để trống tên nhà xuất bản";
                    return View();
                }
                //Name length
                if (pub.Name.ToString().Length > 250)
                {
                    ViewBag.Error = "Tên nhà xuất bản không được vượt quá 250 ký tự";
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
                            pub.Logo = filename;
                            if (pub.ModifiedBy != null)
                            {
                                pub.ModifiedBy = Session["UserName"].ToString();
                            }
                            if (pub.ModifiedDate == null || pub.ModifiedDate != null)
                            {
                                pub.ModifiedDate = DateTime.Now;
                            }
                            UpdateModel(pub);
                            data.SaveChanges();
                            //gửi mail vào sau khi thay đổi thành công dữ liệu
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/edit_pub.html"));

                            content = content.Replace("{{puName}} ", pub.Name);
                            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                            new MailHelper().SendMail(toEmail, "Nhà xuất bản đã được cập nhật thành công", content);
                            //gửi mail vào sau khi thay đổi thành công dữ liệu END
                        }
                        return RedirectToAction("Publishers");
                    }
                }
                else
                {
                    var s = data.Publishers.Single(c => c.ID == id);
                    pub.Logo = s.Logo;
                    if (ModelState.IsValid)
                    {
                        if (pub.ModifiedBy != null)
                        {
                            pub.ModifiedBy = Session["UserName"].ToString();
                        }
                        if (pub.ModifiedDate == null || pub.ModifiedDate != null)
                        {
                            pub.ModifiedDate = DateTime.Now;
                        }
                        UpdateModel(pub);
                        data.SaveChanges();
                    }
                    return RedirectToAction("Publishers");
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
            var t = "DELETE_PUBLISHER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var pub = data.Publishers.Single(p => p.ID == id);
            if(pub==null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(pub);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var pub = data.Publishers.Single(p => p.ID == id);
            var t= Session["UserName"].ToString();
            //kiem tra neu co sách đang mang tên tác giả này
            var listAu = data.Books.Where(a => a.PublisherID == id);
            if (listAu != null)
            {
                ViewBag.Error = "Bạn không thể xóa nhà xuất bản này nếu vẫn có các quyển sách đang thuộc nhà xuất bản này";
                return RedirectToAction("Delete", new { id = pub.ID });
            }
            if (pub == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            data.Publishers.Remove(pub);
            data.SaveChanges();
            //gửi mail vào sau khi thay đổi thành công dữ liệu
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/delete_pub.html"));

            content = content.Replace("{{puName}} ", pub.Name);
            content = content.Replace("{{By}} ", t);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(toEmail, "Nhà xuất bản đã được xóa thành công", content);
            //gửi mail vào sau khi thay đổi thành công dữ liệu END
            return RedirectToAction("Publishers");
        }
    }
}