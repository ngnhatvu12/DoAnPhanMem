using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class KhachHang
{
    [Key]
    [StringLength(10)]
    public string MaKhachHang { get; set; }

    [Required]
    [StringLength(255)]
    public string TenKhachHang { get; set; }

    [StringLength(20)]
    public string SoDienThoai { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    public DateTime NgaySinh { get; set; }

    [StringLength(255)]
    public string DiaChi { get; set; }

    [StringLength(30)]
    public string DacQuyen { get; set; }

    [ForeignKey("TaiKhoan")]
    public string MaTaiKhoan { get; set; }
    public TaiKhoan TaiKhoan { get; set; }
}
