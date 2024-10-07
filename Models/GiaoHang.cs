using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GiaoHang
{
    [Key]
    public string MaGiaoHang { get; set; }

    public string MaDonHang { get; set; }
    public DonHang DonHang { get; set; }

    public DateTime NgayGiao { get; set; }

    [StringLength(100)]
    public string TrangThai { get; set; }  // Giá trị có thể là 'Đang giao', 'Đã giao', 'Hủy'
}


