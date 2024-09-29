using Microsoft.EntityFrameworkCore;
namespace DoAnPhanMem.Models
{
    public class WebTheThaoDbContext : DbContext
    {
        public WebTheThaoDbContext(DbContextOptions<WebTheThaoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<ADMINA> Admins { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<KichThuoc> KichThuocs { get; set; }
        public DbSet<MauSac> MauSacs { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<GiamGia> GiamGias { get; set; }
        public DbSet<GiaoHang> GiaoHangs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<BaoCao> BaoCaos { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }
       
    }
}
