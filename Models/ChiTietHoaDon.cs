using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietHoaDon
{
    [Key]
    public int MaChiTietHD { get; set; }

    [ForeignKey("HoaDon")]
    public int MaHoaDon { get; set; }
    public HoaDon HoaDon { get; set; }

    [ForeignKey("SanPham")]
    public int MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    public int SoLuong { get; set; }

    public decimal GiaBan { get; set; }
}
