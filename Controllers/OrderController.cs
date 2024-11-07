using DoAnPhanMem.Models;
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
        public async Task<IActionResult> Detail()
        {
            // Lấy mã khách hàng từ session
            var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
            if (string.IsNullOrEmpty(maKhachHang))
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách đơn hàng của khách hàng
            var donHangs = await _db.DonHang
                .Where(dh => dh.MaKhachHang == maKhachHang)
                .Select(dh => new
                {
                    dh.MaDonHang,
                    dh.NgayDat,
                    dh.TrangThai,
                    dh.NgayGiao,
                    dh.DiaChiGiao,
                    ChiTietDonHang = _db.ChiTietDonHang
                        .Where(ct => ct.MaDonHang == dh.MaDonHang)
                        .Select(ct => new
                        {
                            ct.MaChiTietDonHang,
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
    }
}
