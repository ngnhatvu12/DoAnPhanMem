namespace DoAnPhanMem.ViewModels
{
    public class ProductUpdateModel
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public decimal GiaBan { get; set; }
        public decimal GiaGiam { get; set; }
        public string TenLoai { get; set; } // Thay đổi thành TenLoai thay vì MaLoai
        public string TenDanhMuc { get; set; } // Thay đổi thành TenDanhMuc thay vì MaDanhMuc
    }
}
