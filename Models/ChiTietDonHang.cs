using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietDonHang
{
    [Key]
    [StringLength(10)]
    public string MaChiTietDonHang { get; set; }

    [ForeignKey("DonHang")]
    public string MaDonHang { get; set; }
    public DonHang DonHang { get; set; }

    [ForeignKey("SanPham")]
    public string MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    [ForeignKey("GiamGia")]
    public string MaGiamGia { get; set; }
    public GiamGia GiamGia { get; set; }

    public int SoLuong { get; set; }
    public decimal GiaBan { get; set; }
}

