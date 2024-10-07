using System.ComponentModel.DataAnnotations;

public class KichThuoc
{
    [Key]
    [StringLength(10)]
    public string MaKichThuoc { get; set; }

    [Required]
    [StringLength(30)]
    public string TenKichThuoc { get; set; }
}

