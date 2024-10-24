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
        [HttpPost]
        public IActionResult ThemYeuThich(string id)
        {
            if (HttpContext.Session.GetString("TenKhachHang") == null)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập trước khi thêm vào danh sách yêu thích." });
            }

            var tenKhachHang = HttpContext.Session.GetString("TenKhachHang");
            var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.TenKhachHang == tenKhachHang);

            if (khachHang != null)
            {
                var daYeuThich = _db.DanhSachYeuThich.Any(w => w.MaSanPham == id && w.MaKhachHang == khachHang.MaKhachHang);

                if (!daYeuThich)
                {
                    var danhSachYeuThich = new DanhSachYeuThich
                    {
                        MaKhachHang = khachHang.MaKhachHang,
                        MaSanPham = id
                    };
                    _db.DanhSachYeuThich.Add(danhSachYeuThich);
                    _db.SaveChanges();

                    // Lấy thông tin sản phẩm để hiển thị trong modal
                    var sanPham = _db.SanPham.FirstOrDefault(sp => sp.MaSanPham == id);

                    if (sanPham != null)
                    {
                        return Json(new
                        {
                            success = true,
                            productName = sanPham.TenSanPham,  // Tên sản phẩm
                            productPrice = sanPham.GiaBan  // Giá sản phẩm
                        });
                    }
                }
            }

            return Json(new { success = false, message = "Có lỗi xảy ra." });
        }

    }
}
