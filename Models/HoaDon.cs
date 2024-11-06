using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HoaDon
{
    [Key]
    public string MaHoaDon { get; set; }

    [ForeignKey("DonHang")]
    public string MaDonHang { get; set; }
    public DonHang DonHang { get; set; }

    public decimal TongTien { get; set; }

    public DateTime NgayLap { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; }  // Giá trị có thể là 'Đã thanh toán', 'Chờ thanh toán', 'Hủy'
}

