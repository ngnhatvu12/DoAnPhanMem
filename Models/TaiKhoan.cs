using System.ComponentModel.DataAnnotations;

public class TaiKhoan
{
    [Key]
    [StringLength(10)]
    public string MaTaiKhoan { get; set; }

    [Required]
    [StringLength(50)]
    public string TenDangNhap { get; set; }

    [Required]
    [StringLength(255)]
    public string MatKhau { get; set; }

    [Required]
    [StringLength(50)]
    public string VaiTro { get; set; } // KhachHang or Admin
}
