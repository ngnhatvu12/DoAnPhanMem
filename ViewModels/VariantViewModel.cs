namespace DoAnPhanMem.ViewModels
{
    public class VariantViewModel
    {
        public string MaChiTietSP { get; set; }      // Mã chi tiết sản phẩm, cần kiểm tra không trùng
        public int SoLuongTon { get; set; }          // Số lượng tồn của biến thể
        public string MaKichThuoc { get; set; }      // Mã kích thước, tham chiếu tới bảng KichThuoc
        public string MaMauSac { get; set; }         // Mã màu sắc, tham chiếu tới bảng MauSac
        public IFormFile HinhAnhBienThe { get; set; } // Hình ảnh biến thể, nhận từ form dưới dạng file

        // Nếu cần hiển thị tình trạng dựa trên số lượng tồn, có thể thêm thuộc tính này:
        public string TrangThai => SoLuongTon > 0 ? "Còn hàng" : "Hết hàng";
    }
}

