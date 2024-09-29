using System;
using System.ComponentModel.DataAnnotations;

public class GiamGia
{
    [Key]
    public int MaGiamGia { get; set; }

    public DateTime NgayTao { get; set; }

    public decimal MucGiamGia { get; set; }

    public string DieuKien { get; set; }

    public DateTime NgayHieuLuc { get; set; }
}
