using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GioHang
{
    [Key]
    public int MaGioHang { get; set; }

    [ForeignKey("KhachHang")]
    public int MaKhachHang { get; set; }
    public KhachHang KhachHang { get; set; }

    public DateTime NgayTao { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; }
}
