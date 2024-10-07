using System.ComponentModel.DataAnnotations;

public class DanhMuc
{
    [Key]
    [StringLength(10)]
    public string MaDanhMuc { get; set; }

    [Required]
    [StringLength(50)]
    public string TenDanhMuc { get; set; }
}

