using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DonHang
{
    [Key]
    public int MaDonHang { get; set; }

    [ForeignKey("KhachHang")]
    public int MaKhachHang { get; set; }
    public KhachHang KhachHang { get; set; }

    public DateTime NgayDat { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; }

    public DateTime? NgayGiao { get; set; }

    [MaxLength(255)]
    public string DiaChiGiao { get; set; }
}
