using DoAnPhanMem.Data;
using DoAnPhanMem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Text.Json;

public class CartController : Controller
{
    private readonly dbSportStoreContext _db;

    public CartController(dbSportStoreContext db)
    {
        _db = db;
    }
    [HttpGet]
    public IActionResult GetCartDetails()
    {
        string maGioHang = HttpContext.Session.GetString("MaGioHang");
        if (string.IsNullOrEmpty(maGioHang)) return Json(new { success = false });


        var cartDetails = _db.ChiTietGioHang
            .Where(ct => ct.MaGioHang == maGioHang)
            .Select(ct => new
            {
                MaSanPham = ct.ChiTietSanPham.MaSanPham,
                TenSanPham = ct.ChiTietSanPham.SanPham.TenSanPham,
                GiaBan = ct.ChiTietSanPham.SanPham.GiaBan,
                SoLuong = ct.SoLuong,
                TongTien = ct.TongTien,
                HinhAnh = string.IsNullOrEmpty(ct.ChiTietSanPham.HinhAnhBienThe) ? ct.ChiTietSanPham.SanPham.HinhAnh : ct.ChiTietSanPham.HinhAnhBienThe,
                MauSac = ct.ChiTietSanPham.MauSac.TenMauSac,
                KichThuoc = ct.ChiTietSanPham.KichThuoc.TenKichThuoc
            })
            .ToList();
        foreach (var item in cartDetails)
        {
            Console.WriteLine($"SanPham: {item.TenSanPham}, MauSac: {item.MauSac}, KichThuoc: {item.KichThuoc}, GiaBan: {item.GiaBan}");
        }


        var totalQuantity = cartDetails.Sum(item => item.SoLuong);

        return Json(new { success = true, cartItems = cartDetails, totalQuantity = totalQuantity });
    }

    public IActionResult Index(string maKhachHang)
    {
        string maGioHang = HttpContext.Session.GetString("MaGioHang");
        if (string.IsNullOrEmpty(maGioHang)) return RedirectToAction("EmptyCart");

        var cartDetails = _db.ChiTietGioHang
            .Where(ct => ct.MaGioHang == maGioHang)
            .Select(ct => new
            {
                MaSanPham = ct.ChiTietSanPham.MaSanPham,
                TenSanPham = ct.ChiTietSanPham.SanPham.TenSanPham,
                GiaBan = ct.ChiTietSanPham.SanPham.GiaBan,
                GiaGiam = ct.ChiTietSanPham.SanPham.GiaGiam, 
                SoLuong = ct.SoLuong,
                TongTien = ct.TongTien,
                HinhAnh = string.IsNullOrEmpty(ct.ChiTietSanPham.HinhAnhBienThe) ? ct.ChiTietSanPham.SanPham.HinhAnh : ct.ChiTietSanPham.HinhAnhBienThe,
                MauSac = ct.ChiTietSanPham.MauSac.TenMauSac,
                KichThuoc = ct.ChiTietSanPham.KichThuoc.TenKichThuoc
            })
            .ToList();

        decimal totalPrice = cartDetails.Sum(item => item.GiaBan * item.SoLuong);
        decimal totalDiscountPrice = cartDetails.Sum(item => item.GiaGiam * item.SoLuong);
        decimal savings = totalPrice - totalDiscountPrice;

        ViewBag.TotalQuantity = cartDetails.Sum(item => item.SoLuong);
        ViewBag.TotalPrice = totalDiscountPrice;
        ViewBag.Savings = savings;
        ViewBag.CartItems = cartDetails;

        var discountCodes = _db.GiamGia.ToList();
        ViewBag.DiscountCodes = discountCodes;

        // Lấy thông tin khách hàng từ session
        var maKhachHan = HttpContext.Session.GetString("MaKhachHang");
        var customer = _db.KhachHang.FirstOrDefault(kh => kh.MaKhachHang == maKhachHan);
        if (customer != null)
        {
            ViewBag.CustomerName = customer.TenKhachHang;
            ViewBag.CustomerEmail = customer.Email;
            ViewBag.CustomerAddress = customer.DiaChi;
            ViewBag.CustomerPhone = customer.SoDienThoai;
        }
        else
        {
            // Điều hướng khi không tìm thấy khách hàng
            return RedirectToAction("Error", "Home");
        }

        return View();
    }
    [HttpPost]
    public IActionResult ApplyDiscount(string maGiamGia)
    {
        var discount = _db.GiamGia.FirstOrDefault(g => g.MaGiamGia == maGiamGia);
        if (discount == null)
        {
            return Json(new { success = false, message = "Mã giảm giá không hợp lệ." });
        }

        // Kiểm tra điều kiện và ngày hiệu lực
        if (discount.NgayHieuLuc > DateTime.Now)
        {
            return Json(new { success = false, message = "Mã giảm giá chưa có hiệu lực." });
        }

        // Trả về mức giảm giá
        return Json(new { success = true, discountAmount = discount.MucGiamGia });
    }

