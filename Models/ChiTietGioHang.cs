using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietGioHang
{
    [Key]
    public string MaChiTietGioHang { get; set; }

    public string MaGioHang { get; set; }
    public GioHang GioHang { get; set; }

    [ForeignKey("ChiTietSanPham")]
    public string MaChiTietSP { get; set; }
    public ChiTietSanPham ChiTietSanPham { get; set; }

    public int SoLuong { get; set; }
    public decimal TongTien { get; set; }
}

