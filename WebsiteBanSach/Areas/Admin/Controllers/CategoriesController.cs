using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using iTextSharp.tool.xml;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Text;
using System.Data.Entity;
using System.Configuration;
using Common;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        // GET: Admin/Categories
        public ActionResult Categories(int? page)
        {
            //xác nhận quyền
            var t= "VIEW_CATEGORY";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            var s = data.Categories.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize);
            if (s == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(s);
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int id)
        {
            //xác nhận quyền
            var t = "VIEW_CATEGORY";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var cate = data.Categories.Single(n => n.ID == id);
            if (cate == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(cate);
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult Export(string ExportData)
        //{
            
        //    using (MemoryStream stream = new System.IO.MemoryStream())
        //    {
        //        //Font f = new Font(BaseFont.CreateFont("D:\\DACS\\WebsiteBanSach\\WebsiteBanSach\\fonts\\vuArial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED));
        //        var msHTML = new MemoryStream(Encoding.UTF8.GetBytes(ExportData));
        //        var msCSS = new MemoryStream(Encoding.UTF8.GetBytes(ExportData));
        //        var fontImp = new iTextSharp.tool.xml.XMLWorkerFontProvider("D:\\DACS\\WebsiteBanSach\\WebsiteBanSach\\fonts\\vuArial.ttf");
        //        fontImp.DefaultEncoding = "Identity-H";

        //        StringReader reader = new StringReader(ExportData);
                
        //        Document PdfFile = new Document(PageSize.A4);
        //        PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
        //        PdfFile.Open();
        //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, msHTML, msCSS, Encoding.UTF8, fontImp);
        //        PdfFile.Close();
        //        return File(stream.ToArray(), "application/pdf", "ExportData.pdf");
        //    }
        //}

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            //xác nhận quyền
            var t = "ADD_CATEGORY";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            return View();
        }

        // POST: Admin/Categories/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Category cate)
        {
            try
            {
                var count = data.Categories.Where(c => c.Name == cate.Name).Count();
                if(count!=0)
                {
                    ViewBag.Error = "Thể loại đã tồn tại";
                    return View();
                }
                //tất cả null

                if(cate.Name==null)
                {
                    ViewBag.Error = "Tên thể loại không được để trống";
                    return View();
                }
                //Name length
                if (cate.Name.ToString().Length > 250)
                {
                    ViewBag.Error = "Tên thể loại không được vượt quá 250 ký tự";
                    return View();
                }

                if (cate.Description == null)
                {
                    ViewBag.Error = "Mô tả không được để trống";
                    return View();
                }
                //Name length
                if (cate.Description.ToString().Length > 900)
                {
                    ViewBag.Error = "Mô tả không được vượt quá 900 ký tự";
                    return View();
                }

                if (cate.CreatedDate==null)
                {
                    cate.CreatedDate = DateTime.Now;
                }
                if (cate.CreatedBy == null)
                {
                    cate.CreatedBy = Session["UserName"].ToString();
                }
                if (cate.ModifiedDate == null)
                {
                    cate.ModifiedDate = DateTime.Now;
                }
                if (cate.ModifiedBy == null)
                {
                    cate.ModifiedBy = Session["UserName"].ToString();
                }
                if(ModelState.IsValid)
                {
                    data.Categories.Add(cate);
                    data.SaveChanges();

                    //gửi mail vào sau khi thay đổi thành công dữ liệu
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/add_cate.html"));

                    content = content.Replace("{{CateName}} ", cate.Name);
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                    
                    new MailHelper().SendMail(toEmail, "Thêm thể loại mới thàng thành công", content);
                    //gửi mail vào sau khi thay đổi thành công dữ liệu END
                }
                return RedirectToAction("Categories");
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int id)
        {
            //xác nhận quyền
            var t = "EDIT_CATEGORY";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var cate = data.Categories.Single(c => c.ID == id);
            if (cate == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(cate);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Category cate)
        {
            try
            {
                cate = data.Categories.Single(c => c.ID == id);
                if (cate.Name.ToString()==null)
                {
                    ViewBag.Error = "Không được để trống tên thể loại";
                    return View();
                }
                else
                {
                    if(cate.Description.ToString()==null)
                    {
                        ViewBag.Error = "Không được để trống mô tả";
                        return View();
                    }
                    else
                    {
                        if(ModelState.IsValid)
                        {
                            if(cate.ModifiedBy!=null)
                            {
                                cate.ModifiedBy = Session["UserName"].ToString();
                            }
                            if(cate.ModifiedDate==null || cate.ModifiedDate != null)
                            {
                                cate.ModifiedDate = DateTime.Now;
                            }
                            UpdateModel(cate);
                            data.SaveChanges();
                            //gửi mail vào sau khi thay đổi thành công dữ liệu
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/edit_cate.html"));

                            content = content.Replace("{{CateName}} ", cate.Name);
                            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                            new MailHelper().SendMail(toEmail, "Thê loại đã được cập nhật thành công", content);
                            //gửi mail vào sau khi thay đổi thành công dữ liệu END
                        }
                        return RedirectToAction("Categories");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int id)
        {
            //xác nhận quyền
            var t = "DELETE_CATEGORY";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var cate = data.Categories.Single(c => c.ID == id);
            if (cate == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(cate);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                var cate = data.Categories.Single(c => c.ID == id);
                var name = cate.Name;
                var t = Session["UserName"].ToString();
                var listAu = data.Books.Where(a => a.CategoryID == id);
                if (listAu != null)
                {
                    ViewBag.Error = "Bạn không thể xóa thể loại này nếu vẫn có các quyển sách thuộc thể loại này";
                    return RedirectToAction("Delete", new { id = cate.ID });
                }
                if (cate==null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                data.Categories.Remove(cate);
                data.SaveChanges();
                //gửi mail vào sau khi thay đổi thành công dữ liệu
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/delete_cate.html"));

                content = content.Replace("{{CateName}} ", name);
                content = content.Replace("{{By}} ", t);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(toEmail, "Thể loại đã được xóa thành công", content);
                //gửi mail vào sau khi thay đổi thành công dữ liệu END
                return RedirectToAction("Categories");
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }
    }
}
