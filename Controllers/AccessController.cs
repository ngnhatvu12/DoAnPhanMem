using Microsoft.AspNetCore.Mvc;
using DoAnPhanMem.Models;
namespace DoAnPhanMem.Controllers
{
    public class AccessController : Controller
    {
        private readonly WebTheThaoDbContext _db;

        public AccessController(WebTheThaoDbContext db)
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
        public IActionResult Login(TaiKhoan taiKhoan)
        {
            if (HttpContext.Session.GetString("TenDangNhap") == null)
            {
                var u = _db.TaiKhoan.Where(x => x.TenDangNhap.Equals(taiKhoan.TenDangNhap) &&
                x.MatKhau.Equals(taiKhoan.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("TenDangNhap", u.TenDangNhap.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
