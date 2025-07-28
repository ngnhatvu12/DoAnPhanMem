using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietDonHang
{
    [Key]
    [StringLength(10)]
    public string MaChiTietDonHang { get; set; }

    public string MaDonHang { get; set; }
    [ForeignKey("MaDonHang")]
    public DonHang DonHang { get; set; }

    public string MaChiTietSP { get; set; }
    [ForeignKey("MaChiTietSP")]
    public ChiTietSanPham ChiTietSanPham { get; set; }

    public string? MaGiamGia { get; set; }
    [ForeignKey("MaGiamGia")]
    public GiamGia? GiamGia { get; set; }

    public int SoLuong { get; set; }
    public decimal GiaBan { get; set; }
}

