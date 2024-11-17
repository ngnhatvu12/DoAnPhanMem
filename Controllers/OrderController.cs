using DoAnPhanMem.Models;
using DoAnPhanMem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnPhanMem.Controllers
{
    public class OrderController : Controller
    {
        private readonly dbSportStoreContext _db;

        public OrderController(dbSportStoreContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Detail(string status)
        {
            // Lấy mã khách hàng từ session
            var maKhachHang = HttpContext.Session.GetString("MaKhachHang");

            // Lấy danh sách đơn hàng của khách hàng
            var query = _db.DonHang.Where(dh => dh.MaKhachHang == maKhachHang);

            // Lọc theo trạng thái nếu có
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(dh => dh.TrangThai.ToLower() == status.ToLower());
            }

            var donHangs = await query
                .Select(dh => new
                {
                    dh.MaDonHang,
                    dh.NgayDat,
                    dh.TrangThai,
                    dh.NgayGiao,
                    dh.DiaChiGiao,
                    MaHoaDon = _db.HoaDon
                .Where(hd => hd.MaDonHang == dh.MaDonHang)
                .Select(hd => hd.MaHoaDon)
                .FirstOrDefault(),
                    ChiTietDonHang = _db.ChiTietDonHang
                        .Where(ct => ct.MaDonHang == dh.MaDonHang)
                        .Select(ct => new
                        {
                            ct.MaChiTietDonHang,
                            MaSanPham = ct.ChiTietSanPham.MaSanPham,
                            ct.SoLuong,
                            ct.GiaBan,
                            SanPham = ct.ChiTietSanPham.SanPham.TenSanPham,
                            HinhAnh = ct.ChiTietSanPham.HinhAnhBienThe
                        }).ToList(),
                    ThanhToan = _db.ThanhToan
                        .Where(tt => tt.MaDonHang == dh.MaDonHang)
                        .Select(tt => new
                        {
                            tt.PhuongThuc,
                            tt.TrangThai,
                            tt.TongTien
                        }).FirstOrDefault()
                }).ToListAsync();

            return View(donHangs);
        }
        [HttpPost]
        public IActionResult GhiDanhGia(DanhGiaViewModel danhGiaModel)
        {
            if (string.IsNullOrEmpty(danhGiaModel.MaHoaDon))
            {
                // Log lỗi hoặc xử lý khi MaHoaDon không được truyền vào
                return BadRequest("Mã hóa đơn không hợp lệ.");
            }
            // Tạo mã đánh giá tự động
            var maDanhGia = "DG" + DateTime.Now.Ticks.ToString();

            // Khởi tạo đối tượng đánh giá
            var danhGia = new DanhGia
            {
                MaDanhGia = maDanhGia,
                MaHoaDon = danhGiaModel.MaHoaDon,
                MaSanPham = danhGiaModel.MaSanPham,
                SoDiem = danhGiaModel.SoDiem,
                BinhLuan = danhGiaModel.BinhLuan,
                TrangThai = "Đã gửi",
                NgayDanhGia = DateTime.Now
            };
            // Thêm vào cơ sở dữ liệu và lưu
            _db.DanhGia.Add(danhGia);
            _db.SaveChanges();

            return RedirectToAction("Detail"); // Điều hướng lại chi tiết đơn hàng
        }

    }
}
