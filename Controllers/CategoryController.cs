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

        // Phương thức lấy danh sách sản phẩm theo danh mục
        public IActionResult SanPhamTheoDanhMuc(string maDanhMuc)
        {
            // Kiểm tra mã danh mục hợp lệ
            if (string.IsNullOrEmpty(maDanhMuc))
            {
                return NotFound("Danh mục không tồn tại.");
            }

            // Lấy danh sách chi tiết sản phẩm theo mã danh mục
            var sanPhamTheoDanhMuc = _db.ChiTietSanPham
                .Include(ctsp => ctsp.SanPham) // Liên kết với bảng Sản Phẩm
                .Where(ctsp => ctsp.MaDanhMuc == maDanhMuc)
                .ToList();

            if (sanPhamTheoDanhMuc == null || !sanPhamTheoDanhMuc.Any())
            {
                return NotFound("Không có sản phẩm nào trong danh mục này.");
            }

            // Trả về view cùng với dữ liệu sản phẩm
            return View("Index",sanPhamTheoDanhMuc);
        }
    }
}
