using DoAnPhanMem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SanPham
{
    [Key]
    [StringLength(10)]
    public string MaSanPham { get; set; }

    [Required]
    [StringLength(255)]
    public string TenSanPham { get; set; }

    [StringLength(200)]
    public string MoTa { get; set; }
    public string HinhAnh { get; set; }
    [ForeignKey("DanhMuc")]
    public string MaDanhMuc { get; set; }
    public DanhMuc DanhMuc { get; set; }
    public decimal GiaBan { get; set; }
    public decimal GiaGiam { get; set; }
    [ForeignKey("Loai")]
    public string MaLoai { get; set; }
    public Loai Loai { get; set; }
    public ICollection<ChiTietSanPham> ChiTietSanPham { get; set; }
}

