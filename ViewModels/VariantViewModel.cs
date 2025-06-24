namespace DoAnPhanMem.ViewModels
{
    public class VariantViewModel
    {
        public string MaChiTietSP { get; set; }     
        public int SoLuongTon { get; set; }         
        public string MaKichThuoc { get; set; }     
        public string MaMauSac { get; set; }        
        public IFormFile HinhAnhBienThe { get; set; } 

        public string TrangThai => SoLuongTon > 0 ? "Còn hàng" : "Hết hàng";
    }
}

