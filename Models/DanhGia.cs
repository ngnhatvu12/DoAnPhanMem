using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DanhGia
{
    [Key]
    public string MaDanhGia { get; set; }

    [ForeignKey("HoaDon")]
    public string MaHoaDon { get; set; }
    public HoaDon HoaDon { get; set; }

    [ForeignKey("SanPham")]
    public string MaSanPham { get; set; }
    public SanPham SanPham { get; set; }

    public int SoDiem { get; set; }

    public string BinhLuan { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; }

    public DateTime NgayDanhGia { get; set; }
}

