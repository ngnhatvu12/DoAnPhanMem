using System;
using System.ComponentModel.DataAnnotations;

public class GiamGia
{
    [Key]
    [StringLength(10)]
    public string MaGiamGia { get; set; }

    public DateTime NgayTao { get; set; }
    public decimal MucGiamGia { get; set; }
    public string DieuKien { get; set; }
    public DateTime NgayHieuLuc { get; set; }
    [StringLength(20)]
    public string LoaiGiamGia { get; set; }
}

