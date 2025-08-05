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
        public IActionResult SanPhamTheoDanhMuc(string tenDanhMuc, string maDanhMuc, decimal? minPrice, decimal? maxPrice, string maLoai = null, string maMauSac = null, string maKichThuoc = null)
        {
            // Kiểm tra danh mục hợp lệ
            if (string.IsNullOrEmpty(maDanhMuc))
            {
                return NotFound("Danh mục không tồn tại.");
            }
            var danhMuc = _db.DanhMuc.Find(maDanhMuc);
            if (danhMuc == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy danh mục sản phẩm";
                return RedirectToAction("Index", "Home");
            }
            // Truy vấn cơ bản dựa vào danh mục
            var query = _db.SanPham
                .Include(sp => sp.ChiTietSanPham) // Bao gồm thông tin chi tiết sản phẩm
                .Where(sp => sp.MaDanhMuc == maDanhMuc);

            // Lọc theo giá (nếu cung cấp)
            if (minPrice.HasValue)
            {
                query = query.Where(sp => sp.GiaBan >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(sp => sp.GiaBan <= maxPrice.Value);
            }

            // Lọc theo loại sản phẩm (mặc định tất cả nếu `maLoai` là null hoặc rỗng)
            if (!string.IsNullOrEmpty(maLoai))
            {
                query = query.Where(sp => sp.MaLoai == maLoai);
            }

            // Lọc theo màu sắc (mặc định tất cả nếu `maMauSac` là null hoặc rỗng)
            if (!string.IsNullOrEmpty(maMauSac))
            {
                query = query.Where(sp => sp.ChiTietSanPham.Any(ct => ct.MaMauSac == maMauSac));
            }

            // Lọc theo kích thước (mặc định tất cả nếu `maKichThuoc` là null hoặc rỗng)
            if (!string.IsNullOrEmpty(maKichThuoc))
            {
                query = query.Where(sp => sp.ChiTietSanPham.Any(ct => ct.MaKichThuoc == maKichThuoc));
            }

            // Thực hiện truy vấn và trả kết quả
            var sanPhamTheoDanhMuc = query.ToList();
            ViewBag.TenDanhMuc = danhMuc.TenDanhMuc;
            ViewBag.DanhSachLoai = _db.Loai.ToList();
            ViewBag.DanhSachMauSac = _db.MauSac.ToList();
            ViewBag.DanhSachKichThuoc = _db.KichThuoc.ToList();
            ViewBag.MaDanhMuc = maDanhMuc; 
            return View("Index", sanPhamTheoDanhMuc);
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
