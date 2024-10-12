using DoAnPhanMem.Models;
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

            if (lstChiTietSanPham == null || !lstChiTietSanPham.Any())
            {
                _logger.LogWarning("Không có sản phẩm nào được tìm thấy.");
            }
            else
            {
                _logger.LogInformation($"Tìm thấy {lstChiTietSanPham.Count} sản phẩm.");
            }

            return View(lstChiTietSanPham);
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
