using Microsoft.EntityFrameworkCore;
namespace DoAnPhanMem.Models
{
    public class dbSportStoreContext : DbContext
    { 
        public dbSportStoreContext(DbContextOptions<dbSportStoreContext> options)
            : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<Admina> Admina { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<KichThuoc> KichThuoc { get; set; }
        public DbSet<MauSac> MauSac { get; set; }
        public DbSet<DanhMuc> DanhMuc { get; set; }
        public DbSet<ChiTietSanPham> ChiTietSanPham { get; set; }
        public DbSet<DonHang> DonHang { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
        public DbSet<GiamGia> GiamGia { get; set; }
        public DbSet<GiaoHang> GiaoHang { get; set; }
        public DbSet<GioHang> GioHang { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHang { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<DanhGia> DanhGia { get; set; }
        public DbSet<BaoCao> BaoCao { get; set; }
        public DbSet<ThanhToan> ThanhToan { get; set; }
        public DbSet<Loai> Loai { get; set; }
    }
}
