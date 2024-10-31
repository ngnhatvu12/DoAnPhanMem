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



    [HttpPost]
    public IActionResult ThemYeuThich(string id)
    {
        try
        {
            // Kiểm tra nếu người dùng chưa đăng nhập
            if (HttpContext.Session.GetString("TenKhachHang") == null)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập trước khi thêm vào danh sách yêu thích." });
            }

            // Lấy tên khách hàng từ session
            var tenKhachHang = HttpContext.Session.GetString("TenKhachHang");
            var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.TenKhachHang == tenKhachHang);

            if (khachHang == null)
            {
                return Json(new { success = false, message = "Không tìm thấy khách hàng." });
            }

            // Kiểm tra nếu mã sản phẩm là hợp lệ
            var sanPham = _db.SanPham.FirstOrDefault(sp => sp.MaSanPham == id);
            if (sanPham == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại." });
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong danh sách yêu thích chưa
            var daYeuThich = _db.DanhSachYeuThich.Any(w => w.MaSanPham == id && w.MaKhachHang == khachHang.MaKhachHang);
            if (daYeuThich)
            {
                return Json(new { success = false, message = "Sản phẩm đã tồn tại trong danh sách yêu thích." });
            }

            // Tạo một đối tượng mới cho sản phẩm yêu thích
            var danhSachYeuThich = new DanhSachYeuThich
            {
                MaYeuThich = Guid.NewGuid().ToString(),
                MaKhachHang = khachHang.MaKhachHang,
                MaSanPham = id,
                NgayTao = DateTime.Now
            };

            // Thêm sản phẩm vào cơ sở dữ liệu
            _db.DanhSachYeuThich.Add(danhSachYeuThich);
            _db.SaveChanges();

            // Cập nhật lại số lượng sản phẩm yêu thích trong session
            var soLuongYeuThich = _db.DanhSachYeuThich.Count(x => x.MaKhachHang == khachHang.MaKhachHang);
            HttpContext.Session.SetInt32("SoLuongYeuThich", soLuongYeuThich);

            return Json(new
            {
                success = true,
                productName = sanPham.TenSanPham,
                productPrice = sanPham.GiaBan,
                wishlistCount = soLuongYeuThich
            });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Lỗi khi lưu vào database: " + ex.Message });
        }
    }

}
