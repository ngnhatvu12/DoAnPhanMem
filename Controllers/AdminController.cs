using DoAnPhanMem.Models;
using DoAnPhanMem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnPhanMem.Controllers
{
    public class AdminController : Controller
    {
        private readonly dbSportStoreContext _db;

        public AdminController(dbSportStoreContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult QuanLyNhanVien()
        {
            ViewData["Title"] = "Quản lý nhân viên";
            // Action này sẽ trả về view cho trang Quản lý nhân viên
            return View();
        }
        public IActionResult POSBanHang()
        {
            ViewData["Title"] = "POS Bán Hàng";
            // Action này sẽ trả về view cho trang Quản lý nhân viên
            return View();
        }
        public IActionResult QuanLySanPham()
        {
            ViewData["Title"] = "Quản lý sản phẩm";
            var products = from sp in _db.SanPham
                           join dm in _db.DanhMuc on sp.MaDanhMuc equals dm.MaDanhMuc
                           select new ProductViewModel
                           {
                               MaSanPham = sp.MaSanPham,
                               TenSanPham = sp.TenSanPham,
                               HinhAnh = sp.HinhAnh,
                               GiaBan = sp.GiaBan,
                               TenDanhMuc = dm.TenDanhMuc,
                               SoLuong = _db.ChiTietSanPham
                                             .Where(ct => ct.MaSanPham == sp.MaSanPham)
                                             .Sum(ct => ct.SoLuongTon),
                               TinhTrang = _db.ChiTietSanPham
                                             .Where(ct => ct.MaSanPham == sp.MaSanPham)
                                             .Any(ct => ct.SoLuongTon > 0) ? "Còn hàng" : "Hết hàng"
                           };

            return View(products.ToList());
        }
        public IActionResult QuanLyDonHang()
        {
            ViewData["Title"] = "Quản lý đơn hàng";
            // Action này sẽ trả về view cho trang Quản lý nhân viên
            return View();
        }

        public IActionResult QuanLyNoiBo()
        {
            ViewData["Title"] = "Quản lý nội bộ";
            // Action này sẽ trả về view cho trang Quản lý nhân viên
            return View();
        }
        public IActionResult BangKeLuong()
        {
            ViewData["Title"] = "Bảng kê lương";
            // Action này sẽ trả về view cho trang Quản lý nhân viên
            return View();
        }
        public IActionResult BaoCaoDoanhThu()
        {
            ViewData["Title"] = "Báo cáo doanh thu";
            // Action này sẽ trả về view cho trang Quản lý nhân viên
            return View();
        }
        public IActionResult LichCongTac()
        {
            ViewData["Title"] = "Lịch công tác";
            // Action này sẽ trả về view cho trang Quản lý nhân viên
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductViewModel model, [FromForm] List<VariantViewModel> variants, [FromForm] IFormFile HinhAnh)
        {
            // Kiểm tra mã sản phẩm có trùng lặp
            if (_db.SanPham.Any(sp => sp.MaSanPham == model.MaSanPham))
            {
                return BadRequest(new { message = "Mã sản phẩm đã tồn tại." });
            }

            // Xử lý upload hình ảnh sản phẩm
            if (HinhAnh != null)
            {
                var fileName = Path.GetFileName(HinhAnh.FileName);
                var filePath = Path.Combine("wwwroot/LayoutOgani/img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await HinhAnh.CopyToAsync(stream);
                }

                model.HinhAnh = $"/LayoutOgani/img/{fileName}";
            }

            // Tạo đối tượng SanPham từ model và thêm vào cơ sở dữ liệu
            var sanPham = new SanPham
            {
                MaSanPham = model.MaSanPham,
                TenSanPham = model.TenSanPham,
                MoTa = model.MoTa,
                GiaBan = model.GiaBan,
                GiaGiam = model.GiaGiam,
                MaLoai = model.MaLoai,
                MaDanhMuc = model.MaDanhMuc,
                HinhAnh = model.HinhAnh
            };
            _db.SanPham.Add(sanPham);
            await _db.SaveChangesAsync();

            // Lưu các biến thể sản phẩm
            foreach (var variant in variants)
            {
                if (_db.ChiTietSanPham.Any(ct => ct.MaChiTietSP == variant.MaChiTietSP))
                {
                    continue; // Bỏ qua nếu mã chi tiết sản phẩm đã tồn tại
                }

                // Xử lý upload hình ảnh biến thể nếu có
                string variantImagePath = null;
                if (variant.HinhAnhBienThe != null)
                {
                    var variantFileName = Path.GetFileName(variant.HinhAnhBienThe.FileName);
                    var variantFilePath = Path.Combine("wwwroot/LayoutOgani/img", variantFileName);

                    using (var stream = new FileStream(variantFilePath, FileMode.Create))
                    {
                        await variant.HinhAnhBienThe.CopyToAsync(stream);
                    }
                    variantImagePath = $"/LayoutOgani/img/{variantFileName}";
                }

                var chiTietSanPham = new ChiTietSanPham
                {
                    MaChiTietSP = variant.MaChiTietSP,
                    MaSanPham = sanPham.MaSanPham,
                    SoLuongTon = variant.SoLuongTon,
                    MaKichThuoc = variant.MaKichThuoc,
                    MaMauSac = variant.MaMauSac,
                    HinhAnhBienThe = variantImagePath
                };
                _db.ChiTietSanPham.Add(chiTietSanPham);
            }
            await _db.SaveChangesAsync();

            return Ok(new { message = "Sản phẩm đã được thêm thành công." });
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.LoaiList = _db.Loai?.ToList() ?? new List<Loai>();  // Lấy danh sách các loại từ cơ sở dữ liệu
            ViewBag.DanhMucList = _db.DanhMuc.ToList();  // Lấy danh sách các danh mục từ cơ sở dữ liệu

            return View();
        }

    }
}
