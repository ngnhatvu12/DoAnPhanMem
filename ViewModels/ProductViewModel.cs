using DoAnPhanMem.Models;

namespace DoAnPhanMem.ViewModels
{
    public class ProductViewModel
    {
        public string MaSanPham { get; set; }      // Mã sản phẩm
        public string TenSanPham { get; set; }     // Tên sản phẩm
        public string MoTa { get; set; }
        public string MaLoai { get; set; }
        public string MaDanhMuc { get; set; }
        public string HinhAnh { get; set; }        // Đường dẫn ảnh sản phẩm
        public int SoLuong { get; set; }           // Tổng số lượng tồn kho từ bảng ChiTietSanPham
        public string TinhTrang { get; set; }      // Tình trạng sản phẩm (Còn hàng / Hết hàng)
        public decimal GiaBan { get; set; }        // Giá bán sản phẩm
        public decimal GiaGiam { get; set; }
        public string TenLoai { get; set; }
        public string TenDanhMuc { get; set; }

    }
}

