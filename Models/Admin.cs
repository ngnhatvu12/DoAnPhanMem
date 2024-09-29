using System;
using System.ComponentModel.DataAnnotations;

public class ADMINA
{
    [Key]
    public int MaAdmin { get; set; }

    [MaxLength(255)]
    public string TenNhanVien { get; set; }

    [MaxLength(50)]
    public string ChucVu { get; set; }

    [MaxLength(20)]
    public string SoDienThoai { get; set; }

    [MaxLength(255)]
    public string Email { get; set; }

    public DateTime NgayTao { get; set; }
}
