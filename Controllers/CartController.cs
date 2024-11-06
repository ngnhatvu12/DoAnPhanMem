using DoAnPhanMem.Data;
using DoAnPhanMem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        // Tính tổng số lượng của tất cả sản phẩm trong giỏ hàng
        var totalQuantity = cartDetails.Sum(item => item.SoLuong);

        return Json(new { success = true, cartItems = cartDetails, totalQuantity = totalQuantity });
    }
    public IActionResult Index()
    {
        string maGioHang = HttpContext.Session.GetString("MaGioHang");
        if (string.IsNullOrEmpty(maGioHang)) return RedirectToAction("EmptyCart"); // Điều hướng khi giỏ hàng rỗng

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

        var totalQuantity = cartDetails.Sum(item => item.SoLuong);
        var totalPrice = cartDetails.Sum(item => item.TongTien);

        ViewBag.TotalQuantity = totalQuantity;
        ViewBag.TotalPrice = totalPrice;
        ViewBag.CartItems = cartDetails;

        // Lấy mã giảm giá từ bảng GiamGia
        var discountCodes = _db.GiamGia.ToList();
        ViewBag.DiscountCodes = discountCodes;

        return View();
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
    public IActionResult CreateOrder()
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
                // Kiểm tra và lấy mã khách hàng từ Session
                var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
                if (string.IsNullOrEmpty(maKhachHang))
                {
                    Console.WriteLine("Khách hàng chưa đăng nhập hoặc session đã hết hạn.");
                    return Json(new { success = false, message = "Bạn cần đăng nhập trước khi đặt hàng." });
                }

                // Lấy thông tin khách hàng từ cơ sở dữ liệu
                var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.MaKhachHang == maKhachHang);
                if (khachHang == null)
                {
                    Console.WriteLine("Không tìm thấy thông tin khách hàng trong database.");
                    return Json(new { success = false, message = "Không tìm thấy thông tin khách hàng." });
                }

            // Tạo mã đơn hàng mới
            string maDonHang = "DH" + DateTime.Now.ToString("yyyyMMdd");

            // Tạo và lưu đơn hàng mới
            var donHang = new DonHang
                {
                    MaDonHang = maDonHang,
                    MaKhachHang = maKhachHang,
                    NgayDat = DateTime.Now,
                    TrangThai = "Đang giao",
                    NgayGiao = DateTime.Now.AddDays(2),
                    DiaChiGiao = khachHang.DiaChi
                };
                _db.DonHang.Add(donHang);
                _db.SaveChanges();

                // Lấy thông tin giỏ hàng
                var maGioHang = HttpContext.Session.GetString("MaGioHang");
            var cartItems = _db.ChiTietGioHang
.Include(ct => ct.ChiTietSanPham)
    .ThenInclude(ctsp => ctsp.SanPham)
.Where(ct => ct.MaGioHang == maGioHang)
.ToList();


            decimal tongTienDonHang = 0;

            // Thêm chi tiết đơn hàng cho mỗi mục trong giỏ hàng
            foreach (var item in cartItems)
            {
                // Kiểm tra xem `ChiTietSanPham` và `SanPham` đã được load thành công chưa
                if (item.ChiTietSanPham == null || item.ChiTietSanPham.SanPham == null)
                {
                    Console.WriteLine("ChiTietSanPham hoặc SanPham null cho mã giỏ hàng: " + item.MaGioHang);
                    continue; // Bỏ qua nếu không thể load dữ liệu sản phẩm
                }

                // Tạo mã chi tiết đơn hàng
                string maChiTietDonHang = "CTDH" + Guid.NewGuid().ToString("N").Substring(0, 6);

                // Khởi tạo `ChiTietDonHang` mới
                var chiTietDonHang = new ChiTietDonHang
                {
                    MaChiTietDonHang = maChiTietDonHang,
                    MaDonHang = maDonHang, // Mã đơn hàng vừa tạo
                    MaChiTietSP = item.MaChiTietSP,
                    MaGiamGia = "GG003", // Giả định mã giảm giá 20%
                    SoLuong = item.SoLuong,
                    GiaBan = item.ChiTietSanPham.SanPham.GiaBan * item.SoLuong
                };

                // Thêm `ChiTietDonHang` vào `DbContext`
                _db.ChiTietDonHang.Add(chiTietDonHang);
            }

            // Gọi `SaveChanges` để lưu tất cả thay đổi vào cơ sở dữ liệu
            _db.SaveChanges();



            // Tạo mã thanh toán và thêm vào bảng thanh toán
            string maThanhToan = "TT" + DateTime.Now.Ticks.ToString().Substring(0, 8);
                var thanhToan = new ThanhToan
                {
                    MaThanhToan = maThanhToan,
                    MaDonHang = maDonHang,
                    PhuongThuc = "Thanh toán khi nhận hàng",
                    TongTien = tongTienDonHang,
                    TrangThai = "Đã thanh toán",
                    NgayThanhToan = DateTime.Now
                };
                _db.ThanhToan.Add(thanhToan);
                _db.SaveChanges();

                _db.ChiTietGioHang.RemoveRange(cartItems);
               _db.SaveChanges();


            transaction.Commit();

                Console.WriteLine("Đơn hàng đã được tạo thành công với mã: " + maDonHang);
                return Json(new { success = true, orderId = maDonHang });
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

            // Kiểm tra tồn kho của sản phẩm
            var product = _db.ChiTietSanPham
                              .AsNoTracking()
                              .Include(p => p.SanPham)  // Lấy thông tin sản phẩm
                              .FirstOrDefault(p => p.MaChiTietSP == maChiTietSP);

            if (product == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
            }

            if (product.SoLuongTon < soLuong)
            {
                return Json(new { success = false, message = "Số lượng tồn không đủ." });
            }

            // Tạo mới chi tiết giỏ hàng
            var chiTietGioHang = new ChiTietGioHang
            {
                MaChiTietGioHang = GenerateUniqueCartDetailId(),
                MaGioHang = maGioHang,
                MaChiTietSP = maChiTietSP,
                SoLuong = soLuong,
                TongTien = product.SanPham.GiaBan * soLuong
                };

                // Thêm chi tiết giỏ hàng vào DbContext và cập nhật tồn kho
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

