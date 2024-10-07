using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietHoaDon
{
    [Key]
    public string MaChiTietHoaDon { get; set; }

    public string MaHoaDon { get; set; }
    public HoaDon HoaDon { get; set; }

    public string MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    public int SoLuong { get; set; }
    public decimal GiaThoiDiemMua { get; set; }
}

