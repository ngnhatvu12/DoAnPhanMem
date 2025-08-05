using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DonHang
{
    [Key]
    [StringLength(10)]
    public string MaDonHang { get; set; }

    [ForeignKey("KhachHang")]
    public string MaKhachHang { get; set; }
    public KhachHang KhachHang { get; set; }

    public DateTime NgayDat { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; }

    public DateTime? NgayGiao { get; set; }

    [StringLength(255)]
    public string DiaChiGiao { get; set; }
    public ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
}

