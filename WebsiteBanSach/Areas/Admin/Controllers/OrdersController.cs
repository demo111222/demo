using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using WebsiteBanSach.ViewModels;
using Rotativa;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        BookStoreDB data = new BookStoreDB();
        // GET: Admin/Orders
        public ActionResult Orders()
        {
            //xác nhận quyền
            var t = "VIEW_ORDER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var o = data.Orders.ToList();
            if (o == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(o);
        }

        public ActionResult Details(int id)
        {
            //xác nhận quyền
            var t = "VIEW_ORDER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            ViewBag.Detail = data.OrderDetails.Where(c => c.OrderID == id).ToList();
            ViewBag.oder = data.Orders.Single(c => c.ID == id);
            var o = data.Orders.Single(n => n.ID == id);
            if (o == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(o);
        }

        //pdf detail
        public ActionResult DownloadPartialViewPDF(int id)
        {
            var o = data.Orders.Single(n => n.ID == id);
            ViewBag.Detail = data.OrderDetails.Where(c => c.OrderID == id).ToList();
            ViewBag.oder = data.Orders.Single(c => c.ID == id);
            return new Rotativa.PartialViewAsPdf("_PDFOrderPartial",o) { FileName = "Hoá đơn.pdf" };
        }
        //pdf detail end

        public ActionResult Edit(int id)
        {
            //xác nhận quyền
            var t = "EDIT_ORDER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            ViewBag.Detail = data.OrderDetails.Where(c => c.OrderID == id).ToList();
            var o = data.Orders.Single(n => n.ID == id);
            if (o == null)
            {
                return RedirectToAction("Er404", "loi/");
            }

            if (o.CheckoutStatus == false || o.CheckoutStatus == true)
            {
                return RedirectToAction("ErOrderFalse", "loi/");
            }
            return View(o);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, Order o, List<OrderDetail> od)
        {
            try
            {
                //danh sách các sản phẩm thuoc don hàng trên
                ViewBag.Detail = data.OrderDetails.Where(c => c.OrderID == id).ToList();
                // danh sách các quyển sách
                ViewBag.Book = data.Books.ToList();

                o = data.Orders.Single(n => n.ID == id);
                var deliverstatus = o.DeliveryStatus;
                var checkoutstatus = o.CheckoutStatus;
                if (o == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        if (o.DeliveryStatus == null && o.CheckoutStatus == true)
                        {
                            ViewBag.Error = "Đơn hàng đang đóng gói không thể thay đổi trạng thái đơn hàng thành \"đã hoàn thành\"";
                            return View();
                        }
                        else
                        {
                            if (o.DeliveryStatus == false && o.CheckoutStatus == true)
                            {
                                ViewBag.Error = "Đơn hàng đang vận chuyển không thể thay đổi trạng thái đơn hàng thành \"đã hoàn thành\"";
                                return View();
                            }
                            else
                            {
                                if (o.DeliveryStatus == true && o.CheckoutStatus == null)
                                {
                                    ViewBag.Error = "Đơn hàng đã giao thì không thể thay đổi trạng thái đơn hàng thành \"đã xác nhận\"";
                                    return View();
                                }
                                else
                                {
                                    if (o.DeliveryStatus == true && o.CheckoutStatus == false)
                                    {
                                        ViewBag.Error = "Đơn hàng đã giao thì không thể thay đổi trạng thái đơn hàng thành \"đã hủy\"";
                                        return View();
                                    }
                                }
                            }
                        }
                        if (o.ModifiedBy != null)
                        {
                            o.ModifiedBy = Session["UserName"].ToString();
                        }
                        if (o.ModifiedDate == null || o.ModifiedDate != null)
                        {
                            o.ModifiedDate = DateTime.Now;
                        }
                        //nếu đơn hàng bị hủy thì tự động trả cập nhật số lượng các sản phẩm được mua lại kho
                        if (o.CheckoutStatus == false)
                        {
                            foreach (var book in ViewBag.Book)
                            {
                                foreach (var item in ViewBag.Detail)
                                {
                                    if (item.BookID == book.ID)
                                    {
                                        book.Quantity += item.Number;
                                    }
                                }
                            }
                        }
                        UpdateModel(o);
                        data.SaveChanges();

                        // nếu tình trạng giao hàng được thay đổi
                        if (o.DeliveryStatus != deliverstatus)
                        {
                            //gửi mail vào sau khi thay đổi thành công dữ liệu
                            string content_de = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/edit_o.html"));

                            content_de = content_de.Replace("{{UserName}}", o.User.Name);
                            content_de = content_de.Replace("{{OrderID}}", o.ID.ToString());
                            if (o.DeliveryStatus == true)
                            {
                                content_de = content_de.Replace("{{status}}", "đã giao thành công");
                            }
                            else
                            {
                                if (o.DeliveryStatus == false)
                                {
                                    content_de = content_de.Replace("{{status}}", "đã bắt đầu vận chuyển");
                                }
                                else
                                {
                                    content_de = content_de.Replace("{{status}}", "đang được đóng gói");
                                }
                            }

                            var toEmail_de = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                            new MailHelper().SendMail(o.User.Email, "Thay đổi đơn hàng thành công", content_de);
                            new MailHelper().SendMail(toEmail_de, "Thay đổi đơn hàng thành công", content_de);
                        }
                        else
                        {
                            if (o.CheckoutStatus != checkoutstatus)
                            {

                                //gửi mail vào sau khi thay đổi trạng thái thanh toán thành công dữ liệu
                                string content_de = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/edit_o.html"));

                                content_de = content_de.Replace("{{UserName}}", o.User.Name);
                                content_de = content_de.Replace("{{OrderID}}", o.ID.ToString());
                                if (o.CheckoutStatus == true)
                                {
                                    content_de = content_de.Replace("{{status}}", "đã hoàn thành");
                                }
                                else
                                {
                                    if (o.CheckoutStatus == false)
                                    {
                                        content_de = content_de.Replace("{{status}}", "đã hủy");
                                    }
                                    else
                                    {
                                        content_de = content_de.Replace("{{status}}", "đã xác nhận thành công");
                                    }
                                }

                                var toEmail_de = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                                new MailHelper().SendMail(o.User.Email, "Thay đổi đơn hàng thành công", content_de);
                                new MailHelper().SendMail(toEmail_de, "Thay đổi đơn hàng thành công", content_de);
                            }
                            else
                            {
                                if (o.CheckoutStatus != checkoutstatus && o.DeliveryStatus != deliverstatus)
                                {
                                    //gửi mail vào sau khi thay đổi trạng thái thanh toán thành công dữ liệu
                                    string content_de = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/edit_o.html"));

                                    content_de = content_de.Replace("{{UserName}}", o.User.Name);
                                    content_de = content_de.Replace("{{OrderID}}", o.ID.ToString());
                                    if (o.CheckoutStatus == true && o.DeliveryStatus == true)
                                    {
                                        content_de = content_de.Replace("{{status}}", "đã hoàn thành");
                                    }
                                    else
                                    {
                                        if (o.CheckoutStatus == false)
                                        {
                                            content_de = content_de.Replace("{{status}}", "đã hủy");
                                        }
                                        else
                                        {
                                            content_de = content_de.Replace("{{status}}", "đã xác nhận thành công");
                                        }
                                    }

                                    var toEmail_de = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                                    new MailHelper().SendMail(o.User.Email, "Thay đổi đơn hàng thành công", content_de);
                                    new MailHelper().SendMail(toEmail_de, "Thay đổi đơn hàng thành công", content_de);
                                }
                                else
                                {
                                    //gửi mail vào sau khi thay đổi thành công dữ liệu
                                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/edit_o_2.html"));

                                    content = content.Replace("{{UserName}}", o.User.Name);
                                    content = content.Replace("{{OrderID}}", o.ID.ToString());
                                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                                    new MailHelper().SendMail(o.User.Email, "Thay đổi đơn hàng thành công", content);
                                    new MailHelper().SendMail(toEmail, "Thay đổi đơn hàng thành công", content);
                                }
                            }
                        }
                    }
                    return RedirectToAction("Orders");
                }
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        public ActionResult EditDetail(int OrderID, int id)
        {
            //xác nhận quyền
            var t = "EDIT_ORDER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var od = data.OrderDetails.Single(n => n.BookID == id && n.OrderID == OrderID);
            if (od == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(od);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditDetail(int OrderID, int id, OrderDetail od)
        {
            try
            {
                od = data.OrderDetails.Single(n => n.BookID == id && n.OrderID == OrderID);
                var number = od.Number;
                var o = data.Orders.Single(C => C.ID == od.OrderID);
                if (od == null)
                {
                    return RedirectToAction("Er404", "loi/");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        if (od.Number <= 0)
                        {
                            ViewBag.Error("Số lượng không được bé hơn hoặc bằng không");
                            return View();
                        }
                        if (o.ModifiedBy != null)
                        {
                            o.ModifiedBy = Session["UserName"].ToString();
                        }
                        if (o.ModifiedDate == null || o.ModifiedDate != null)
                        {
                            o.ModifiedDate = DateTime.Now;
                        }
                    }
                    UpdateModel(od);
                    data.SaveChanges();

                    //gửi mail vào sau khi thay đổi thành công dữ liệu
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/update_od.html"));

                    content = content.Replace("{{UserName}}", o.User.Name);
                    content = content.Replace("{{OrderID}}", od.OrderID.ToString());
                    content = content.Replace("{{Book}}", od.Book.Name);
                    if (od.Number > number || od.Number <= number)
                    {
                        content = content.Replace("{{change}}", "số lượng thành" + od.Number.ToString() + " thành công");
                    }
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    new MailHelper().SendMail(o.User.Email, "Thay đổi đơn hàng thành công", content);
                    new MailHelper().SendMail(toEmail, "Thay đổi đơn hàng thành công", content);
                    //gửi mail vào sau khi thay đổi thành công dữ liệu end
                }
                return RedirectToAction("Edit", new { id = OrderID });
            }
            catch
            {
                return RedirectToAction("Er404", "loi/");
            }
        }

        public ActionResult DeleteDetail(int OrderID, int id)
        {
            //xác nhận quyền
            var t = "DELETE_ORDER";
            var t1 = Session["UserGroup"].ToString();
            ViewBag.Permission = data.Permissions.Single(n => n.UserGroupID == t1 && n.RoleID == t);
            var od = data.OrderDetails.Single(n => n.BookID == id && n.OrderID == OrderID);
            var o = data.Orders.Single(C => C.ID == od.OrderID);
            if (od == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            return View(od);
        }

        [HttpPost, ActionName("DeleteDetail")]
        public ActionResult ConfirmDeleteDetail(int OrderID, int id)
        {
            var od = data.OrderDetails.Single(n => n.BookID == id && n.OrderID == OrderID);
            var o = data.Orders.Single(C => C.ID == od.OrderID);
            var t = Session["UserName"].ToString();
            var count = data.OrderDetails.Where(n => n.OrderID == od.OrderID).Count();
            if (count == 1)
            {
                ViewBag.Error = "Đơn hàng phải có ít nhất một sản phẩm";
                return View();
            }
            if (od == null)
            {
                return RedirectToAction("Er404", "loi/");
            }
            if (o.ModifiedBy != null)
            {
                o.ModifiedBy = Session["UserName"].ToString();
            }
            if (o.ModifiedDate == null || o.ModifiedDate != null)
            {
                o.ModifiedDate = DateTime.Now;
            }
            data.OrderDetails.Remove(od);
            data.SaveChanges();

            //gửi mail vào sau khi thay đổi thành công dữ liệu
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Content/template/delete_od.html"));

            content = content.Replace("{{UserName}}", o.User.Name);
            content = content.Replace("{{OrderID}}", od.OrderID.ToString());
            content = content.Replace("{{Book}}", od.Book.Name);
            content = content.Replace("{{By}}", t);

            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(o.User.Email, "Thay đổi đơn hàng thành công", content);
            new MailHelper().SendMail(toEmail, "Thay đổi đơn hàng thành công", content);
            //gửi mail vào sau khi thay đổi thành công dữ liệu end

            return RedirectToAction("Edit", new { id = od.OrderID });
        }
    }
}