namespace DoAnPhanMem.ViewModels
{
    public class InvoiceViewModel
    {
        public string TenKhachHang { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public List<HoaDonViewModel> HoaDons { get; set; }
    }
}
