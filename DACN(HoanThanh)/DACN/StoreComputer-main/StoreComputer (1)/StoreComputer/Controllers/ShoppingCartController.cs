using StoreComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using StoreComputer.Models;
using System.Web.DynamicData;
using System.Web.UI.WebControls.WebParts;
using PayPal.Api;
using Newtonsoft.Json.Linq;
using System.Security.Policy;

namespace StoreComputer.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        Model1 db = new Model1();
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return View(cart.items);
            }
            return View();
        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.items.Count},JsonRequestBehavior.AllowGet);
            }
            return Json(new {Count = 0},JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddToCart(int id,int soLuong)
        {
            var code = new
            {
                Success = false,
                msg = "",
                code = -1,
                Count = 0,
            };
            var db = new Model1();
            var checkHangHoa = db.HangHoa.FirstOrDefault(x => x.maHang == id);
            if(checkHangHoa != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    maHang = checkHangHoa.maHang,
                    tenHang = checkHangHoa.tenHang,
                    loaiHang = checkHangHoa.LoaiHang.tenLoai,
                    soLuong = soLuong,
                    hinhAnh = checkHangHoa.hinhAnh,

                };
                item.giaTien = (float)checkHangHoa.giaMoi;
                item.tongTien = item.soLuong * (decimal)item.giaTien;
                cart.AddToCart(item, soLuong);
                Session["Cart"] = cart;
                code = new
                {
                    Success = true,
                    msg = "Thêm sản phẩm vào giỏ hàng thành công! ",
                    code = 1,
                    Count = cart.items.Count,
                };
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { msg = "", code = - 1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                var checkHangHoa = cart.items.FirstOrDefault(x => x.maHang == id);
                if(checkHangHoa != null)
                {
                    cart.remove(id);
                    code = new { msg = "", code = 1, Count = cart.items.Count };
                }
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                cart.clearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Update(int id, int soLuong)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.capnhapSL(id,soLuong);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        public ActionResult thanhToan()
        {
            if(Session["taiKhoan"] != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                return View();
            }
            return RedirectToAction("DangNhap", "NguoiDung");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult thanhToan(DatHangViewModel req)
        {
            var code = new { Success = false, Code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    KhachHang kh = new KhachHang();
                    DatHang dh = new DatHang();
                    dh.tenKhachHang = req.tenKhachHang;
                    dh.sdtKhachHang = req.sdtKhachHang;
                    dh.diaChi = req.diaChi;
                    dh.ghiChu = req.ghiChu;
                    dh.ngayDatHang = DateTime.Now;
                    dh.tinhtrangThanhToan = "Chưa thanh toán";
                    dh.tinhtrangDonHang = "Chưa giao hàng";
                    cart.items.ForEach(x => dh.ChiTietDatHang.Add(new ChiTietDatHang
                    {
                        maHang = x.maHang,
                        soLuong = x.soLuong,
                        thanhTien = x.giaTien,
                        ngayDatHang = DateTime.Now,
                    })) ;
                    dh.tongTien = cart.items.Sum(s => (s.giaTien * s.soLuong));
                    dh.ptThanhToan = req.ptThanhToan;
                    Random rand = new Random(10000);
                    dh.maHD = rand.Next();
                    db.DatHang.Add(dh);
                    db.SaveChanges();
                    code = new { Success = true, Code = 1 };
                    return RedirectToAction("thanhToanThanhCong");
                }
            }
            return Json(code);
        }

        public ActionResult thanhToanThanhCong()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            cart.clearCart();
            return View();
        }

        public ActionResult partial_item_thanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return View(cart.items);
            }
            return PartialView();
        }
        public ActionResult FailureView()
        {
            return View();
        }
        // Paypal
        // Tạo trang thông báo mua hàng thành công
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //lấy APIContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //Người trả tiền cho phương thức thanh toán dưới dạng Paypal
                //ID người trả về khi hoàn thành thanh toán 
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Nếu ID không tồn tại
                    // Tạo thanh toán
                    // baseURI là url paypal gửi lại
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/shoppingcart/PaymentWithPayPal?";

                    var guid = Convert.ToString((new Random()).Next(100000));
                    //Createpayment cho url phê duyệt thanh toán
                    //chuyển hướng đến trang thanh toán tài khoản paypal
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    // Nếu thanh toán được thực hiện không thành công thì chúng tôi sẽ hiển thị thông báo thanh toán không thành công cho người dùng
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
            var code = new { Success = false, Code = -1 };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    string tenKH = Session["tenKhachHang"].ToString();
                    string soDienThoai = Session["soDienThoai"].ToString();
                    string diaChi = Session["diaChi"].ToString();
                    DatHang dh = new DatHang();
                    dh.tenKhachHang = tenKH;
                    dh.sdtKhachHang = soDienThoai;
                    dh.diaChi = diaChi;
                    dh.ngayDatHang = DateTime.Now;
                    dh.tinhtrangThanhToan = "Đã thanh toán";
                    dh.tinhtrangDonHang = "Chưa giao hàng";
                    dh.ptThanhToan = "Chuyển khoản";
                    cart.items.ForEach(x => dh.ChiTietDatHang.Add(new ChiTietDatHang
                    {
                        maHang = x.maHang,
                        soLuong = x.soLuong,
                        thanhTien = x.giaTien,
                        ngayDatHang = DateTime.Now,
                    }));
                    dh.tongTien = cart.items.Sum(s => (s.giaTien * s.soLuong));
                    Random rand = new Random(10000);
                    dh.maHD = rand.Next();
                    db.DatHang.Add(dh);
                    db.SaveChanges();
                    code = new { Success = true, Code = 1 };
                    return RedirectToAction("thanhToanThanhCong");
                }
            }
            return Json(code);
        }

        private PayPal.Api.Payment payment;
        // Trừ tiền vào tài khoản paypal
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        // Tính tổng tiền
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listSanPham = Session["Cart"] as ShoppingCart;
            // tạo danh sách vật phẩm
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            // Add các đối tượng vào detail
            foreach (var i in listSanPham.items)
            {
                itemList.items.Add(new Item()
                {
                    name = i.tenHang,
                    currency = "USD",
                    price = Math.Round(i.giaTien / 24275).ToString(),
                    quantity = i.soLuong.ToString(),
                    sku = i.maHang.ToString(),
                });
            }
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Định cấu hình Url chuyển hướng ở đây với đối tượng RedirectUrls
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Add tax , shipping , subtotal
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = listSanPham.Total().ToString()
            };
            var amount = new Amount()
            {
                currency = "USD",
                total = listSanPham.Total().ToString(), // Tổng của tax,shipping,subtotal
                details = details
            };
            var transactionList = new List<Transaction>();
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(),
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
            // Tạo payment sử dụng APIContext
            return this.payment.Create(apiContext);
        }

    }
}