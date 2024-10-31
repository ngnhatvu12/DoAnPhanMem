using DoAnPhanMem.Models;
using DoAnPhanMem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DoAnPhanMem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbSportStoreContext _db; // Thêm trường để lưu dbSportStoreContext

        public HomeController(ILogger<HomeController> logger, dbSportStoreContext db) // Nhận dbSportStoreContext qua constructor
        {
            _logger = logger;
            _db = db; // Gán dbSportStoreContext vào trường
        }

        public IActionResult Index()
        {
            // Lấy toàn bộ danh sách sản phẩm từ bảng SanPham
            var lstSanPham = _db.SanPham.ToList(); // Lấy tất cả sản phẩm

            // Lọc sản phẩm theo danh mục
            var sanPhamPhoBienNhat = lstSanPham
                                      .Where(sp => sp.MaDanhMuc == "DM001") // DM001 là mã danh mục Phổ biến nhất
                                      .Take(4)
                                      .ToList();

            var sanPhamYeuThichNhat = lstSanPham
                                       .Where(sp => sp.MaDanhMuc == "DM002") // DM002 là mã danh mục Được yêu thích nhất
                                       .Take(4)
                                       .ToList();

            var sanPhamBanChayNhat = lstSanPham
                                      .Where(sp => sp.MaDanhMuc == "DM003") // DM003 là mã danh mục Bán chạy nhất
                                      .Take(4)
                                      .ToList();

            var sanPhamCoTheQuanTam = lstSanPham
                                       .Where(sp => sp.MaDanhMuc == "DM004") // DM004 là mã danh mục Có thể bạn quan tâm
                                       .Take(4)
                                       .ToList();

            // Lấy danh sách sản phẩm yêu thích (wishlist)
            var wishlistItems = _db.DanhSachYeuThich // Giả sử bạn có bảng Wishlist
                                   .Include(w => w.SanPham) // Liên kết với bảng SanPham
                                   .Select(w => w.SanPham)
                                   .ToList();

            // Tạo một view model để gửi dữ liệu sang view
            var homeViewModel = new HomeViewModel
            {
                PhoBienNhat = sanPhamPhoBienNhat,
                YeuThichNhat = sanPhamYeuThichNhat,
                BanChayNhat = sanPhamBanChayNhat,
                CoTheQuanTam = sanPhamCoTheQuanTam,
                WishlistItems = wishlistItems // Gán sản phẩm yêu thích vào view model
            };

            return View(homeViewModel);  // Gửi view model sang View
        }

        public IActionResult ProductDetail(string id)
        {
            // Lấy chi tiết sản phẩm từ bảng SanPham
            var sanPham = _db.SanPham
                            .FirstOrDefault(sp => sp.MaSanPham == id);

            if (sanPham == null)
            {
                _logger.LogWarning("Không tìm thấy sản phẩm.");
                return NotFound();
            }

            return View(sanPham);
        }
        public IActionResult GetVariantDetails(string maSanPham, string maMauSac, string maKichThuoc)
        {
            var chiTietSanPham = _db.ChiTietSanPham
                                    .FirstOrDefault(ct => ct.MaSanPham == maSanPham &&
                                                          ct.MaMauSac == maMauSac &&
                                                          ct.MaKichThuoc == maKichThuoc);

            if (chiTietSanPham == null)
            {
                return Json(new { success = false, message = "Không tìm thấy biến thể sản phẩm." });
            }

            return Json(new
            {
                success = true,
                soLuongTon = chiTietSanPham.SoLuongTon,
                hinhAnhBienThe = chiTietSanPham.HinhAnhBienThe
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult GetProductDetails(string id)
        {
            // Lấy thông tin sản phẩm từ bảng `SanPham`
            var product = _db.SanPham.FirstOrDefault(sp => sp.MaSanPham == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
            }

            // Lấy các biến thể của sản phẩm từ bảng `ChiTietSanPham`
            var productVariants = _db.ChiTietSanPham
                                     .Where(ct => ct.MaSanPham == id)
                                     .Select(ct => new
                                     {
                                         MaMauSac = ct.MaMauSac,
                                         HinhAnhBienThe = ct.HinhAnhBienThe,
                                         MaKichThuoc = ct.MaKichThuoc
                                     })
                                     .ToList();

            // Lấy các tùy chọn màu sắc có sẵn dựa trên biến thể của sản phẩm
            var colorOptions = productVariants
                                .GroupBy(v => v.MaMauSac)
                                .Select(g => new
                                {
                                    MaMauSac = g.Key,
                                    TenMauSac = _db.MauSac.FirstOrDefault(m => m.MaMauSac == g.Key)?.TenMauSac,
                                    HinhAnhBienThe = g.First().HinhAnhBienThe
                                })
                                .ToList();

            // Lấy danh sách kích thước
            var sizeOptions = productVariants
                              .GroupBy(v => v.MaKichThuoc)
                              .Select(g => new
                              {
                                  MaKichThuoc = g.Key,
                                  TenKichThuoc = _db.KichThuoc.FirstOrDefault(k => k.MaKichThuoc == g.Key)?.TenKichThuoc
                              })
                              .ToList();

            var productDetails = new
            {
                success = true,
                tenSanPham = product.TenSanPham,
                giaBan = product.GiaBan,
                hinhAnh = product.HinhAnh,
                colorOptions = colorOptions,
                sizeOptions = sizeOptions
            };

            return Json(productDetails);
        }
        [HttpGet]
        public IActionResult GetProductQuantity(string productId, string colorId, string sizeId)
        {
            // Tìm sản phẩm theo mã, màu sắc và kích thước
            var productVariant = _db.ChiTietSanPham
                                    .FirstOrDefault(ct => ct.MaSanPham == productId &&
                                                          ct.MaMauSac == colorId &&
                                                          ct.MaKichThuoc == sizeId);

            if (productVariant == null || productVariant.SoLuongTon <= 0)
            {
                return Json(new { success = false, message = "Đã hết hàng" });
            }

            return Json(new { success = true, soLuongTon = productVariant.SoLuongTon });
        }

    }
}
