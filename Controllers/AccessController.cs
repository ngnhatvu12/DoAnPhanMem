using Microsoft.AspNetCore.Mvc;
using DoAnPhanMem.Models;
using Microsoft.EntityFrameworkCore;
using DoAnPhanMem.ViewModels;
namespace DoAnPhanMem.Controllers
{
    public class AccessController : Controller
    {
        private readonly dbSportStoreContext _db;

        public AccessController(dbSportStoreContext db)
        {
            _db = db;
        }
        [HttpGet]        
        public IActionResult Login()
        {
                return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra tài khoản trong bảng TaiKhoan
                var user = _db.TaiKhoan.SingleOrDefault(t => t.TenDangNhap == model.Username && t.MatKhau == model.Password);

                if (user != null)
                {
                    // Lưu thông tin đăng nhập vào session
                    HttpContext.Session.SetString("MaTaiKhoan", user.MaTaiKhoan.ToString());
                    HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);

                    // Kiểm tra vai trò của người dùng
                    if (user.VaiTro == "KhachHang")
                    {
                        // Lấy thông tin khách hàng dựa vào MaTaiKhoan
                        var khachHang = _db.KhachHang.SingleOrDefault(kh => kh.MaTaiKhoan == user.MaTaiKhoan);

                        if (khachHang != null)
                        {
                            // Lưu tên khách hàng vào session
                            HttpContext.Session.SetString("TenKhachHang", khachHang.TenKhachHang);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else if (user.VaiTro == "QuanLy")
                    {
                        // Điều hướng đến trang quản lý
                        return RedirectToAction("Index", "QuanLy");
                    }
                }
                else
                {
                    // Tài khoản hoặc mật khẩu sai, hiển thị thông báo lỗi
                    ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu của bạn sai.");
                }
            }

            return View(model);
        }

        // GET: Logout
        public IActionResult Logout()
        {
            // Xoá session khi người dùng đăng xuất
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Access");
        }
    }

}
