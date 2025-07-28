using DoAnPhanMem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
            var response = await ProcessMessage(message.Text);
            return Json(new { response });
        }

        private async Task<dynamic> ProcessMessage(string message)
        {
            message = message.ToLower();

            // Xử lý các câu hỏi phổ biến
            if (message.Contains("chào") || message.Contains("hello") || message.Contains("hi"))
            {
                return new
                {
                    text = "Xin chào! Tôi có thể giúp gì cho bạn hôm nay?",
                    type = "text"
                };
            }

            // Xử lý khi người dùng đề cập đến sản phẩm
            if (message.Contains("giày") || message.Contains("giay") ||
                message.Contains("mua") || message.Contains("tốt nhất") ||
                message.Contains("nên mua") || message.Contains("sản phẩm"))
            {
                var productType = DetectProductType(message);
                var products = await GetRecommendedProducts(productType);

                if (products.Any())
                {
                    return new
                    {
                        text = $"Dưới đây là một số sản phẩm {productType} bán chạy:",
                        type = "products",
                        products = products
                    };
                }
                return new
                {
                    text = $"Hiện chúng tôi chưa có sản phẩm {productType} nào. Bạn muốn xem sản phẩm khác không?",
                    type = "text"
                };
            }

            // Xử lý khi người dùng đề cập đến loại sản phẩm cụ thể
            var productTypes = await _context.Loai.ToListAsync();
            foreach (var type in productTypes)
            {
                if (message.Contains(type.TenLoai.ToLower()))
                {
                    var products = await GetRecommendedProducts(type.TenLoai);

                    if (products.Any())
                    {
                        return new
                        {
                            text = $"Dưới đây là một số sản phẩm {type.TenLoai} bán chạy:",
                            type = "products",
                            products = products
                        };
                    }
                    return new
                    {
                        text = $"Hiện chúng tôi chưa có sản phẩm {type.TenLoai} nào. Bạn muốn xem sản phẩm khác không?",
                        type = "text"
                    };
                }
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

            if (message.Contains("đơn hàng") || message.Contains("order"))
            {
                return new
                {
                    text = "Để kiểm tra đơn hàng, vui lòng cung cấp mã đơn hàng hoặc đăng nhập vào tài khoản của bạn. Bạn có muốn tôi hướng dẫn cách kiểm tra đơn hàng không?",
                    type = "text"
                };
            }

            if (message.Contains("cảm ơn") || message.Contains("thanks"))
            {
                return new
                {
                    text = "Không có gì! Nếu bạn cần thêm thông tin gì, cứ hỏi tôi nhé!",
                    type = "text"
                };
            }

            return new
            {
                text = "Xin lỗi, tôi chưa hiểu câu hỏi của bạn. Bạn có thể diễn đạt lại hoặc liên hệ hotline 1900.xxxx để được hỗ trợ trực tiếp.",
                type = "text"
            };
        }

        private string DetectProductType(string message)
        {
            var productTypes = _context.Loai.ToList();
            foreach (var type in productTypes)
            {
                if (message.Contains(type.TenLoai.ToLower()))
                {
                    return type.TenLoai;
                }
            }

            // Mặc định trả về giày thể thao nếu không phát hiện loại cụ thể
            return "giày thể thao";
        }

        private async Task<List<ProductResponse>> GetRecommendedProducts(string productType)
        {
            try
            {
                var query = from p in _context.SanPham
                            join l in _context.Loai on p.MaLoai equals l.MaLoai
                            where l.TenLoai == productType
                            let salesCount = (from ct in _context.ChiTietHoaDon
                                              join hd in _context.HoaDon on ct.MaHoaDon equals hd.MaHoaDon
                                              where ct.MaSanPham == p.MaSanPham && hd.TrangThai == "Đã hoàn thành"
                                              select ct.SoLuong).Sum()
                            orderby salesCount descending
                            select new ProductResponse
                            {
                                MaSanPham = p.MaSanPham,
                                TenSanPham = p.TenSanPham,
                                HinhAnh = p.HinhAnh,
                                GiaBan = p.GiaBan,
                                GiaGiam = p.GiaGiam,
                                Loai = l.TenLoai
                            };

                return await query.Take(3).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log lỗi để debug
                Console.WriteLine($"Error in GetRecommendedProducts: {ex.Message}");
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
}