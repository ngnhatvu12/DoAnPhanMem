using Microsoft.AspNetCore.Mvc;

namespace DoAnPhanMem.Controllers
{
    public class AdminController : Controller
    {
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
            // Action này sẽ trả về view cho trang Quản lý nhân viên
            return View();
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
    }
}
