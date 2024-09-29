using System.ComponentModel.DataAnnotations;

public class KichThuoc
{
    [Key]
    public int MaKichThuoc { get; set; }

    [MaxLength(30)]
    public string TenKichThuoc { get; set; }
}
