using System;
using System.ComponentModel.DataAnnotations;

public class BaoCao
{
    [Key]
    public string MaBaoCao { get; set; }

    public string MaAdmin { get; set; }
    public Admina Admin { get; set; }

    public DateTime NgayTao { get; set; }

    [StringLength(500)]
    public string NoiDung { get; set; }

    [StringLength(30)]
    public string TrangThai { get; set; }
}

