using DoAnPhanMem.Models;
using DoAnPhanMem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // Lấy danh sách chi tiết sản phẩm và include sản phẩm tương ứng
            var lstChiTietSanPham = _db.ChiTietSanPham
                                       .Include(ctsp => ctsp.SanPham)  // Liên kết với bảng SanPham
                                       .ToList();

            // Lọc sản phẩm theo danh mục
            var sanPhamPhoBienNhat = lstChiTietSanPham
                                     .Where(ctsp => ctsp.MaDanhMuc == "DM001") // DM01 là mã danh mục Phổ biến nhất
                                     .Take(4)
                                     .ToList();

            var sanPhamYeuThichNhat = lstChiTietSanPham
                                      .Where(ctsp => ctsp.MaDanhMuc == "DM002") // DM02 là mã danh mục Được yêu thích nhất
                                      .Take(4)
                                      .ToList();

            var sanPhamBanChayNhat = lstChiTietSanPham
                                     .Where(ctsp => ctsp.MaDanhMuc == "DM003") // DM03 là mã danh mục Bán chạy nhất
                                     .Take(4)
                                     .ToList();

            var sanPhamCoTheQuanTam = lstChiTietSanPham
                                      .Where(ctsp => ctsp.MaDanhMuc == "DM004") // DM04 là mã danh mục Có thể bạn quan tâm
                                      .Take(4)
                                      .ToList();

            if (lstChiTietSanPham == null || !lstChiTietSanPham.Any())
            {
                _logger.LogWarning("Không có sản phẩm nào được tìm thấy.");
            }
            else
            {
                _logger.LogInformation($"Tìm thấy {lstChiTietSanPham.Count} sản phẩm.");
            }

            // Tạo một view model để gửi dữ liệu sang view
            var homeViewModel = new HomeViewModel
            {
                PhoBienNhat = sanPhamPhoBienNhat,
                YeuThichNhat = sanPhamYeuThichNhat,
                BanChayNhat = sanPhamBanChayNhat,
                CoTheQuanTam = sanPhamCoTheQuanTam
            };

            return View(homeViewModel);  // Gửi view model sang View
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
    }
}
