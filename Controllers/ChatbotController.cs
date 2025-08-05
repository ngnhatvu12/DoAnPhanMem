using DoAnPhanMem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;

namespace DoAnPhanMem.Controllers
{
    public class ChatbotController : Controller
    {
        private readonly dbSportStoreContext _context;

        public ChatbotController(dbSportStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GetResponse([FromBody] ChatMessage message)
        {
            var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
            var response = await ProcessMessage(message.Text);
            return Json(new { response });
        }

        private async Task<dynamic> ProcessMessage(string message)
        {
            message = message.ToLower().Trim();

            // Xử lý các câu hỏi phổ biến
            if (message.Contains("chào") || message.Contains("hello") || message.Contains("hi"))
            {
                return new
                {
                    text = "Xin chào! Tôi có thể giúp gì cho bạn về đồ thể thao hôm nay?",
                    type = "text",
                    quickReplies = new List<string> { "Giày thể thao", "Áo thể thao", "Quần thể thao", "Dụng cụ thể thao", "Phụ kiện thể thao" }
                };
            }
            if (message.Contains("cảm ơn") || message.Contains("thanks"))
            {
                return new
                {
                    text = "Không có gì! Nếu bạn cần thêm thông tin gì về đồ thể thao, cứ hỏi tôi nhé!",
                    type = "text",
                    quickReplies = new List<string> { "Giày thể thao", "Áo thể thao", "Quần thể thao", "Dụng cụ thể thao" }
                };
            }
            if (message.Contains("đơn hàng") || message.Contains("order") || message.Contains("đặt hàng"))
            {
                var maKhachHang = HttpContext.Session.GetString("MaKhachHang");
                if (string.IsNullOrEmpty(maKhachHang))
                {
                    return new
                    {
                        text = "Bạn cần đăng nhập để xem thông tin đơn hàng. Vui lòng đăng nhập và thử lại.",
                        type = "text",
                        quickReplies = new List<string> { "Đăng nhập ngay" }
                    };
                }

                // Kiểm tra đơn hàng hiện tại (đang xử lý, đang giao, chờ lấy)
                if (message.Contains("hiện tại") || message.Contains("đang xử lý") || message.Contains("đang giao") || message.Contains("chờ lấy"))
                {
                    return await GetCurrentOrders(maKhachHang);
                }

                // Kiểm tra đơn hàng theo ngày cụ thể
                var dateMatch = Regex.Match(message, @"(\d{1,2})\/(\d{1,2})\/(\d{4})");
                if (dateMatch.Success)
                {
                    int day = int.Parse(dateMatch.Groups[1].Value);
                    int month = int.Parse(dateMatch.Groups[2].Value);
                    int year = int.Parse(dateMatch.Groups[3].Value);
                    return await GetOrdersByDate(maKhachHang, new DateTime(year, month, day));
                }

                // Mặc định lấy 3 đơn hàng gần nhất
                return await GetRecentOrders(maKhachHang, 3);
            }
            // Xử lý tìm kiếm sản phẩm theo loại và thương hiệu
            var productMatch = Regex.Match(message,
                @"(giày|giay|áo|ao|quần|quan|phụ kiện|phu kien|dụng cụ|dung cu|balo|bình nước|găng tay|dép|dep|túi|thảm tập|bóng|vợt|đai|dây nhảy|khăn|mũ|băng đô|bộ đồ bơi|đồ tập gym)\s*" +
                @"(nike|adidas|puma|yonex|multi-purpose|grip pro|comfort slide|flex fit|high waist|quick dry|eco-friendly|pro ball|all-star|speed jump|slimming belt|adventure|dance fit|active|aqua|sun shield|sweat band|fashion swimwear|back support|graphic tee)?\s*" +
                @"(phổ biến nhất|bán chạy|tốt nhất|nổi tiếng|được yêu thích)?");

            if (productMatch.Success)
            {
                string productCategory = productMatch.Groups[1].Value; // loại sản phẩm
                string brand = productMatch.Groups[2].Value; // thương hiệu
                string popularFlag = productMatch.Groups[3].Value; // phổ biến/bán chạy...

                bool isPopular = !string.IsNullOrEmpty(popularFlag);
                string normalizedCategory = NormalizeCategory(productCategory);

                // Nếu có đề cập đến thương hiệu cụ thể
                if (!string.IsNullOrEmpty(brand))
                {
                    var products = await SearchProductsByBrandAndCategory(brand, normalizedCategory, isPopular);
                    if (products.Any())
                    {
                        return new
                        {
                            text = isPopular
                                ? $"Dưới đây là một số {productCategory} {brand} bán chạy:"
                                : $"Dưới đây là một số {productCategory} {brand}:",
                            type = "products",
                            products = products
                        };
                    }
                    return new
                    {
                        text = $"Hiện chúng tôi chưa có {productCategory} {brand} nào. Bạn muốn xem sản phẩm khác không?",
                        type = "text",
                        quickReplies = new List<string> { "Sản phẩm khác", "Giày thể thao", "Áo thể thao", "Dụng cụ thể thao" }
                    };
                }

                // Nếu chỉ đề cập đến loại sản phẩm
                var productsByCategory = isPopular
                    ? await GetPopularProductsByCategory(normalizedCategory)
                    : await GetRandomProductsByCategory(normalizedCategory);

                if (productsByCategory.Any())
                {
                    return new
                    {
                        text = isPopular
                            ? $"Dưới đây là một số {productCategory} bán chạy:"
                            : $"Dưới đây là một số {productCategory}:",
                        type = "products",
                        products = productsByCategory
                    };
                }
                return new
                {
                    text = $"Hiện chúng tôi chưa có {productCategory} nào. Bạn muốn xem sản phẩm khác không?",
                    type = "text",
                    quickReplies = new List<string> { "Sản phẩm khác", "Giày thể thao", "Áo thể thao", "Dụng cụ thể thao" }
                };
            }

            // Xử lý các trường hợp khác
            if (message.Contains("khiếu nại") || message.Contains("phàn nàn"))
            {
                return new
                {
                    text = "Xin lỗi vì sự bất tiện này. Vui lòng cho tôi biết chi tiết vấn đề bạn gặp phải, tôi sẽ hỗ trợ bạn giải quyết. Hoặc bạn có thể gọi hotline 1900.xxxx để được hỗ trợ nhanh hơn.",
                    type = "text"
                };
            }
            return new
            {
                text = "Xin lỗi, tôi chưa hiểu câu hỏi của bạn. Bạn đang tìm kiếm sản phẩm thể thao nào?",
                type = "text",
                quickReplies = new List<string> { "Giày thể thao", "Áo thể thao", "Quần thể thao", "Dụng cụ thể thao", "Phụ kiện thể thao" }
            };
        }
        private async Task<dynamic> GetCurrentOrders(string maKhachHang)
        {
            var statuses = new[] { "Đang xử lý", "Đang giao hàng", "Chờ lấy hàng" };
            var orders = await _context.DonHang
                .Where(d => d.MaKhachHang == maKhachHang && statuses.Contains(d.TrangThai))
                .OrderByDescending(d => d.NgayDat)
                .Take(5)
                .Include(d => d.ChiTietDonHang)
                .ThenInclude(c => c.ChiTietSanPham)
                .ThenInclude(c => c.SanPham)
                .ToListAsync();

            if (!orders.Any())
            {
                return new
                {
                    text = "Hiện không có đơn hàng nào đang trong quá trình xử lý.",
                    type = "text"
                };
            }

            return new
            {
                text = $"Bạn có {orders.Count} đơn hàng đang trong quá trình xử lý:",
                type = "orders",
                orders = orders.Select(o => new OrderResponse
                {
                    MaDonHang = o.MaDonHang,
                    TrangThai = o.TrangThai,
                    NgayDat = o.NgayDat,
                    TongTien = o.ChiTietDonHang.Sum(c => c.SoLuong * c.GiaBan),
                    ChiTiet = o.ChiTietDonHang.Select(c => new OrderItem
                    {
                        TenSanPham = c.ChiTietSanPham.SanPham.TenSanPham,
                        SoLuong = c.SoLuong,
                        GiaBan = c.GiaBan,
                        HinhAnh = c.ChiTietSanPham.SanPham.HinhAnh
                    }).ToList()
                }).ToList()
            };
        }

        private async Task<dynamic> GetOrdersByDate(string maKhachHang, DateTime date)
        {
            var orders = await _context.DonHang
                .Where(d => d.MaKhachHang == maKhachHang && d.NgayDat.Date == date.Date)
                .OrderByDescending(d => d.NgayDat)
                .Include(d => d.ChiTietDonHang)
                .ThenInclude(c => c.ChiTietSanPham)
                .ThenInclude(c => c.SanPham)
                .ToListAsync();

            if (!orders.Any())
            {
                return new
                {
                    text = $"Không tìm thấy đơn hàng nào vào ngày {date:dd/MM/yyyy}.",
                    type = "text"
                };
            }

            return new
            {
                text = $"Bạn có {orders.Count} đơn hàng vào ngày {date:dd/MM/yyyy}:",
                type = "orders",
                orders = orders.Select(o => new OrderResponse
                {
                    MaDonHang = o.MaDonHang,
                    TrangThai = o.TrangThai,
                    NgayDat = o.NgayDat,
                    TongTien = o.ChiTietDonHang.Sum(c => c.SoLuong * c.GiaBan),
                    ChiTiet = o.ChiTietDonHang.Select(c => new OrderItem
                    {
                        TenSanPham = c.ChiTietSanPham.SanPham.TenSanPham,
                        SoLuong = c.SoLuong,
                        GiaBan = c.GiaBan,
                        HinhAnh = c.ChiTietSanPham.SanPham.HinhAnh
                    }).ToList()
                }).ToList()
            };
        }

        private async Task<dynamic> GetRecentOrders(string maKhachHang, int count)
        {
            var orders = await _context.DonHang
                .Where(d => d.MaKhachHang == maKhachHang)
                .OrderByDescending(d => d.NgayDat)
                .Take(count)
                .Include(d => d.ChiTietDonHang)
                .ThenInclude(c => c.ChiTietSanPham)
                .ThenInclude(c => c.SanPham)
                .ToListAsync();

            if (!orders.Any())
            {
                return new
                {
                    text = "Bạn chưa có đơn hàng nào.",
                    type = "text"
                };
            }

            return new
            {
                text = $"Dưới đây là {orders.Count} đơn hàng gần nhất của bạn:",
                type = "orders",
                orders = orders.Select(o => new OrderResponse
                {
                    MaDonHang = o.MaDonHang,
                    TrangThai = o.TrangThai,
                    NgayDat = o.NgayDat,
                    TongTien = o.ChiTietDonHang.Sum(c => c.SoLuong * c.GiaBan),
                    ChiTiet = o.ChiTietDonHang.Select(c => new OrderItem
                    {
                        TenSanPham = c.ChiTietSanPham.SanPham.TenSanPham,
                        SoLuong = c.SoLuong,
                        GiaBan = c.GiaBan,
                        HinhAnh = c.ChiTietSanPham.SanPham.HinhAnh
                    }).ToList()
                }).ToList()
            };
        }
        private string NormalizeCategory(string category)
        {
            switch (category)
            {
                case "giay":
                case "giày":
                    return "giày";
                case "ao":
                case "áo":
                    return "áo";
                case "quần":
                case "quan":
                    return "quần";
                case "phụ kiện":
                case "phu kien":
                    return "phụ kiện";
                case "dụng cụ":
                case "dung cu":
                    return "dụng cụ";
                case "dép":
                case "dep":
                    return "dép";
                default:
                    return category;
            }
        }

        private async Task<List<ProductResponse>> SearchProductsByBrandAndCategory(string brand, string category, bool isPopular)
        {
            try
            {
                var query = _context.SanPham
                    .Where(p => p.TenSanPham.ToLower().Contains(brand.ToLower()) &&
                               p.TenSanPham.ToLower().Contains(category.ToLower()))
                    .Include(p => p.Loai);

                if (isPopular)
                {
                    query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<SanPham, Loai>)query.OrderByDescending(p =>
                        _context.ChiTietHoaDon
                            .Where(ct => ct.MaSanPham == p.MaSanPham)
                            .Sum(ct => ct.SoLuong));
                }
                else
                {
                    // Lấy ngẫu nhiên
                    query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<SanPham, Loai>)query.OrderBy(p => Guid.NewGuid());
                }

                var products = await query
                    .Take(3)
                    .Select(p => new ProductResponse
                    {
                        MaSanPham = p.MaSanPham,
                        TenSanPham = p.TenSanPham,
                        HinhAnh = p.HinhAnh,
                        GiaBan = p.GiaBan,
                        GiaGiam = p.GiaGiam,
                        Loai = p.Loai.TenLoai
                    })
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchProductsByBrandAndCategory: {ex.Message}");
                return new List<ProductResponse>();
            }
        }

