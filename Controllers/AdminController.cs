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
            // Tính tổng số khách hàng với vai trò là 'KhachHang'
            var totalCustomers = _db.TaiKhoan.Count(tk => tk.VaiTro == "KhachHang");

            // Tính tổng số sản phẩm
            var totalProducts = _db.SanPham.Count();

            // Tính tổng đơn hàng trong tháng hiện tại
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var totalOrdersThisMonth = _db.DonHang
                .Count(dh =>  
                             dh.NgayDat.Month == currentMonth &&
                             dh.NgayDat.Year == currentYear);

            // Tính số lượng sản phẩm sắp hết hàng (SoLuongTon < 20)
            var lowStockProducts = _db.ChiTietSanPham
                .Count(ctsp => ctsp.SoLuongTon < 20);

            // Tạo ViewData để truyền dữ liệu qua View
            ViewData["TotalCustomers"] = totalCustomers;
            ViewData["TotalProducts"] = totalProducts;
            ViewData["TotalOrdersThisMonth"] = totalOrdersThisMonth;
            ViewData["LowStockProducts"] = lowStockProducts;

            // Lấy 5 đơn hàng gần nhất
            var recentOrders = _db.DonHang
                .OrderByDescending(dh => dh.NgayDat)
                .Take(5)
                .Select(dh => new
                {
                    MaDonHang = dh.MaDonHang,
                    TenKhachHang = _db.KhachHang
                        .Where(kh => kh.MaKhachHang == dh.MaKhachHang)
                        .Select(kh => kh.TenKhachHang)
                        .FirstOrDefault(),
                    TongTien = _db.ChiTietDonHang
                        .Where(ctdh => ctdh.MaDonHang == dh.MaDonHang)
                        .Sum(ctdh => (decimal?)(ctdh.SoLuong * ctdh.GiaBan)) ?? 0,
                    TrangThai = dh.TrangThai
                })
                .ToList();

            // Lấy 5 khách hàng mới nhất
            var recentCustomers = _db.KhachHang
                .OrderByDescending(kh => kh.MaKhachHang) // Assuming newer customers have higher IDs
                .Take(5)
                .Select(kh => new
                {
                    MaKhachHang = kh.MaKhachHang,
                    TenKhachHang = kh.TenKhachHang,
                    NgaySinh = kh.NgaySinh,
                    SoDienThoai = kh.SoDienThoai
                })
                .ToList();

            // Truyền dữ liệu vào ViewData để sử dụng trong view
            ViewData["RecentOrders"] = recentOrders;
            ViewData["RecentCustomers"] = recentCustomers;
            return View();
        }

        public IActionResult QuanLyKhachHang()
        {
            ViewData["Title"] = "Quản lý khách hàng";
            var khachHangList = (from kh in _db.KhachHang
                                 join tk in _db.TaiKhoan on kh.MaTaiKhoan equals tk.MaTaiKhoan
                                 select new KhachHangViewModel
                                 {
                                     MaKhachHang = kh.MaKhachHang,
                                     TenKhachHang = kh.TenKhachHang,
                                     TenDangNhap = tk.TenDangNhap,
                                     NgaySinh = kh.NgaySinh,
                                     DacQuyen = kh.DacQuyen,
                                     HinhAnh =  string.IsNullOrEmpty(kh.HinhAnh) ? "/LayoutOgani/img/noimg.jpg" : kh.HinhAnh
                                 }).ToList();
            return View(khachHangList);
        }

        public IActionResult ChiTietKhachHang(string maKhachHang)
        {
            // Lấy thông tin khách hàng
            var khachHang = (from kh in _db.KhachHang
                             join tk in _db.TaiKhoan on kh.MaTaiKhoan equals tk.MaTaiKhoan
                             where kh.MaKhachHang == maKhachHang
                             select new KhachHangChiTietViewModel
                             {
                                 MaKhachHang = kh.MaKhachHang,
                                 TenKhachHang = kh.TenKhachHang,
                                 TenDangNhap = tk.TenDangNhap,
                                 NgaySinh = kh.NgaySinh,
                                 DacQuyen = kh.DacQuyen,
                                 SoDienThoai = kh.SoDienThoai,
                                 Email = kh.Email,
                                 DiaChi = kh.DiaChi,
                                 HinhAnh = string.IsNullOrEmpty(kh.HinhAnh) ? "/LayoutOgani/img/noimg.jpg" : kh.HinhAnh
                             }).FirstOrDefault();

            // Lấy lịch sử mua hàng của khách hàng
            var lichSuMuaHang = (from dh in _db.DonHang
                                 join ct in _db.ChiTietDonHang on dh.MaDonHang equals ct.MaDonHang
                                 join ctsp in _db.ChiTietSanPham on ct.MaChiTietSP equals ctsp.MaChiTietSP
                                 join sp in _db.SanPham on ctsp.MaSanPham equals sp.MaSanPham
                                 where dh.MaKhachHang == maKhachHang
                                 select new LichSuMuaHangViewModel
                                 {
                                     MaDonHang = dh.MaDonHang,
                                     TenSanPham = sp.TenSanPham,
                                     SoLuong = ct.SoLuong,
                                     NgayDat = dh.NgayDat
                                 }).ToList();

            var viewModel = new KhachHangChiTietModalViewModel
            {
                KhachHang = khachHang,
                LichSuMuaHang = lichSuMuaHang
            };

            return Json(viewModel);
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

            // Lấy ngẫu nhiên 4 sản phẩm
            var randomProducts = _db.SanPham
                .OrderBy(r => Guid.NewGuid())
                .Take(4)
                .Select(sp => new
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    HinhAnh = sp.HinhAnh,
                    GiaBan = sp.GiaBan,
                    SoLuongTon = _db.ChiTietSanPham
                        .Where(ct => ct.MaSanPham == sp.MaSanPham)
                        .Sum(ct => (int?)ct.SoLuongTon) ?? 0
                })
                .ToList();

            // Lấy danh sách kích thước và màu sắc
            var kichThuocs = _db.KichThuoc.Select(kt => new { kt.MaKichThuoc, kt.TenKichThuoc }).ToList();
            var mauSacs = _db.MauSac.Select(ms => new { ms.MaMauSac, ms.TenMauSac }).ToList();

            ViewData["RandomProducts"] = randomProducts;
            ViewData["KichThuocs"] = kichThuocs;
            ViewData["MauSacs"] = mauSacs;

            return View();

        }
        [HttpGet]
        public IActionResult GetProductDetail(string maSanPham)
        {
            var productDetails = _db.ChiTietSanPham
                .Where(ct => ct.MaSanPham == maSanPham)
                .Select(ct => new
                {
                    ct.MaMauSac,
                    TenMauSac = _db.MauSac.FirstOrDefault(ms => ms.MaMauSac == ct.MaMauSac).TenMauSac,
                    ct.MaKichThuoc,
                    TenKichThuoc = _db.KichThuoc.FirstOrDefault(kt => kt.MaKichThuoc == ct.MaKichThuoc).TenKichThuoc
                })
                .ToList();

            var groupedByColor = productDetails
                .GroupBy(ct => new { ct.MaMauSac, ct.TenMauSac })
                .Select(g => new
                {
                    MaMauSac = g.Key.MaMauSac,
                    TenMauSac = g.Key.TenMauSac,
                    Sizes = g.Select(s => new { s.MaKichThuoc, s.TenKichThuoc }).ToList()
                })
                .ToList();

            return Json(groupedByColor);
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
            var orders = _db.DonHang.Select(dh => new OrderViewModel
            {
                MaDonHang = dh.MaDonHang,
                TenKhachHang = _db.KhachHang
                                  .Where(kh => kh.MaKhachHang == dh.MaKhachHang)
                                  .Select(kh => kh.TenKhachHang)
                                  .FirstOrDefault(),

                ChiTietDonHang = _db.ChiTietDonHang
                    .Where(ctdh => ctdh.MaDonHang == dh.MaDonHang)
                    .Select(ctdh => new OrderDetailViewModel
                    {
                        TenSanPham = _db.ChiTietSanPham
                                       .Where(ctsp => ctsp.MaChiTietSP == ctdh.MaChiTietSP)
                                       .Join(_db.SanPham,
                                             ctsp => ctsp.MaSanPham,
                                             sp => sp.MaSanPham,
                                             (ctsp, sp) => sp.TenSanPham)
                                       .FirstOrDefault(),

                        MauSac = _db.ChiTietSanPham
                                    .Where(ctsp => ctsp.MaChiTietSP == ctdh.MaChiTietSP)
                                    .Join(_db.MauSac,
                                          ctsp => ctsp.MaMauSac,
                                          ms => ms.MaMauSac,
                                          (ctsp, ms) => ms.TenMauSac)
                                    .FirstOrDefault(),

                        KichThuoc = _db.ChiTietSanPham
                                       .Where(ctsp => ctsp.MaChiTietSP == ctdh.MaChiTietSP)
                                       .Join(_db.KichThuoc,
                                             ctsp => ctsp.MaKichThuoc,
                                             kt => kt.MaKichThuoc,
                                             (ctsp, kt) => kt.TenKichThuoc)
                                       .FirstOrDefault(),

                        SoLuong = ctdh.SoLuong
                    }).ToList(),

                TongSoLuong = _db.ChiTietDonHang
                                 .Where(ctdh => ctdh.MaDonHang == dh.MaDonHang)
                                 .Sum(ctdh => (int?)ctdh.SoLuong) ?? 0,

                TongTien = _db.ChiTietDonHang
                              .Where(ctdh => ctdh.MaDonHang == dh.MaDonHang)
                              .Sum(ctdh => (decimal?)(ctdh.SoLuong * ctdh.GiaBan)) ?? 0,

                TrangThai = dh.TrangThai ?? "Chờ duyệt"
            }).ToList();

            return View(orders);
        }



        public IActionResult QuanLyGiaoHang()
        {
            ViewData["Title"] = "Quản lý giao hànng";
            var giaoHangList = (from gh in _db.GiaoHang
                                join dh in _db.DonHang on gh.MaDonHang equals dh.MaDonHang
                                join kh in _db.KhachHang on dh.MaKhachHang equals kh.MaKhachHang
                                select new GiaoHangViewModel
                                {
                                    MaGiaoHang = gh.MaGiaoHang,
                                    MaDonHang = gh.MaDonHang,
                                    TenKhachHang = kh.TenKhachHang,
                                    NgayGiao = gh.NgayGiao,
                                    DiaChi = kh.DiaChi,
                                    TrangThai = gh.TrangThai
                                }).ToList();

            return View(giaoHangList);
        }
        public IActionResult QuanLyGiamGia()
        {
            ViewData["Title"] = "Quản lý giảm giá";
            var giamGiaList = _db.GiamGia
                        .Select(gg => new GiamGiaViewModel
                        {
                            MaGiamGia = gg.MaGiamGia,
                            NgayTao = gg.NgayTao,
                            MucGiamGia = gg.MucGiamGia,
                            DieuKien = gg.DieuKien,
                            NgayHieuLuc = gg.NgayHieuLuc
                        }).ToList();

            return View(giamGiaList);
        }
        [HttpPost]
        public IActionResult Create(GiamGiaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var giamGia = new GiamGia
                    {
                        MaGiamGia = model.MaGiamGia,
                        NgayTao = model.NgayTao,
                        MucGiamGia = model.MucGiamGia,
                        DieuKien = model.DieuKien,
                        NgayHieuLuc = model.NgayHieuLuc
                    };

                    _db.GiamGia.Add(giamGia);
                    _db.SaveChanges();

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
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

        //XU LY CHO QUAN LY SAN PHAM
        [HttpPost]
        public async Task<IActionResult> AddProduct(
    [FromForm] ProductViewModel model,
    [FromForm] List<VariantViewModel> variants,
    [FromForm] IFormFile HinhAnh)
        {
            // Kiểm tra mã sản phẩm có trùng lặp
            if (_db.SanPham.Any(sp => sp.MaSanPham == model.MaSanPham))
            {
                return BadRequest(new { message = "Mã sản phẩm đã tồn tại." });
            }

            // Xử lý upload hình ảnh sản phẩm chính
            if (HinhAnh != null)
            {
                var fileName = Path.GetFileName(HinhAnh.FileName);
                var filePath = Path.Combine("wwwroot/LayoutOgani/img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await HinhAnh.CopyToAsync(stream);
                }

                model.HinhAnh = $"{fileName}";
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
                    variantImagePath = $"{variantFileName}";
                }

                // Đặt trạng thái dựa trên số lượng tồn (SoLuongTon)
                string trangThai = variant.SoLuongTon > 0 ? "Còn hàng" : "Hết hàng";

                // Tạo đối tượng ChiTietSanPham và thêm vào cơ sở dữ liệu
                var chiTietSanPham = new ChiTietSanPham
                {
                    MaChiTietSP = variant.MaChiTietSP,
                    MaSanPham = sanPham.MaSanPham,
                    SoLuongTon = variant.SoLuongTon,
                    MaKichThuoc = variant.MaKichThuoc,
                    MaMauSac = variant.MaMauSac,
                    TrangThai = trangThai, // Lưu trạng thái
                    HinhAnhBienThe = variantImagePath // Lưu đường dẫn ảnh biến thể
                };
                _db.ChiTietSanPham.Add(chiTietSanPham);
            }
            await _db.SaveChangesAsync();

            return Ok(new { message = "Sản phẩm đã được thêm thành công." });
        }


        [HttpGet]
        public IActionResult GetLoaiList()
        {
            var loaiList = _db.Loai.Select(loai => new { loai.MaLoai, loai.TenLoai }).ToList();
            return Json(loaiList);
        }

        [HttpGet]
        public IActionResult GetDanhMucList()
        {
            var danhMucList = _db.DanhMuc.Select(dm => new { dm.MaDanhMuc, dm.TenDanhMuc }).ToList();
            return Json(danhMucList);
        }

        [HttpGet]
        public IActionResult GetKichThuocList()
        {
            var kichThuocList = _db.KichThuoc.Select(kt => new { kt.MaKichThuoc, kt.TenKichThuoc }).ToList();
            return Json(kichThuocList);
        }

        [HttpGet]
        public IActionResult GetMauSacList()
        {
            var mauSacList = _db.MauSac.Select(ms => new { ms.MaMauSac, ms.TenMauSac }).ToList();
            return Json(mauSacList);
        }
        [HttpGet]
        public IActionResult GetProductDetails(string maSanPham)
        {
            var product = _db.SanPham
                                   .Where(p => p.MaSanPham == maSanPham)
                                   .Select(p => new {
                                       p.MaSanPham,
                                       p.TenSanPham,
                                       p.MoTa,
                                       p.GiaBan,
                                       p.GiaGiam,
                                       Loai = p.Loai.TenLoai, // Nếu có quan hệ tới bảng Loai
                                       DanhMuc = p.DanhMuc.TenDanhMuc // Nếu có quan hệ tới bảng DanhMuc
                                   })
                                   .FirstOrDefault();

            if (product == null)
            {
                return NotFound(new { message = "Sản phẩm không tồn tại" });
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct([FromBody] ProductUpdateModel model)
        {
            var product = _db.SanPham.Find(model.MaSanPham);
            if (product == null)
            {
                return NotFound(new { message = "Sản phẩm không tồn tại" });
            }

            // Tra cứu MaLoai dựa trên TenLoai
            var loai = _db.Loai.FirstOrDefault(l => l.TenLoai == model.TenLoai);
            if (loai == null)
            {
                return BadRequest(new { message = "Loại không tồn tại" });
            }

            // Tra cứu MaDanhMuc dựa trên TenDanhMuc
            var danhMuc = _db.DanhMuc.FirstOrDefault(d => d.TenDanhMuc == model.TenDanhMuc);
            if (danhMuc == null)
            {
                return BadRequest(new { message = "Danh mục không tồn tại" });
            }

            // Cập nhật thông tin sản phẩm
            product.TenSanPham = model.TenSanPham;
            product.MoTa = model.MoTa;
            product.GiaBan = model.GiaBan;
            product.GiaGiam = model.GiaGiam;
            product.MaLoai = loai.MaLoai; // Gán MaLoai từ bảng Loai
            product.MaDanhMuc = danhMuc.MaDanhMuc; // Gán MaDanhMuc từ bảng DanhMuc

            _db.SaveChanges();
            return Ok(new { message = "Cập nhật thành công!" });
        }
        [HttpPost]
        public IActionResult UpdateProductWithVariants([FromBody] ProductWithVariantsUpdateModel model)
        {
            var product = _db.SanPham.Find(model.ProductData.MaSanPham);
            if (product == null)
            {
                return NotFound(new { message = "Sản phẩm không tồn tại" });
            }

            // Cập nhật thông tin cơ bản của sản phẩm
            var loai = _db.Loai.FirstOrDefault(l => l.TenLoai == model.ProductData.TenLoai);
            var danhMuc = _db.DanhMuc.FirstOrDefault(d => d.TenDanhMuc == model.ProductData.TenDanhMuc);

            product.TenSanPham = model.ProductData.TenSanPham;
            product.MoTa = model.ProductData.MoTa;
            product.GiaBan = model.ProductData.GiaBan;
            product.GiaGiam = model.ProductData.GiaGiam;
            product.MaLoai = loai?.MaLoai;
            product.MaDanhMuc = danhMuc?.MaDanhMuc;

            // Cập nhật thông tin biến thể
            foreach (var variant in model.Variants)
            {
                var existingVariant = _db.ChiTietSanPham.Find(variant.MaChiTietSP);
                if (existingVariant != null)
                {
                    existingVariant.SoLuongTon = variant.SoLuongTon;
                    existingVariant.MaKichThuoc = _db.KichThuoc.FirstOrDefault(k => k.TenKichThuoc == variant.TenKichThuoc)?.MaKichThuoc;
                    existingVariant.MaMauSac = _db.MauSac.FirstOrDefault(m => m.TenMauSac == variant.TenMauSac)?.MaMauSac;
                }
            }

            _db.SaveChanges();
            return Ok(new { message = "Cập nhật thành công!" });
        }
        [HttpGet]
        public IActionResult GetProductVariants(string productId)
        {
            var variants = _db.ChiTietSanPham
                            .Where(ct => ct.MaSanPham == productId)
                            .Select(ct => new
                            {
                                ct.MaChiTietSP,
                                ct.SoLuongTon,
                                ct.TrangThai,
                                ct.MaKichThuoc,
                                ct.MaMauSac,
                                ct.HinhAnhBienThe
                            }).ToList();

            if (variants == null || !variants.Any())
            {
                return NotFound();
            }

            return Json(variants);
        }

        [HttpGet]
        public IActionResult GetProductReviews(string maSanPham)
        {
            // Lấy thông tin chi tiết sản phẩm
            var product = (from sp in _db.SanPham
                           join dm in _db.DanhMuc on sp.MaDanhMuc equals dm.MaDanhMuc
                           where sp.MaSanPham == maSanPham
                           select new ProductViewModel
                           {
                               MaSanPham = sp.MaSanPham,
                               TenSanPham = sp.TenSanPham,
                               MoTa = sp.MoTa,
                               GiaBan = sp.GiaBan,
                               GiaGiam = sp.GiaGiam,
                               HinhAnh = sp.HinhAnh,
                               TenDanhMuc = dm.TenDanhMuc,
                               SoLuong = _db.ChiTietSanPham
                                            .Where(ct => ct.MaSanPham == sp.MaSanPham)
                                            .Sum(ct => ct.SoLuongTon),
                               TinhTrang = _db.ChiTietSanPham
                                            .Where(ct => ct.MaSanPham == sp.MaSanPham)
                                            .Any(ct => ct.SoLuongTon > 0) ? "Còn hàng" : "Hết hàng"
                           }).FirstOrDefault();

            if (product == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại." });
            }

            // Lấy danh sách đánh giá
            var danhGia = (from dg in _db.DanhGia
                           join hd in _db.HoaDon on dg.MaHoaDon equals hd.MaHoaDon
                           join dh in _db.DonHang on hd.MaDonHang equals dh.MaDonHang
                           join kh in _db.KhachHang on dh.MaKhachHang equals kh.MaKhachHang
                           where dg.MaSanPham == maSanPham
                           select new
                           {
                               TenKhachHang = kh.TenKhachHang,
                               SoDiem = dg.SoDiem,
                               BinhLuan = dg.BinhLuan,
                               NgayDanhGia = dg.NgayDanhGia,
                               MaDanhGia = dg.MaDanhGia
                           }).ToList();

            return Json(new { success = true, product, danhGia });
        }
        [HttpPost]
        public IActionResult DeleteReview(string maDanhGia)
        {
            var review = _db.DanhGia.Find(maDanhGia);
            if (review == null)
            {
                return Json(new { success = false, message = "Đánh giá không tồn tại." });
            }

            _db.DanhGia.Remove(review);
            _db.SaveChanges();
            return Json(new { success = true, message = "Xóa đánh giá thành công." });
        }



        //XU LY CHO QUAN LY DON HANG
        public IActionResult DuyetDonHang(string maDonHang)
{
    var donHang = _db.DonHang.FirstOrDefault(dh => dh.MaDonHang == maDonHang);
    if (donHang != null)
    {
        // Cập nhật trạng thái đơn hàng
        if (donHang.TrangThai == "Đã thanh toán")
        {
            donHang.TrangThai = "Đang xử lý";
        }
        else if (donHang.TrangThai == "Đang xử lý")
        {
            donHang.TrangThai = "Chờ lấy hàng";
        }
        else if (donHang.TrangThai == "Chờ lấy hàng")
        {
            donHang.TrangThai = "Đang giao hàng";
                    string maGiaoHang = "GH" + Guid.NewGuid().ToString("N").Substring(0, 6); // Tạo mã Giao Hàng tự động
                    var giaoHang = new GiaoHang
                    {
                        MaGiaoHang = maGiaoHang,
                        MaDonHang = maDonHang,
                        NgayGiao =donHang.NgayGiao ?? DateTime.MinValue, // Nếu null, sử dụng DateTime.MinValue
                        TrangThai = "Chưa hoàn thành"
                    };
                    _db.GiaoHang.Add(giaoHang);
                }
        else if (donHang.TrangThai == "Đang giao hàng")
        {
            donHang.TrangThai = "Đã hoàn thành";
        }

        // Khi trạng thái đơn hàng là "Đã hoàn thành", thêm vào bảng HoaDon và ChiTietHoaDon
        if (donHang.TrangThai == "Đã hoàn thành")
        {
            // Tạo mã hóa đơn tự động (giả sử chúng ta có một hàm GenerateMaHoaDon để tạo mã duy nhất)
            string maHoaDon = GenerateMaHoaDon();

                    // Tính tổng tiền của đơn hàng dựa trên các chi tiết đơn hàng
                    var chiTietSanPhamList = _db.ChiTietDonHang
            .Where(ctdh => ctdh.MaDonHang == maDonHang)
            .Select(ctdh => new
            {
                SoLuong = ctdh.SoLuong,
                GiaBan = _db.ChiTietSanPham
                    .Where(ctsp => ctsp.MaChiTietSP == ctdh.MaChiTietSP)
                    .Join(_db.SanPham,
                          ctsp => ctsp.MaSanPham,
                          sp => sp.MaSanPham,
                          (ctsp, sp) => sp.GiaBan)
                    .FirstOrDefault()
            }).ToList();

                    // Bước 2: Tính tổng tiền
                    decimal tongTien = chiTietSanPhamList.Sum(item => item.SoLuong * item.GiaBan);
                    // Tạo hóa đơn mới
                    var hoaDon = new HoaDon
            {
                MaHoaDon = maHoaDon,
                MaDonHang = donHang.MaDonHang,
                TongTien = tongTien, // Lấy tổng tiền của đơn hàng đã tính ở trên
                NgayLap = DateTime.Now, // Ngày lập hóa đơn là ngày hiện tại
                TrangThai = "Đã lưu"
            };
            _db.HoaDon.Add(hoaDon);

            // Thêm chi tiết hóa đơn dựa trên các chi tiết đơn hàng
            var chiTietDonHangList = _db.ChiTietDonHang.Where(ct => ct.MaDonHang == maDonHang).ToList();
            foreach (var chiTietDonHang in chiTietDonHangList)
            {
                // Lấy mã sản phẩm từ bảng ChiTietSanPham
                var maSanPham = _db.ChiTietSanPham
                    .Where(ctsp => ctsp.MaChiTietSP == chiTietDonHang.MaChiTietSP)
                    .Select(ctsp => ctsp.MaSanPham)
                    .FirstOrDefault();

                // Lấy giá bán tại thời điểm mua từ bảng SanPham
                var giaThoiDiemMua = _db.SanPham
                    .Where(sp => sp.MaSanPham == maSanPham)
                    .Select(sp => sp.GiaBan)
                    .FirstOrDefault();

                // Tạo mã chi tiết hóa đơn tự động
                string maChiTietHoaDon = GenerateMaChiTietHoaDon();

                // Tạo chi tiết hóa đơn mới
                var chiTietHoaDon = new ChiTietHoaDon
                {
                    MaChiTietHoaDon = maChiTietHoaDon,
                    MaHoaDon = maHoaDon,
                    MaSanPham = maSanPham,
                    SoLuong = chiTietDonHang.SoLuong,
                    GiaThoiDiemMua = giaThoiDiemMua
                };
                _db.ChiTietHoaDon.Add(chiTietHoaDon);
            }
        }

        // Lưu các thay đổi vào cơ sở dữ liệu
        _db.SaveChanges();
    }

    return RedirectToAction("QuanLyDonHang");
}

// Giả sử chúng ta có các hàm để tạo mã tự động cho Hóa Đơn và Chi Tiết Hóa Đơn
private string GenerateMaHoaDon()
{
    // Mã hóa đơn tự động có thể được tạo dựa trên một quy tắc hoặc tăng dần
    return "HD" + DateTime.Now.Ticks;
}

private string GenerateMaChiTietHoaDon()
{
    return "CTHD" + DateTime.Now.Ticks;
}


        [HttpPost]
        public IActionResult HuyDonHang(string maDonHang, string lyDo)
        {
            var donHang = _db.DonHang.FirstOrDefault(dh => dh.MaDonHang == maDonHang);
            if (donHang != null)
            {
                // Cập nhật trạng thái đơn hàng
                donHang.TrangThai = "Đã hủy";

                // Tạo thông báo hủy đơn hàng
                var thongBao = new ThongBao
                {
                    MaThongBao = Guid.NewGuid().ToString().Substring(0, 10), // Tạo mã thông báo ngẫu nhiên
                    MaKhachHang = donHang.MaKhachHang,
                    MoTa = lyDo,
                    ThoiGian = DateTime.Now
                };

                _db.ThongBao.Add(thongBao);
                _db.SaveChanges();
            }
            return RedirectToAction("QuanLyDonHang");
        }

    }
}
