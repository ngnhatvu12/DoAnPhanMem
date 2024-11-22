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
                            HttpContext.Session.SetString("MaTaiKhoan", khachHang.MaTaiKhoan);
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

        [HttpGet]
        public IActionResult Profile()
        {
            // Lấy MaTaiKhoan từ session
            string maTaiKhoan = HttpContext.Session.GetString("MaTaiKhoan");
            // Lấy thông tin khách hàng dựa trên MaTaiKhoan
            var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.MaTaiKhoan == maTaiKhoan);

            if (khachHang == null)
            {
                return NotFound("Không tìm thấy thông tin khách hàng.");
            }

            // Nếu ảnh trống, sử dụng ảnh mặc định
            if (string.IsNullOrEmpty(khachHang.HinhAnh))
            {
                khachHang.HinhAnh = "/LayoutOgani/img/noimg.jpg";
            }

            return View(khachHang); // Truyền thông tin khách hàng sang View
        }

        [HttpPost]
        public IActionResult Profile(KhachHang model, IFormFile avatarUpload)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Trả lại View nếu dữ liệu không hợp lệ
            }

            var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.MaKhachHang == model.MaKhachHang);
            if (khachHang == null)
            {
                return NotFound("Không tìm thấy thông tin khách hàng.");
            }

            // Xử lý cập nhật hình ảnh
            if (avatarUpload != null)
            {
                // Đường dẫn thư mục lưu ảnh
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/LayoutOgani/img");
                // Đường dẫn ảnh cũ
                string oldImagePath = Path.Combine(uploadPath, Path.GetFileName(khachHang.HinhAnh));

                // Xóa ảnh cũ nếu tồn tại
                if (!string.IsNullOrEmpty(khachHang.HinhAnh) && System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                // Lưu ảnh mới
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarUpload.FileName);
                string newFilePath = Path.Combine(uploadPath, newFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    avatarUpload.CopyTo(stream);
                }

                // Cập nhật đường dẫn ảnh vào DB
                khachHang.HinhAnh = "/LayoutOgani/img/" + newFileName;
            }
            else if (string.IsNullOrEmpty(khachHang.HinhAnh))
            {
                // Nếu không có ảnh, sử dụng ảnh mặc định
                khachHang.HinhAnh = "/LayoutOgani/img/noimg.jpg";
            }

            // Cập nhật các thông tin khác
            khachHang.TenKhachHang = model.TenKhachHang;
            khachHang.Email = model.Email;
            khachHang.SoDienThoai = model.SoDienThoai;
            khachHang.NgaySinh = model.NgaySinh;
            khachHang.DiaChi = model.DiaChi;
            khachHang.DacQuyen = model.DacQuyen;

            _db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            ViewBag.Message = "Cập nhật thông tin thành công!";
            return View(khachHang);
        }
        [HttpGet]
        public IActionResult Notification()
        {
            // Lấy MaKhachHang từ session
            string maKhachHang = HttpContext.Session.GetString("MaKhachHang");
            // Lấy danh sách thông báo của khách hàng
            var thongBaos = _db.ThongBao
                                .Where(tb => tb.MaKhachHang == maKhachHang)
                                .OrderByDescending(tb => tb.ThoiGian)
                                .ToList();

            return View(thongBaos); // Truyền danh sách thông báo sang view
        }
        [HttpPost]
        public IActionResult MarkAsRead(string maThongBao)
        {
            var thongBao = _db.ThongBao.FirstOrDefault(tb => tb.MaThongBao == maThongBao);
            if (thongBao != null)
            {
                thongBao.TrangThai = 1; // Đánh dấu là đã đọc
                _db.SaveChanges();
            }

            return Json(new { success = true });
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
