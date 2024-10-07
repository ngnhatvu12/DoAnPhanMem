using System.ComponentModel.DataAnnotations;

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

    public decimal GiaBan { get; set; }
    public decimal GiaGiam { get; set; }
}

