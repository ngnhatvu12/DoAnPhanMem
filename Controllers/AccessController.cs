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
                var account = _db.TaiKhoan.FirstOrDefault(t => t.TenDangNhap == model.Username && t.MatKhau == model.Password);
                if (account != null)
                {
                    string role = account.VaiTro;
                    HttpContext.Session.SetString("VaiTro", role);

                    if (role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (role == "KhachHang")
                    {
                        var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.MaTaiKhoan == account.MaTaiKhoan);
                        if (khachHang != null)
                        {
                            HttpContext.Session.SetString("TenKhachHang", khachHang.TenKhachHang);
                            HttpContext.Session.SetString("MaKhachHang", khachHang.MaKhachHang);
                            Console.WriteLine("MaKhachHang từ Session: " + HttpContext.Session.GetString("MaKhachHang"));
                            var gioHang = _db.GioHang.FirstOrDefault(gh => gh.MaTaiKhoan == account.MaTaiKhoan && gh.TrangThai == "Chưa thanh toán");

                            if (gioHang == null)
                            {
                                gioHang = new GioHang
                                {
                                    MaGioHang = Guid.NewGuid().ToString("N").Substring(0, 10),
                                    MaTaiKhoan = account.MaTaiKhoan,
                                    NgayTao = DateTime.Now,
                                    TrangThai = "Chưa thanh toán"
                                };

                                _db.GioHang.Add(gioHang);
                                _db.SaveChanges();
                            }
                            else
                            {
                                HttpContext.Session.SetString("MaGioHang", gioHang.MaGioHang);
                            }
                            var soLuongYeuThich = _db.DanhSachYeuThich.Count(w => w.MaKhachHang == khachHang.MaKhachHang);
                            HttpContext.Session.SetInt32("SoLuongYeuThich", soLuongYeuThich);

                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Sai tên tài khoản hoặc mật khẩu.");
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
