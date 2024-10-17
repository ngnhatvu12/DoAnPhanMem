using DoAnPhanMem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DoAnPhanMem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly dbSportStoreContext _db;

        public CategoryController(dbSportStoreContext db)
        {
            _db = db;
        }

        // Phương thức tìm kiếm sản phẩm
        [HttpGet]
        public IActionResult TimKiem(string keyword)
        {
            // Kiểm tra nếu không có từ khóa
            if (string.IsNullOrEmpty(keyword))
            {
                return View(new List<ChiTietSanPham>()); // Trả về view rỗng
            }

            // Tìm sản phẩm theo tên gần giống với từ khóa tìm kiếm
            var ketQuaTimKiem = _db.ChiTietSanPham
                .Include(ctsp => ctsp.SanPham) // Bao gồm thông tin từ bảng Sản Phẩm
                .Where(ctsp => ctsp.SanPham.TenSanPham.Contains(keyword))
                .ToList();

            // Trả về view cùng với danh sách sản phẩm tìm kiếm được
            return View(ketQuaTimKiem);
        }
    }
}
