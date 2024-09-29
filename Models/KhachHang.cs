using System;
using System.ComponentModel.DataAnnotations;

public class KhachHang
{
    [Key]
    public int MaKhachHang { get; set; }

    [Required]
    [MaxLength(255)]
    public string TenKhachHang { get; set; }

    [MaxLength(20)]
    public string SoDienThoai { get; set; }

    [MaxLength(255)]
    public string Email { get; set; }

    public DateTime? NgaySinh { get; set; }

    [MaxLength(255)]
    public string DiaChi { get; set; }

    [MaxLength(30)]
    public string DacQuyen { get; set; }
}
