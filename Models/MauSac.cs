using System.ComponentModel.DataAnnotations;

public class MauSac
{
    [Key]
    public int MaMauSac { get; set; }

    [MaxLength(30)]
    public string TenMauSac { get; set; }
}
