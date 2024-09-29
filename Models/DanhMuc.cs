using System.ComponentModel.DataAnnotations;

public class DanhMuc
{
    [Key]
    public int MaDanhMuc { get; set; }

    [MaxLength(50)]
    public string TenDanhMuc { get; set; }
}
