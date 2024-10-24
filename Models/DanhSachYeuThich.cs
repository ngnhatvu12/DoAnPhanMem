using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnPhanMem.Models
{
    public class DanhSachYeuThich
    {
        [Key]
        public string MaYeuThich { get; set; }

        [ForeignKey("KhachHang")]
        public string MaKhachHang { get; set; }
        public KhachHang KhachHang { get; set; }
        public DateTime NgayTao { get; set; }
        [ForeignKey("SanPham")]
        public string MaSanPham { get; set; }
        public SanPham SanPham { get; set; }
    }
}
