using System.ComponentModel.DataAnnotations;

public class SanPham
{
    [Key]
    public int MaSanPham { get; set; }

    [MaxLength(255)]
    public string TenSanPham { get; set; }

    [MaxLength(200)]
    public string MoTa { get; set; }

    public decimal GiaBan { get; set; }

    public decimal GiaGiam { get; set; }
}
