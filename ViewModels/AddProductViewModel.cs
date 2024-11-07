using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoAnPhanMem.ViewModels
{
    public class AddProductViewModel
    {
        public ProductViewModel Product { get; set; }
        public List<SelectListItem> LoaiList { get; set; }
        public List<SelectListItem> DanhMucList { get; set; }
        public IFormFile HinhAnh { get; set; }
        public List<VariantViewModel> Variants { get; set; } = new List<VariantViewModel>();
    }
}

