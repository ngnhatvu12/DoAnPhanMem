using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietGioHang
{
    [Key]
    [StringLength(10)]
    public string MaChiTietGioHang { get; set; }

    [Required]
    [StringLength(10)]
    public string MaGioHang { get; set; }

    [Required]
    [StringLength(10)]
    public string MaChiTietSP { get; set; }

    [Required]
    public int SoLuong { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TongTien { get; set; }

    // Khóa ngoại liên kết với bảng GioHang
    [ForeignKey("MaGioHang")]
    public virtual GioHang GioHang { get; set; }

    // Khóa ngoại liên kết với bảng ChiTietSanPham
    [ForeignKey("MaChiTietSP")]
    public virtual ChiTietSanPham ChiTietSanPham { get; set; }
}

