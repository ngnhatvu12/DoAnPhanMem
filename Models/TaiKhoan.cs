using System.ComponentModel.DataAnnotations;

public class TaiKhoan
{
    [Key]
    public int MaTaiKhoan { get; set; }

    [Required]
    [MaxLength(50)]
    public string TenDangNhap { get; set; }

    [Required]
    [MaxLength(255)]
    public string MatKhau { get; set; }

    [Required]
    [MaxLength(50)]
    public string VaiTro { get; set; } // KhachHang, Admin
}