    public IActionResult ConfirmPayment(string maKhachHang, string maDonHang, decimal tongTien, string phuongThuc, string trangThai)
    {
        var payment = new ThanhToan
        {
            MaThanhToan = GeneratePaymentId(), // hàm này tự định nghĩa để tạo mã thanh toán
            MaDonHang = maDonHang,
            PhuongThuc = phuongThuc,
            TongTien = tongTien,
            TrangThai = trangThai,
            NgayThanhToan = DateTime.Now
        };

        _db.ThanhToan.Add(payment);
        _db.SaveChanges();

        return Json(new { success = true });
    }
    private string GeneratePaymentId()
    {
        return "TT" + DateTime.Now.ToString("yyyyMMddHHmmss");
    }

    [HttpPost]
    public IActionResult CreateOrder(string paymentMethodId, string paymentMethod, string maGiamGia = null)
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
                if (string.IsNullOrEmpty(maKhachHang))
                {
                    return Json(new { success = false, message = "Bạn cần đăng nhập trước khi đặt hàng." });
                }

                var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.MaKhachHang == maKhachHang);
                if (khachHang == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin khách hàng." });
                }
                GiamGia discount = null;
                if (!string.IsNullOrEmpty(maGiamGia))
                {
                    discount = _db.GiamGia.FirstOrDefault(g => g.MaGiamGia == maGiamGia);
                    if (discount == null)
                    {
                        return Json(new { success = false, message = "Mã giảm giá không hợp lệ." });
                    }
                    if (discount.NgayHieuLuc < DateTime.Now)
                    {
                        return Json(new { success = false, message = "Mã giảm giá đã hết hạn." });
                    }
                }
                string maDonHang = "DH" + DateTime.Now.ToString("yyyyMMdd");
                Console.WriteLine("Mã đơn hàng được tạo: " + maDonHang);

                var donHang = new DonHang
                {
                    MaDonHang = maDonHang,
                    MaKhachHang = maKhachHang,
                    NgayDat = DateTime.Now,
                    TrangThai = "Đang xử lý",
                    NgayGiao = DateTime.Now.AddDays(2),
                    DiaChiGiao = khachHang.DiaChi
                };
                _db.DonHang.Add(donHang);
                _db.SaveChanges();
                var maGioHang = HttpContext.Session.GetString("MaGioHang");
                var cartItems = _db.ChiTietGioHang.Include(ct => ct.ChiTietSanPham).ThenInclude(ctsp => ctsp.SanPham)
                                                   .Where(ct => ct.MaGioHang == maGioHang).ToList();

                decimal tongTienDonHang = 0;
                foreach (var item in cartItems)
                {
                    if (item.ChiTietSanPham == null || item.ChiTietSanPham.SanPham == null) continue;

                    string maChiTietDonHang = "CTDH" + Guid.NewGuid().ToString("N").Substring(0, 6);
                    var chiTietDonHang = new ChiTietDonHang
                    {
                        MaChiTietDonHang = maChiTietDonHang,
                        MaDonHang = maDonHang,
                        MaChiTietSP = item.MaChiTietSP,
                        MaGiamGia = discount?.MaGiamGia,
                        SoLuong = item.SoLuong,
                        GiaBan = item.ChiTietSanPham.SanPham.GiaBan * item.SoLuong
                    };
                    if (discount != null)
                    {
                        chiTietDonHang.GiaBan -= discount.MucGiamGia;
                        if (chiTietDonHang.GiaBan < 0) chiTietDonHang.GiaBan = 0;
                    }
                    tongTienDonHang += chiTietDonHang.GiaBan;
                    _db.ChiTietDonHang.Add(chiTietDonHang);
                    var chiTietSanPham = _db.ChiTietSanPham.FirstOrDefault(ctsp => ctsp.MaChiTietSP == item.MaChiTietSP);
                    if (chiTietSanPham != null)
                    {
                        if (chiTietSanPham.SoLuongTon < item.SoLuong)
                        {
                            transaction.Rollback();
                            return Json(new { success = false, message = $"Sản phẩm {chiTietSanPham.MaChiTietSP} không đủ hàng tồn." });
                        }
                        chiTietSanPham.SoLuongTon -= item.SoLuong;
                        _db.ChiTietSanPham.Update(chiTietSanPham);
                    }
                }
                if (discount != null)
                {
                    _db.GiamGia.Remove(discount);
                }
                string maThanhToan = "TT" + DateTime.Now.Ticks.ToString().Substring(0, 8);
                var thanhToan = new ThanhToan
                {
                    MaThanhToan = maThanhToan,
                    MaDonHang = maDonHang,
                    PhuongThuc = paymentMethod,
                    TongTien = tongTienDonHang,
                    TrangThai = paymentMethod == "stripe" ? "Đang xử lý" : "Chưa thanh toán",
                    NgayThanhToan = DateTime.Now
                };
                _db.ThanhToan.Add(thanhToan);
                _db.SaveChanges();

                if (paymentMethod == "stripe" && !string.IsNullOrEmpty(paymentMethodId))
                {
                    var stripeClient = new Stripe.StripeClient("sk_test_51QHQ0IGVyUQsdJTU1qOVMdRrplEbGWsZC6fcZk9UTajsUkljyutoPhNd1uaDi8VksmDaxJc5N0F9t2j7Wp234exh00oc5Bwib3"); // Thay YOUR_SECRET_API_KEY bằng khóa bí mật của bạn
                    var service = new PaymentIntentService(stripeClient);
                    var options = new PaymentIntentCreateOptions
                    {
                        Amount = (long)(tongTienDonHang),
                        Currency = "vnd",
                        PaymentMethod = paymentMethodId,
                        Confirm = true,
                        AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                        {
                            Enabled = true,   
                            AllowRedirects = "never" 
                        }
                    };

                    try
                    {
                        var paymentIntent = service.Create(options);
                        if (paymentIntent.Status == "requires_action" && paymentIntent.NextAction.Type == "use_stripe_sdk")
                        {
                            return Json(new { success = false, requiresAction = true, paymentIntentClientSecret = paymentIntent.ClientSecret });
                        }
                        else if (paymentIntent.Status == "succeeded")
                        {
                            thanhToan.TrangThai = "Đã thanh toán";
                            donHang.TrangThai = "Đã thanh toán";
                            _db.SaveChanges();

                            transaction.Commit();
                            _db.ChiTietGioHang.RemoveRange(cartItems);
                            _db.SaveChanges();
                            return Json(new { success = true, orderId = maDonHang });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi Stripe: " + ex.Message);
                        transaction.Rollback();
                        return Json(new { success = false, message = "Lỗi thanh toán với Stripe: " + ex.Message });
                    }
                }
                else
                {
                    _db.ChiTietGioHang.RemoveRange(cartItems);
                    _db.SaveChanges();

                    transaction.Commit();
                    return Json(new { success = true, orderId = maDonHang });
                }

                transaction.Rollback();
                return Json(new { success = false, message = "Đã xảy ra lỗi không xác định trong quá trình đặt hàng." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xảy ra trong quá trình đặt hàng: " + ex.Message);
                transaction.Rollback();
                return Json(new { success = false, message = "Đã xảy ra lỗi trong quá trình tạo đơn hàng: " + ex.Message });
            }
        }
    }



    [HttpPost]
    public ActionResult AddToCart([FromBody] Dictionary<string, JsonElement> payload)
    {
        var maChiTietSP = payload.ContainsKey("maChiTietSP") ? payload["maChiTietSP"].GetString() : null;
        var soLuong = payload.ContainsKey("soLuong") ? payload["soLuong"].GetInt32() : 0;
        Console.WriteLine($"Giá trị maChiTietSP nhận được từ payload: {maChiTietSP}");
        Console.WriteLine($"Số lượng nhận được: {soLuong}");
        // Lấy mã giỏ hàng từ session
        var maGioHang = HttpContext.Session.GetString("MaGioHang");
        if (string.IsNullOrEmpty(maGioHang))
        {
            return Json(new { success = false, message = "Bạn cần đăng nhập để thêm vào giỏ hàng." });
        }

        try
        {
            var product = _db.ChiTietSanPham
                              .AsNoTracking()
                              .Include(p => p.SanPham)  
                              .FirstOrDefault(p => p.MaChiTietSP == maChiTietSP);

            if (product == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
            }

            if (product.SoLuongTon < soLuong)
            {
                return Json(new { success = false, message = "Số lượng tồn không đủ." });
            }
            var chiTietGioHang = new ChiTietGioHang
            {
                MaChiTietGioHang = GenerateUniqueCartDetailId(),
                MaGioHang = maGioHang,
                MaChiTietSP = maChiTietSP,
                SoLuong = soLuong,
                TongTien = product.SanPham.GiaBan * soLuong
                };
                _db.ChiTietGioHang.Add(chiTietGioHang);
                product.SoLuongTon -= soLuong;
                _db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private string GenerateUniqueCartDetailId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 6);
        }

}

