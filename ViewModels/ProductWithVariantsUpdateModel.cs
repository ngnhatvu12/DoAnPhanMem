namespace DoAnPhanMem.ViewModels
{
    public class ProductWithVariantsUpdateModel
    {
        public ProductUpdateModel ProductData { get; set; }
        public List<VariantUpdateModel> Variants { get; set; }
    }
}
