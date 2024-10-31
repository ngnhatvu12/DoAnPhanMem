using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoAnPhanMem.ViewModels
{
    public class ProductDetailViewModel
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public decimal GiaBan { get; set; }
        public decimal? GiaGiam { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuongTon { get; set; }
        public string HinhAnhBienThe { get; set; }
        public List<SelectListItem> MauSacOptions { get; set; }
        public List<SelectListItem> KichThuocOptions { get; set; }
    }

}
