using System;
using System.ComponentModel.DataAnnotations;

public class BaoCao
{
    [Key]
    public int MaBaoCao { get; set; }

    public DateTime NgayLap { get; set; }

    [MaxLength(255)]
    public string NoiDung { get; set; }
}
