using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GioHang
{
    [Key]
    public string MaGioHang { get; set; }

    public string MaKhachHang { get; set; }
    public KhachHang KhachHang { get; set; }

    public DateTime NgayTao { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; }
}

