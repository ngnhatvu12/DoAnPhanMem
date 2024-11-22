using DoAnPhanMem.Models;
using DoAnPhanMem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnPhanMem.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly dbSportStoreContext _db;

        public InvoiceController(dbSportStoreContext db)
        {
            _db = db;
        }
        public IActionResult Detail()
        {
            // Lấy MaKhachHang từ session
            string maKhachHang = HttpContext.Session.GetString("MaKhachHang");

            if (string.IsNullOrEmpty(maKhachHang))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
            }

            // Lấy thông tin khách hàng
            var khachHang = _db.KhachHang.FirstOrDefault(k => k.MaKhachHang == maKhachHang);
            if (khachHang == null)
            {
                return NotFound("Không tìm thấy khách hàng.");
            }

            // Lấy danh sách hóa đơn của khách hàng
            var danhSachHoaDon = _db.HoaDon
                .Where(hd => _db.DonHang
                    .Any(dh => dh.MaDonHang == hd.MaDonHang && dh.MaKhachHang == maKhachHang))
                .Select(hd => new
                {
                    hd.MaHoaDon,
                    hd.TongTien,
                    hd.NgayLap,
                    hd.TrangThai,
                    ChiTietHoaDon = _db.ChiTietHoaDon
                        .Where(ct => ct.MaHoaDon == hd.MaHoaDon)
                        .Select(ct => new
                        {
                            ct.MaSanPham,
                            ct.SoLuong,
                            ct.GiaThoiDiemMua,
                            TenSanPham = _db.SanPham
                                .Where(sp => sp.MaSanPham == ct.MaSanPham)
                                .Select(sp => sp.TenSanPham)
                                .FirstOrDefault()
                        })
                        .ToList()
                })
                .ToList();

            // Tạo model ViewModel để truyền sang View
            var viewModel = new InvoiceViewModel
            {
                TenKhachHang = khachHang.TenKhachHang,
                Email = khachHang.Email,
                SoDienThoai = khachHang.SoDienThoai,
                DiaChi = khachHang.DiaChi,
                HoaDons = danhSachHoaDon.Select(hd => new HoaDonViewModel
                {
                    MaHoaDon = hd.MaHoaDon,
                    TongTien = hd.TongTien,
                    NgayLap = hd.NgayLap,
                    TrangThai = hd.TrangThai,
                    ChiTietHoaDons = hd.ChiTietHoaDon.Select(ct => new ChiTietHoaDonViewModel
                    {
                        TenSanPham = ct.TenSanPham,
                        SoLuong = ct.SoLuong,
                        Gia = ct.GiaThoiDiemMua,
                        Tong = ct.SoLuong * ct.GiaThoiDiemMua
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
