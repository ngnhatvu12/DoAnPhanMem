namespace DoAnPhanMem.ViewModels
{
    public class OrderViewModel
    {
        public string MaDonHang { get; set; }
        public string TenKhachHang { get; set; }
        public List<OrderDetailViewModel> ChiTietDonHang { get; set; }
        public int TongSoLuong { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; } 
    }
}
