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
    public class BooksController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        // GET: Admin/Books
        public ActionResult Books()
        {
            var b = data.Books.ToList().OrderBy(n => n.Name);
            //xác nhận quyền
            var t = "VIEW_BOOK";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            if (b==null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(b);
        }

        public ActionResult Details(int id)
        {
            //xác nhận quyền
            var t = "VIEW_BOOK";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);

            var b = data.Books.Single(n => n.ID == id);
            if (b == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(b);
        }

        [ValidateInput(false)]
        public ActionResult Create()
        {
            try
            {
                //xác nhận quyền
                var t = "ADD_BOOK";
                var t1 = Session["UserGroup"].ToString();
                ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);

                ViewBag.AuthorID = new SelectList(data.Authors.ToList().OrderBy(n => n.Name), "ID", "Name");
                ViewBag.CategoryID = new SelectList(data.Categories.ToList().OrderBy(n => n.Name), "ID", "Name");
                ViewBag.PublisherID = new SelectList(data.Publishers.ToList().OrderBy(n => n.Name), "ID", "Name");
                var Salelist = new List<SelectListItem>();
                for (var i = 0; i < 91; i++)
                    Salelist.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() + "%" });
                ViewBag.Salelist = Salelist;
                return View();
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Book b, HttpPostedFileBase fileUpload)
        {
            try
            {
                ViewBag.AuthorID = new SelectList(data.Authors.ToList().OrderBy(n => n.Name), "ID", "Name");
                ViewBag.CategoryID = new SelectList(data.Categories.ToList().OrderBy(n => n.Name), "ID", "Name");
                ViewBag.PublisherID = new SelectList(data.Publishers.ToList().OrderBy(n => n.Name), "ID", "Name");
                var Salelist = new List<SelectListItem>();
                for (var i = 0; i < 91; i++)
                    Salelist.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() + "%" });
                ViewBag.Salelist = Salelist;
                if (b == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }

                var count = data.Books.Where(c => c.Name == b.Name).Count();
                if (count != 0)
                {
                    ViewBag.Error = "Sách đã tồn tại";
                    return View();
                }

                //tên null
                if (b.Name == null)
                {
                    ViewBag.Error = "Tên sách không được để trống";
                    return View();
                }
                //Name length
                if (b.Name.ToString().Length > 250)
                {
                    ViewBag.Error = "Tên tác giả không được vượt quá 250 ký tự";
                    return View();
                }

                //mo ta
                if (b.Description == null)
                {
                    ViewBag.Error = "Mô tả không được để trống";
                    return View();
                }
                if (b.Description.ToString().Length > 900)
                {
                    ViewBag.Error = "Mô tả không được vượt quá 900 ký tự";
                    return View();
                }

                //gia
                if(b.Price.ToString() == null)
                {
                    ViewBag.Error = "Giá không được để trống";
                    return View();
                }
                if (b.PromotionPrice.ToString() == null)
                {
                    ViewBag.Error = "Giá khuyến mãi không được để trống";
                    return View();
                }

                //Số lượng tồn kho
                if (b.Quantity.ToString() == null)
                {
                    ViewBag.Error = "Số lượng tồn kho không được để trống";
                    return View();
                }
                if(b.Quantity<0)
                {
                    ViewBag.Error = "Số lượng tồn kho không được nhỏ hơn không";
                    return View();
                }

                //Tác giả
                if(b.AuthorID.ToString() == null)
                {
                    ViewBag.Error = "Tác giả không được để trống";
                    return View();
                }

                //Thể loại
                if (b.CategoryID.ToString() == null)
                {
                    ViewBag.Error = "Thể loại không được để trống";
                    return View();
                }

                //Nhà xuất bản
                if (b.PublisherID.ToString() == null)
                {
                    ViewBag.Error = "Thể loại không được để trống";
                    return View();
                }

                //chi tiết
                if(b.Detail == null)
                {
                    ViewBag.Error = "Chi tiết không được để trống";
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

                //lượt xem null
                if (b.ViewCount.ToString()==null)
                {
                    b.ViewCount = 0;
                }

                //người tạo & ngày tạo,  người cập nhật và ngày cập nhật
                if (b.CreatedDate == null)
                {
                    b.CreatedDate = DateTime.Now;
                }
                if (b.CreatedBy == null)
                {
                    b.CreatedBy = Session["UserName"].ToString();
                }
                if (b.ModifiedDate == null)
                {
                    b.ModifiedDate = DateTime.Now;
                }
                if (b.ModifiedBy == null)
                {
                    b.ModifiedBy = Session["UserName"].ToString();
                }
                
                //tình trạng
                if(b.Quantity>0)
                {
                    b.Status = true;
                }
                else
                {
                    b.Status = false;
                }
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
                    b.Image = filename;
                    data.Books.Add(b);
                    data.SaveChanges();
                    //gửi mail vào sau khi thay đổi thành công dữ liệu
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/add_b.html"));

                    content = content.Replace("{{BName}} ", b.Name);
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    new MailHelper().SendMail(toEmail, "Sách đã được thêm thành công", content);
                    //gửi mail vào sau khi thay đổi thành công dữ liệu END
                }
                return RedirectToAction("Books");
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        public ActionResult Edit(int id)
        {
            //xác nhận quyền
            var t = "EDIT_BOOK";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);

            var b = data.Books.Single(n => n.ID == id);
            if (b == null)
            {
                return RedirectToAction("Er404", "loi/");
            }

            //dropdownlist
            ViewBag.AuthorID = new SelectList(data.Authors.ToList().OrderBy(n => n.Name), "ID", "Name");
            ViewBag.CategoryID = new SelectList(data.Categories.ToList().OrderBy(n => n.Name), "ID", "Name");
            ViewBag.PublisherID = new SelectList(data.Publishers.ToList().OrderBy(n => n.Name), "ID", "Name");
            var Salelist = new List<SelectListItem>();
            for (var i = 0; i < 91; i++)
                Salelist.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() + "%" });
            ViewBag.Salelist = Salelist;

            return View(b);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Edit(int id,Book bo,HttpPostedFileBase fileUpload)
        {
            try
            {
                bo = data.Books.Single(m => m.ID == id);
                //dropdownlist
                ViewBag.AuthorID = new SelectList(data.Authors.ToList().OrderBy(n => n.Name), "ID", "Name");
                ViewBag.CategoryID = new SelectList(data.Categories.ToList().OrderBy(n => n.Name), "ID", "Name");
                ViewBag.PublisherID = new SelectList(data.Publishers.ToList().OrderBy(n => n.Name), "ID", "Name");
                var Salelist = new List<SelectListItem>();
                for (var i = 0; i < 91; i++)
                    Salelist.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() + "%" });
                ViewBag.Salelist = Salelist;

                if (bo == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }

                //var count = data.Books.Where(c => c.Name == bo.Name).Count();
                //if (count != 0)
                //{
                //    ViewBag.Error = "Sách đã tồn tại";
                //    return View();
                //}

                //tên null
                if (bo.Name == null)
                {
                    ViewBag.Error = "Tên sách không được để trống";
                    return View();
                }
                //Name length
                if (bo.Name.ToString().Length > 250)
                {
                    ViewBag.Error = "Tên tác giả không được vượt quá 250 ký tự";
                    return View();
                }

                //mo ta
                if (bo.Description == null)
                {
                    ViewBag.Error = "Mô tả không được để trống";
                    return View();
                }
                if (bo.Description.ToString().Length > 900)
                {
                    ViewBag.Error = "Mô tả không được vượt quá 900 ký tự";
                    return View();
                }

                //gia
                if (bo.Price.ToString() == null)
                {
                    ViewBag.Error = "Giá không được để trống";
                    return View();
                }
                if (bo.PromotionPrice.ToString() == null)
                {
                    ViewBag.Error = "Giá khuyến mãi không được để trống";
                    return View();
                }

                //Số lượng tồn kho
                if (bo.Quantity.ToString() == null)
                {
                    ViewBag.Error = "Số lượng tồn kho không được để trống";
                    return View();
                }
                if (bo.Quantity < 0)
                {
                    ViewBag.Error = "Số lượng tồn kho không được nhỏ hơn không";
                    return View();
                }

                //Tác giả
                if (bo.AuthorID.ToString() == null)
                {
                    ViewBag.Error = "Tác giả không được để trống";
                    return View();
                }

                //Thể loại
                if (bo.CategoryID.ToString() == null)
                {
                    ViewBag.Error = "Thể loại không được để trống";
                    return View();
                }

                //Nhà xuất bản
                if (bo.PublisherID.ToString() == null)
                {
                    ViewBag.Error = "Thể loại không được để trống";
                    return View();
                }

                //chi tiết
                if (bo.Detail == null)
                {
                    ViewBag.Error = "Chi tiết không được để trống";
                    return View();
                }

                //tình trạng
                if (bo.Quantity > 0)
                {
                    bo.Status = true;
                }
                else
                {
                    bo.Status = false;
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
                            bo.Image = filename;
                            if (bo.ModifiedBy != null)
                            {
                                bo.ModifiedBy = Session["UserName"].ToString();
                            }
                            if (bo.ModifiedDate == null || bo.ModifiedDate != null)
                            {
                                bo.ModifiedDate = DateTime.Now;
                            }
                            UpdateModel(bo);
                            data.SaveChanges();
                            //gửi mail vào sau khi thay đổi thành công dữ liệu
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/edit_b.html"));

                            content = content.Replace("{{BName}} ", bo.Name);
                            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                            new MailHelper().SendMail(toEmail, "Sách đã được thay đổi thành công", content);
                            //gửi mail vào sau khi thay đổi thành công dữ liệu END
                        }
                        return RedirectToAction("Books");
                    }
                }
                else
                {
                    var s = data.Books.Single(c => c.ID == id);
                    bo.Image = s.Image;
                    if (ModelState.IsValid)
                    {
                        if (bo.ModifiedBy != null)
                        {
                            bo.ModifiedBy = Session["UserName"].ToString();
                        }
                        if (bo.ModifiedDate == null || bo.ModifiedDate != null)
                        {
                            bo.ModifiedDate = DateTime.Now;
                        }
                        UpdateModel(bo);
                        data.SaveChanges();
                    }
                    return RedirectToAction("Books");
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
            var t = "DELETE_BOOK";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);

            var bo = data.Books.Single(p => p.ID == id);
            if (bo == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(bo);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var bo = data.Books.Single(p => p.ID == id);
            var name = bo.Name;
            var t = Session["UserName"].ToString();
            if (bo == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            data.Books.Remove(bo);
            data.SaveChanges();
            //gửi mail vào sau khi thay đổi thành công dữ liệu
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/delete_b.html"));

            content = content.Replace("{{BName}} ", bo.Name);
            content = content.Replace("{{By}} ", t);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(toEmail, "Sách đã được xóa thành công", content);
            //gửi mail vào sau khi thay đổi thành công dữ liệu END
            return RedirectToAction("Books");
        }
    }
}