using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ThanhToan
{
    [Key]
    public string MaThanhToan { get; set; }

    [ForeignKey("DonHang")]
    public string MaDonHang { get; set; }
    public DonHang DonHang { get; set; }

    [StringLength(50)]
    public string PhuongThuc { get; set; }

    public decimal TongTien { get; set; }

    [StringLength(50)]
    public string TrangThai { get; set; }  // Giá trị có thể là 'Thành công', 'Thất bại', 'Chờ xử lý'

    public DateTime NgayThanhToan { get; set; }
}

