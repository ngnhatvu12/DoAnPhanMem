using Microsoft.AspNetCore.Mvc;
using DoAnPhanMem.Data; // Đảm bảo import đúng namespace
using System.Linq;
using DoAnPhanMem.Models;
using Microsoft.EntityFrameworkCore;

public class ProductController : Controller
{
    private readonly dbSportStoreContext _db;

    public ProductController(dbSportStoreContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetProductDetails(string id)
    {
        // Lấy thông tin sản phẩm từ bảng SanPham
        var product = _db.SanPham.FirstOrDefault(p => p.MaSanPham == id);
        if (product == null)
        {
            return NotFound();
        }

        // Lấy thông tin chi tiết sản phẩm từ bảng ChiTietSanPham
        var productDetail = _db.ChiTietSanPham.FirstOrDefault(cd => cd.MaSanPham == id);
        if (productDetail == null)
        {
            return NotFound(); // Hoặc có thể trả về một thông báo khác nếu không tìm thấy chi tiết
        }

        var productDetails = new
        {
            hinhAnh = product.HinhAnh, // Lấy hình ảnh từ bảng ChiTietSanPham
            tenSanPham = product.TenSanPham, // Tên sản phẩm từ bảng SanPham
            giaBan = product.GiaBan // Giá bán từ bảng SanPham
        };

        return Json(productDetails);
    }
    [HttpGet]
    public IActionResult GetWishlistItems()
    {
        var tenKhachHang = HttpContext.Session.GetString("TenKhachHang");
        if (string.IsNullOrEmpty(tenKhachHang))
        {
            return Json(new { success = false, message = "Bạn cần đăng nhập." });
        }

        var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.TenKhachHang == tenKhachHang);
        if (khachHang == null)
        {
            return Json(new { success = false, message = "Không tìm thấy khách hàng." });
        }

        // Sử dụng Include để nạp dữ liệu từ bảng SanPham
        var wishlistItems = _db.DanhSachYeuThich
            .Include(x => x.SanPham) // Nạp dữ liệu từ bảng SanPham
            .Where(x => x.MaKhachHang == khachHang.MaKhachHang)
            .Select(x => new
            {
                x.MaSanPham,
                Name = x.SanPham.TenSanPham ?? "Tên không xác định", // Lấy tên sản phẩm
                Price = x.SanPham.GiaBan, // Lấy giá sản phẩm
                ImageUrl = x.SanPham.HinhAnh
            }).ToList();

        return Json(wishlistItems);
    }
}
