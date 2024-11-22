using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnPhanMem.Models
{
    public class ThongBao
    {
        [Key]
        public string MaThongBao { get; set; }

        [ForeignKey("KhachHang")]
        public string MaKhachHang { get; set; }
        public KhachHang KhachHang { get; set; }
        public string MoTa { get; set; }
        public DateTime ThoiGian { get; set; }
        public int TrangThai { get; set; } = 0;
    }
}
