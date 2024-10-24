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
    public string HinhAnh { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; }

    [ForeignKey("DanhMuc")]
    public string MaDanhMuc { get; set; }
    public DanhMuc DanhMuc { get; set; }

    [ForeignKey("Loai")]
    public string MaLoai { get; set; }
    public Loai Loai { get; set; }

    [ForeignKey("KichThuoc")]
    public string MaKichThuoc { get; set; }
    public KichThuoc KichThuoc { get; set; }

    [ForeignKey("MauSac")]
    public string MaMauSac { get; set; }
    public MauSac MauSac { get; set; }
   
}

