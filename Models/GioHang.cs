using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GioHang
{
    [Key]
    [StringLength(10)]
    public string MaGioHang { get; set; }

    [Required]
    [StringLength(10)]
    public string MaTaiKhoan { get; set; }

    [ForeignKey("MaTaiKhoan")]
    public TaiKhoan TaiKhoan { get; set; }

    public DateTime NgayTao { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; }

    public ICollection<ChiTietGioHang> ChiTietGioHang { get; set; }
}


