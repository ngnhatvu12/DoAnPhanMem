using System.ComponentModel.DataAnnotations;

public class MauSac
{
    [Key]
    [StringLength(10)]
    public string MaMauSac { get; set; }

    [Required]
    [StringLength(30)]
    public string TenMauSac { get; set; }
}

