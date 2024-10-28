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
                // Kiểm tra xem tài khoản có tồn tại trong database không
                var account = _db.TaiKhoan.FirstOrDefault(t => t.TenDangNhap == model.Username && t.MatKhau == model.Password);
                if (account != null)
                {
                    // Kiểm tra vai trò của tài khoản
                    if (account.VaiTro == "Admin")
                    {
                        // Đăng nhập với vai trò Admin
                        HttpContext.Session.SetString("VaiTro", "Admin");

                        // Điều hướng về trang Admin (ví dụ: Admin/Index)
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (account.VaiTro == "KhachHang")
                    {
                        // Tìm khách hàng có mã tài khoản tương ứng
                        var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.MaTaiKhoan == account.MaTaiKhoan);

                        if (khachHang != null)
                        {
                            // Lưu tên khách hàng vào session
                            HttpContext.Session.SetString("TenKhachHang", khachHang.TenKhachHang);

                            // Lấy số lượng sản phẩm yêu thích của khách hàng
                            var soLuongYeuThich = _db.DanhSachYeuThich.Count(w => w.MaKhachHang == khachHang.MaKhachHang);

                            // Lưu số lượng vào session
                            HttpContext.Session.SetInt32("SoLuongYeuThich", soLuongYeuThich);

                            // Lưu vai trò vào session
                            HttpContext.Session.SetString("VaiTro", "KhachHang");

                            // Điều hướng về trang Home/Index (layout của khách hàng)
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                // Nếu sai tài khoản hoặc mật khẩu, thêm thông báo lỗi
                ModelState.AddModelError(string.Empty, "Sai tên tài khoản hoặc mật khẩu.");
            }

            return View(model); // Trả về trang Login kèm thông báo lỗi nếu đăng nhập thất bại
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
