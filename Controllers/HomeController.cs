using DoAnPhanMem.Models;
using DoAnPhanMem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using PagedList.Core;

namespace DoAnPhanMem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbSportStoreContext _db; 

        public HomeController(ILogger<HomeController> logger, dbSportStoreContext db) 
        {
            _logger = logger;
            _db = db; 
        }
        [HttpGet]
        public IActionResult TimKiem(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return View(new List<SanPham>());
            }
            var ketQuaTimKiem = _db.SanPham
                .Where(sp => sp.TenSanPham.Contains(keyword))
                .ToList();
            HttpContext.Response.Cookies.Append("SearchKeyword", keyword);
            return View(ketQuaTimKiem);
        }
        public IActionResult Index()
        {
            var lstSanPham = _db.SanPham.ToList();
            var sanPhamPhoBienNhat = lstSanPham
                                      .Where(sp => sp.MaDanhMuc == "DM001") 
                                      .Take(4)
                                      .ToList();
            var sanPhamYeuThichNhat = lstSanPham
                                       .Where(sp => sp.MaDanhMuc == "DM002") 
                                       .Take(4)
                                       .ToList();
            var sanPhamBanChayNhat = lstSanPham
                                      .Where(sp => sp.MaDanhMuc == "DM003") 
                                      .Take(4)
                                      .ToList();
            var sanPhamCoTheQuanTam = lstSanPham
                                       .Where(sp => sp.MaDanhMuc == "DM004") 
                                       .Take(4)
                                       .ToList();
            string searchKeyword = HttpContext.Request.Cookies["SearchKeyword"];
            var sanPhamGoiY = string.IsNullOrEmpty(searchKeyword) ? new List<SanPham>() :
                lstSanPham.Where(sp => sp.TenSanPham.Contains(searchKeyword)).Take(4).ToList();
            var homeViewModel = new HomeViewModel
            {
                PhoBienNhat = sanPhamPhoBienNhat,
                YeuThichNhat = sanPhamYeuThichNhat,
                BanChayNhat = sanPhamBanChayNhat,
                CoTheQuanTam = sanPhamCoTheQuanTam,
                GợiÝ = sanPhamGoiY
            };

            return View(homeViewModel);
        }
        private List<SanPham> GetSuggestedProducts(string keyword)
        {
            return _db.SanPham
                .Where(sp => sp.TenSanPham.Contains(keyword))
                .Take(4)
                .ToList();
        }
        public IActionResult ProductDetail(string id)
        {
            var sanPham = _db.SanPham
                            .FirstOrDefault(sp => sp.MaSanPham == id);
            if (sanPham == null)
            {
                _logger.LogWarning("Không tìm thấy sản phẩm.");
                return NotFound();
            }
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
            var chiTietSanPhamList = (from ctp in _db.ChiTietSanPham
                                      join kt in _db.KichThuoc on ctp.MaKichThuoc equals kt.MaKichThuoc
                                      join ms in _db.MauSac on ctp.MaMauSac equals ms.MaMauSac
                                      where ctp.MaSanPham == id
                                      select new ChiTietSanPhamViewModel
                                      {
                                          MaChiTietSP = ctp.MaChiTietSP,
                                          TenKichThuoc = kt.TenKichThuoc,
                                          TenMauSac = ms.TenMauSac,
                                          HinhAnhBienThe = ctp.HinhAnhBienThe
                                      }).ToList();

            var viewModel = new ProductDetailViewModel2
            {
                SanPham = sanPham,
                DanhGiaList = danhGiaList,
                ChiTietSanPhamList = chiTietSanPhamList
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
            var product = _db.SanPham.FirstOrDefault(sp => sp.MaSanPham == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
            }
            var productVariants = _db.ChiTietSanPham
                                     .Where(ct => ct.MaSanPham == id)
                                     .Select(ct => new
                                     {
                                         MaMauSac = ct.MaMauSac,
                                         HinhAnhBienThe = ct.HinhAnhBienThe,
                                         MaKichThuoc = ct.MaKichThuoc
                                     })
                                     .ToList();
            var colorOptions = productVariants
                                .GroupBy(v => v.MaMauSac)
                                .Select(g => new
                                {
                                    MaMauSac = g.Key,
                                    TenMauSac = _db.MauSac.FirstOrDefault(m => m.MaMauSac == g.Key)?.TenMauSac,
                                    HinhAnhBienThe = g.First().HinhAnhBienThe
                                })
                                .ToList();
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
                var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
                if (string.IsNullOrEmpty(maKhachHang))
                {
                    return Json(new { success = false, message = "Bạn cần đăng nhập để thêm sản phẩm vào yêu thích." });
                }
                var existingWishlist = _db.DanhSachYeuThich
                                          .FirstOrDefault(x => x.MaSanPham == maSanPham && x.MaKhachHang == maKhachHang);

                if (existingWishlist != null)
                {
                    return Json(new { success = false, message = "Sản phẩm đã có trong danh sách yêu thích." });
                }
                var wishlist = new DanhSachYeuThich
                {
                    MaYeuThich = Guid.NewGuid().ToString(), 
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
            var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
            if (string.IsNullOrEmpty(maKhachHang))
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để xem danh sách yêu thích." });
            }
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
        public IActionResult ProductType(string maLoai, string danhMuc, int? page, string sortOrder)
        {
            int pageNumber = page ?? 1;
            int pageSize = 12;

            // Xây dựng query
            IQueryable<SanPham> query = _db.SanPham.Where(p => p.MaLoai == maLoai);

            if (!string.IsNullOrEmpty(danhMuc))
            {
                query = query.Where(p => p.MaDanhMuc == danhMuc);
            }

            switch (sortOrder)
            {
                case "price_asc":
                    query = query.OrderBy(p => p.GiaBan);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.GiaBan);
                    break;
                case "name_asc":
                    query = query.OrderBy(p => p.TenSanPham);
                    break;
                default:
                    query = query.OrderBy(p => p.TenSanPham);
                    break;
            }

            // Sử dụng constructor đúng của PagedList
            var pagedProducts = new PagedList<SanPham>(query, pageNumber, pageSize);

            var productType = _db.Loai.Find(maLoai);
            if (productType == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy loại sản phẩm";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ProductTypeName = productType.TenLoai;
            ViewBag.MaLoai = maLoai;
            ViewBag.Categories = _db.DanhMuc.ToList();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PriceAscSort = "price_asc";
            ViewBag.PriceDescSort = "price_desc";
            ViewBag.NameSort = "name_asc";

            return View(pagedProducts);
        }

        public IActionResult KhuyenMai()
        {
            // Lấy thông tin khách hàng từ session
            var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
            var isVip = false;

            if (!string.IsNullOrEmpty(maKhachHang))
            {
                var khachHang = _db.KhachHang.FirstOrDefault(k => k.MaKhachHang == maKhachHang);
                isVip = khachHang?.DacQuyen == "VIP";
            }

            // Lấy tất cả mã giảm giá thông thường
            var discountCodes = _db.GiamGia.Where(g => g.LoaiGiamGia == "Thường" || g.LoaiGiamGia == null).ToList();

            // Nếu là VIP, thêm các mã giảm giá VIP
            if (isVip)
            {
                var vipDiscounts = _db.GiamGia.Where(g => g.LoaiGiamGia == "VIP").ToList();
                discountCodes.AddRange(vipDiscounts);
            }

            ViewBag.DiscountCodes = discountCodes;
            ViewBag.IsVip = isVip;
            return View();
        }
    }
}
