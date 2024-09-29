using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ThanhToan
{
    [Key]
    public int MaThanhToan { get; set; }

    [ForeignKey("HoaDon")]
    public int MaHoaDon { get; set; }
    public HoaDon HoaDon { get; set; }

    [MaxLength(50)]
    public string PhuongThucThanhToan { get; set; } // Ví dụ: Thẻ tín dụng, Chuyển khoản ngân hàng

    public DateTime NgayThanhToan { get; set; }
}
