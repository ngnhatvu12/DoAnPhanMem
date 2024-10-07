using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Admina
{
    [Key]
    [StringLength(10)]
    public string MaAdmin { get; set; }

    [Required]
    [StringLength(255)]
    public string TenNhanVien { get; set; }

    [StringLength(50)]
    public string ChucVu { get; set; }

    [StringLength(20)]
    public string SoDienThoai { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    public DateTime NgayTao { get; set; }

    [ForeignKey("TaiKhoan")]
    public string MaTaiKhoan { get; set; }
    public TaiKhoan TaiKhoan { get; set; }
}

