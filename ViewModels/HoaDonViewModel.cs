namespace DoAnPhanMem.ViewModels
{
    public class HoaDonViewModel
    {
        public string MaHoaDon { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayLap { get; set; }
        public string TrangThai { get; set; }
        public List<ChiTietHoaDonViewModel> ChiTietHoaDons { get; set; }
    }
}
