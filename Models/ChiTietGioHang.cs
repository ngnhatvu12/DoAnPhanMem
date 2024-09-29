using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietGioHang
{
    [Key]
    public int MaChiTietGioHang { get; set; }

    [ForeignKey("GioHang")]
    public int MaGioHang { get; set; }
    public GioHang GioHang { get; set; }

    [ForeignKey("SanPham")]
    public int MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    public int SoLuong { get; set; }

    public decimal TongTien { get; set; }
}
