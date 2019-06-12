using WebsiteBanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;

namespace WebsiteBanSach.Controllers
{
    public class GioHangController : Controller
    {
        BookStoreDB db = new BookStoreDB();
        // GET: GioHang
        public List<ItemGioHang> LayGioHang()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int Masach, string strURL)
        {
            Book sach = db.Books.SingleOrDefault(n => n.ID == Masach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang sachCheck = lstGioHang.SingleOrDefault(n => n.ID == Masach);
            if (sachCheck != null)
            {
                if (sach.Quantity < sachCheck.Number)
                {
                    return View("ThongBao");
                }
                sachCheck.Number++;
                sachCheck.ToTal = sachCheck.Number * sachCheck.PromotionPrice;
                return Redirect(strURL);
            }
            ItemGioHang ItemGH = new ItemGioHang(Masach);

            if (sach.Quantity < ItemGH.Number)
            {
                return View("ThongBao");
            }

            lstGioHang.Add(ItemGH);
            return Redirect(strURL);
        }


        public double TinhTongSoLuong()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.Number);
        }
        public decimal TinhTongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ToTal);
        }

        public ActionResult XemGioHang()
        {
            ViewBag.ToTal = TinhTongTien();
            var laygiohang = LayGioHang();

            List<ItemGioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        public ActionResult ThanhToan()
        {
            return View();
        }
        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }
        public ActionResult TotalPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            decimal tien = TinhTongTien();
            ViewBag.TongTien = tien.ToString("N0");

            return PartialView();
        }
        [HttpGet]
        public ActionResult SuaGioHang(int Masach)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Book sach = db.Books.SingleOrDefault(n => n.ID == Masach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang sachCheck = lstGioHang.SingleOrDefault(n => n.ID == Masach);

            if (sachCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GioHang = lstGioHang;
            return View(sachCheck);
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            Book sachCheck = db.Books.Single(n => n.ID == itemGH.ID);
            if (sachCheck.Quantity < itemGH.Number)
            {
                return View("ThongBao");
            }
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang itemGHUpdate = lstGioHang.Find(n => n.ID == itemGH.ID);
            itemGHUpdate.Number = itemGH.Number;
            itemGHUpdate.ToTal = itemGHUpdate.Number * itemGHUpdate.PromotionPrice;
            return RedirectToAction("XemGioHang");
        }
        public ActionResult XoaGioHang(int Masach)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Book sach = db.Books.SingleOrDefault(n => n.ID == Masach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang sachCheck = lstGioHang.SingleOrDefault(n => n.ID == Masach);

            if (sachCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstGioHang.Remove(sachCheck);
            return RedirectToAction("XemGioHang");
        }
        public ActionResult DatHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //DocGia DG = Session["Taikhoan"] as DocGia;
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var order = new WebsiteBanSach.Models.Order();

            order.CreatedDate = DateTime.Now;
            order.DeliveryDate = DateTime.Now;
            User user = Session["Taikhoan"] as User;
            order.UserID = user.ID;
            order.ModifiedDate = DateTime.Now;
            order.ModifiedBy = user.Name;
            order.DeliveryStatus = null;
            order.CheckoutStatus = null;
            db.Orders.Add(order);
            db.SaveChanges();
            List<ItemGioHang> lstGH = LayGioHang();
            foreach (var item in lstGH)
            {
                OrderDetail ord = new OrderDetail();
                ord.OrderID = order.ID;
                ord.BookID = item.ID;
                ord.Number = item.Number;
                var bk = db.Books.Single(c => c.ID == ord.BookID);
                bk.Quantity -= ord.Number;
                ord.Price = item.Price;
                db.OrderDetails.Add(ord);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang", "GioHang");
        }
        public decimal ChuyenTienVietSangTienDo(decimal? tien)
        {
            //20000 = 0.86$ 
            //tien = ?
            double? tientemp = (double?)tien;
            decimal ketqua = (decimal)((tientemp * 0.86) / 20000);
            return ketqua;
        }
        //------------------Paypal
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/GioHang/PaymentWithPayPal?";

                    //here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {

                    // This function exectues after receving all parameters for the payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");

            }

            //on successful payment, show success page to user.
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang", "GioHang");
            //return View("SuccessView");
        }

        private Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };
            // List<ItemGioHang> lstGioHang = LayGioHang();
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            //Adding Item Details like name, currency, price etc
            foreach (var GH in lstGioHang)
            {
                itemList.items.Add(new Item()
                {
                    name = GH.Name,
                    currency = "USD",
                    price = /*GH.PromotionPrice.ToString(),*/ ChuyenTienVietSangTienDo(GH.PromotionPrice).ToString(),
                    quantity = GH.Quantity.ToString(),
                    sku = "sku"
                });
            }


            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = ChuyenTienVietSangTienDo(lstGioHang.Sum(x => x.Quantity * x.PromotionPrice)).ToString(),
            };

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)), //Generate an Invoice No
                amount = amount,
                item_list = itemList
            });


            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return payment.Create(apiContext);
        }
    }
}