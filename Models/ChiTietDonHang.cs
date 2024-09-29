using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietDonHang
{
    [Key]
    public int MaChiTietDonHang { get; set; }

    [ForeignKey("DonHang")]
    public int MaDonHang { get; set; }
    public DonHang DonHang { get; set; }

    [ForeignKey("SanPham")]
    public int MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    [ForeignKey("GiamGia")]
    public int? MaGiamGia { get; set; }
    public GiamGia GiamGia { get; set; }

    public int SoLuong { get; set; }
    public decimal GiaBan { get; set; }
}