        private async Task<List<ProductResponse>> GetPopularProductsByCategory(string category)
        {
            try
            {
                var query = _context.SanPham
                    .Where(p => p.TenSanPham.ToLower().Contains(category.ToLower()))
                    .Include(p => p.Loai)
                    .OrderByDescending(p =>
                        _context.ChiTietHoaDon
                            .Where(ct => ct.MaSanPham == p.MaSanPham)
                            .Sum(ct => ct.SoLuong))
                    .Take(3)
                    .Select(p => new ProductResponse
                    {
                        MaSanPham = p.MaSanPham,
                        TenSanPham = p.TenSanPham,
                        HinhAnh = p.HinhAnh,
                        GiaBan = p.GiaBan,
                        GiaGiam = p.GiaGiam,
                        Loai = p.Loai.TenLoai
                    });

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPopularProductsByCategory: {ex.Message}");
                return new List<ProductResponse>();
            }
        }

        private async Task<List<ProductResponse>> GetRandomProductsByCategory(string category)
        {
            try
            {
                var query = _context.SanPham
                    .Where(p => p.TenSanPham.ToLower().Contains(category.ToLower()))
                    .Include(p => p.Loai)
                    .OrderBy(p => Guid.NewGuid()) // Lấy ngẫu nhiên
                    .Take(3)
                    .Select(p => new ProductResponse
                    {
                        MaSanPham = p.MaSanPham,
                        TenSanPham = p.TenSanPham,
                        HinhAnh = p.HinhAnh,
                        GiaBan = p.GiaBan,
                        GiaGiam = p.GiaGiam,
                        Loai = p.Loai.TenLoai
                    });

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRandomProductsByCategory: {ex.Message}");
                return new List<ProductResponse>();
            }
        }
    }

    public class ChatMessage
    {
        public string Text { get; set; }
    }

    public class ProductResponse
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public decimal GiaBan { get; set; }
        public decimal GiaGiam { get; set; }
        public string Loai { get; set; }
    }
    public class OrderResponse
    {
        public string MaDonHang { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayDat { get; set; }
        public decimal TongTien { get; set; }
        public List<OrderItem> ChiTiet { get; set; }
    }

    public class OrderItem
    {
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public string HinhAnh { get; set; }
    }
}