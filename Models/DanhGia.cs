using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DanhGia
{
    [Key]
    public int MaDanhGia { get; set; }

    [ForeignKey("KhachHang")]
    public int MaKhachHang { get; set; }
    public KhachHang KhachHang { get; set; }

    [ForeignKey("SanPham")]
    public int MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    public int SoSao { get; set; } // Đánh giá từ 1 đến 5 sao

    [MaxLength(500)]
    public string BinhLuan { get; set; }

    public DateTime NgayDanhGia { get; set; }
}
