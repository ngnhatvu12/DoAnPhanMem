using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HoaDon
{
    [Key]
    public int MaHoaDon { get; set; }

    [ForeignKey("DonHang")]
    public int MaDonHang { get; set; }
    public DonHang DonHang { get; set; }

    public decimal TongTien { get; set; }

    public DateTime NgayLap { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; }
}
