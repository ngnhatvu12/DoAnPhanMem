using DoAnPhanMem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietSanPham
{
    [Key]
    [StringLength(10)]
    public string MaChiTietSP { get; set; }

    [ForeignKey("SanPham")]
    public string MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    public int SoLuongTon { get; set; }
    public string HinhAnhBienThe { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; }

    [ForeignKey("KichThuoc")]
    public string MaKichThuoc { get; set; }
    public KichThuoc KichThuoc { get; set; }

    [ForeignKey("MauSac")]
    public string MaMauSac { get; set; }
    public MauSac MauSac { get; set; }
   
}

