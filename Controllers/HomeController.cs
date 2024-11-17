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

            // Tạo một view model để gửi dữ liệu sang view
            var homeViewModel = new HomeViewModel
            {
                PhoBienNhat = sanPhamPhoBienNhat,
                YeuThichNhat = sanPhamYeuThichNhat,
                BanChayNhat = sanPhamBanChayNhat,
                CoTheQuanTam = sanPhamCoTheQuanTam,
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

            // Lấy danh sách đánh giá và bình luận cho sản phẩm này
            var danhGiaList = (from dg in _db.DanhGia
                               join hd in _db.HoaDon on dg.MaHoaDon equals hd.MaHoaDon
                               join dh in _db.DonHang on hd.MaDonHang equals dh.MaDonHang
                               join kh in _db.KhachHang on dh.MaKhachHang equals kh.MaKhachHang
                               where dg.MaSanPham == id
                               select new DanhGiaViewModel2
                               {
                                   TenKhachHang = kh.TenKhachHang,
                                   SoDiem = dg.SoDiem,
                                   BinhLuan = dg.BinhLuan,
                                   NgayDanhGia = dg.NgayDanhGia
                               }).ToList();

            var viewModel = new ProductDetailViewModel2
            {
                SanPham = sanPham,
                DanhGiaList = danhGiaList
            };

            return View(viewModel);
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

        [HttpGet]
        public IActionResult CheckLoginStatus()
        {
            var isLoggedIn = HttpContext.Session.GetString("VaiTro") != null;
            return Json(new { isLoggedIn });
        }
        [HttpPost]
        public JsonResult GetProductDetailId(string productId, string colorId, string sizeId)
        {
            var productDetail = _db.ChiTietSanPham
                .FirstOrDefault(pd => pd.MaSanPham == productId && pd.MaMauSac == colorId && pd.MaKichThuoc == sizeId);

            if (productDetail != null)
            {
                return Json(new { success = true, maChiTietSP = productDetail.MaChiTietSP });
            }
            else
            {
                return Json(new { success = false, message = "Không tìm thấy chi tiết sản phẩm cho lựa chọn của bạn." });
            }
        }

        [HttpPost]
        public JsonResult AddToWishlist(string maSanPham)
        {
            try
            {
                // Lấy thông tin khách hàng từ Session
                var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
                if (string.IsNullOrEmpty(maKhachHang))
                {
                    return Json(new { success = false, message = "Bạn cần đăng nhập để thêm sản phẩm vào yêu thích." });
                }

                // Kiểm tra sản phẩm đã có trong danh sách yêu thích hay chưa
                var existingWishlist = _db.DanhSachYeuThich
                                          .FirstOrDefault(x => x.MaSanPham == maSanPham && x.MaKhachHang == maKhachHang);

                if (existingWishlist != null)
                {
                    return Json(new { success = false, message = "Sản phẩm đã có trong danh sách yêu thích." });
                }

                // Tạo mới mục yêu thích
                var wishlist = new DanhSachYeuThich
                {
                    MaYeuThich = Guid.NewGuid().ToString(), // Tạo mã yêu thích duy nhất
                    MaKhachHang = maKhachHang,
                    MaSanPham = maSanPham,
                    NgayTao = DateTime.Now
                };

                _db.DanhSachYeuThich.Add(wishlist);
                _db.SaveChanges();

                return Json(new { success = true, message = "Sản phẩm đã được thêm vào yêu thích." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        public IActionResult GetWishlist()
        {
            // Lấy MaKhachHang từ Session
            var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
            if (string.IsNullOrEmpty(maKhachHang))
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để xem danh sách yêu thích." });
            }

            // Lấy danh sách sản phẩm yêu thích
            var wishlist = _db.DanhSachYeuThich
                              .Where(y => y.MaKhachHang == maKhachHang)
                              .Join(
                                  _db.SanPham,
                                  yeuThich => yeuThich.MaSanPham,
                                  sanPham => sanPham.MaSanPham,
                                  (yeuThich, sanPham) => new
                                  {
                                      sanPham.TenSanPham,
                                      sanPham.GiaBan,
                                      sanPham.HinhAnh,
                                      yeuThich.MaYeuThich
                                  })
                              .ToList();

            return Json(new { success = true, data = wishlist });
        }
        public JsonResult GetWishlistCount()
        {
            var maKhachHang = HttpContext.Session.GetString("MaKhachHang");

            if (string.IsNullOrEmpty(maKhachHang))
            {
                return Json(new { count = 0 });
            }

            var count = _db.DanhSachYeuThich.Count(y => y.MaKhachHang == maKhachHang);
            return Json(new { count });
        }
        [HttpPost]
        public JsonResult RemoveFromWishlist(string maYeuThich)
        {
            try
            {
                var item = _db.DanhSachYeuThich.FirstOrDefault(y => y.MaYeuThich == maYeuThich);
                if (item != null)
                {
                    _db.DanhSachYeuThich.Remove(item);
                    _db.SaveChanges();
                    return Json(new { success = true, message = "Xóa sản phẩm khỏi danh sách yêu thích thành công." });
                }
                return Json(new { success = false, message = "Sản phẩm không tồn tại trong danh sách yêu thích." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Đã xảy ra lỗi: {ex.Message}" });
            }
        }

    }
}
