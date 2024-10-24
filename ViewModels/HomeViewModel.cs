namespace DoAnPhanMem.ViewModels
{
    public class HomeViewModel
    {
        public List<ChiTietSanPham> PhoBienNhat { get; set; }
        public List<ChiTietSanPham> YeuThichNhat { get; set; }
        public List<ChiTietSanPham> BanChayNhat { get; set; }
        public List<ChiTietSanPham> CoTheQuanTam { get; set; }
        public List<SanPham> WishlistItems { get; set; }
    }
}
