using DoAnPhanMem.Models;

namespace DoAnPhanMem.ViewModels
{
    public class ProductViewModel
    {
        public string MaSanPham { get; set; }      
        public string TenSanPham { get; set; }     
        public string MoTa { get; set; }
        public string MaLoai { get; set; }
        public string MaDanhMuc { get; set; }
        public string HinhAnh { get; set; }      
        public int SoLuong { get; set; }          
        public string TinhTrang { get; set; }      
        public decimal GiaBan { get; set; }        
        public decimal GiaGiam { get; set; }
        public string TenLoai { get; set; }
        public string TenDanhMuc { get; set; }

    }
}

