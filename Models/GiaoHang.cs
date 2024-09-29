using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GiaoHang
{
    [Key]
    public int MaGiaoHang { get; set; }

    [ForeignKey("DonHang")]
    public int MaDonHang { get; set; }
    public DonHang DonHang { get; set; }

    public DateTime NgayGiao { get; set; }

    [MaxLength(100)]
    public string TrangThai { get; set; } // Đang giao, Đã giao, Hủy
}
