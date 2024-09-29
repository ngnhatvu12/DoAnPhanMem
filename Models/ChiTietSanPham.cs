using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietSanPham
{
    [Key]
    public int MaChiTietSP { get; set; }

    [ForeignKey("SanPham")]
    public int MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    public int SoLuongTon { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; }

    [ForeignKey("DanhMuc")]
    public int MaDanhMuc { get; set; }
    public DanhMuc DanhMuc { get; set; }

    [ForeignKey("KichThuoc")]
    public int MaKichThuoc { get; set; }
    public KichThuoc KichThuoc { get; set; }

    [ForeignKey("MauSac")]
    public int MaMauSac { get; set; }
    public MauSac MauSac { get; set; }
}
