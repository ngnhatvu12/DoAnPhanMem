using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnPhanMem.Migrations
{
    /// <inheritdoc />
    public partial class CapNhatdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_TaiKhoan_MaTaiKhoan",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_BaoCaos_Admins_AdminMaAdmin",
                table: "BaoCaos");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHangs_DonHangs_MaDonHang",
                table: "ChiTietDonHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHangs_GiamGias_MaGiamGia",
                table: "ChiTietDonHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHangs_SanPham_MaSanPham",
                table: "ChiTietDonHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietGioHangs_GioHangs_GioHangMaGioHang",
                table: "ChiTietGioHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietGioHangs_SanPham_SanPhamMaSanPham",
                table: "ChiTietGioHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_HoaDons_HoaDonMaHoaDon",
                table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_SanPham_SanPhamMaSanPham",
                table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPhams_DanhMucs_MaDanhMuc",
                table: "ChiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPhams_KichThuocs_MaKichThuoc",
                table: "ChiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPhams_MauSacs_MaMauSac",
                table: "ChiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPhams_SanPham_MaSanPham",
                table: "ChiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGias_HoaDons_HoaDonMaHoaDon",
                table: "DanhGias");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGias_KhachHang_KhachHangMaKhachHang",
                table: "DanhGias");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGias_SanPham_SanPhamMaSanPham",
                table: "DanhGias");

            migrationBuilder.DropForeignKey(
                name: "FK_DonHangs_KhachHang_MaKhachHang",
                table: "DonHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoHangs_DonHangs_DonHangMaDonHang",
                table: "GiaoHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_GioHangs_KhachHang_KhachHangMaKhachHang",
                table: "GioHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDons_DonHangs_DonHangMaDonHang",
                table: "HoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToans_DonHangs_DonHangMaDonHang",
                table: "ThanhToans");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToans_KhachHang_KhachHangMaKhachHang",
                table: "ThanhToans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThanhToans",
                table: "ThanhToans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MauSacs",
                table: "MauSacs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KichThuocs",
                table: "KichThuocs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoaDons",
                table: "HoaDons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GioHangs",
                table: "GioHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GiaoHangs",
                table: "GiaoHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GiamGias",
                table: "GiamGias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonHangs",
                table: "DonHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DanhMucs",
                table: "DanhMucs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DanhGias",
                table: "DanhGias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietSanPhams",
                table: "ChiTietSanPhams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietHoaDons",
                table: "ChiTietHoaDons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietGioHangs",
                table: "ChiTietGioHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietDonHangs",
                table: "ChiTietDonHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaoCaos",
                table: "BaoCaos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "ThanhToans",
                newName: "ThanhToan");

            migrationBuilder.RenameTable(
                name: "MauSacs",
                newName: "MauSac");

            migrationBuilder.RenameTable(
                name: "KichThuocs",
                newName: "KichThuoc");

            migrationBuilder.RenameTable(
                name: "HoaDons",
                newName: "HoaDon");

            migrationBuilder.RenameTable(
                name: "GioHangs",
                newName: "GioHang");

            migrationBuilder.RenameTable(
                name: "GiaoHangs",
                newName: "GiaoHang");

            migrationBuilder.RenameTable(
                name: "GiamGias",
                newName: "GiamGia");

            migrationBuilder.RenameTable(
                name: "DonHangs",
                newName: "DonHang");

            migrationBuilder.RenameTable(
                name: "DanhMucs",
                newName: "DanhMuc");

            migrationBuilder.RenameTable(
                name: "DanhGias",
                newName: "DanhGia");

            migrationBuilder.RenameTable(
                name: "ChiTietSanPhams",
                newName: "ChiTietSanPham");

            migrationBuilder.RenameTable(
                name: "ChiTietHoaDons",
                newName: "ChiTietHoaDon");

            migrationBuilder.RenameTable(
                name: "ChiTietGioHangs",
                newName: "ChiTietGioHang");

            migrationBuilder.RenameTable(
                name: "ChiTietDonHangs",
                newName: "ChiTietDonHang");

            migrationBuilder.RenameTable(
                name: "BaoCaos",
                newName: "BaoCao");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "Admina");

            migrationBuilder.RenameIndex(
                name: "IX_ThanhToans_KhachHangMaKhachHang",
                table: "ThanhToan",
                newName: "IX_ThanhToan_KhachHangMaKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_ThanhToans_DonHangMaDonHang",
                table: "ThanhToan",
                newName: "IX_ThanhToan_DonHangMaDonHang");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDons_DonHangMaDonHang",
                table: "HoaDon",
                newName: "IX_HoaDon_DonHangMaDonHang");

            migrationBuilder.RenameIndex(
                name: "IX_GioHangs_KhachHangMaKhachHang",
                table: "GioHang",
                newName: "IX_GioHang_KhachHangMaKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_GiaoHangs_DonHangMaDonHang",
                table: "GiaoHang",
                newName: "IX_GiaoHang_DonHangMaDonHang");

            migrationBuilder.RenameIndex(
                name: "IX_DonHangs_MaKhachHang",
                table: "DonHang",
                newName: "IX_DonHang_MaKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_DanhGias_SanPhamMaSanPham",
                table: "DanhGia",
                newName: "IX_DanhGia_SanPhamMaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_DanhGias_KhachHangMaKhachHang",
                table: "DanhGia",
                newName: "IX_DanhGia_KhachHangMaKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_DanhGias_HoaDonMaHoaDon",
                table: "DanhGia",
                newName: "IX_DanhGia_HoaDonMaHoaDon");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPhams_MaSanPham",
                table: "ChiTietSanPham",
                newName: "IX_ChiTietSanPham_MaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPhams_MaMauSac",
                table: "ChiTietSanPham",
                newName: "IX_ChiTietSanPham_MaMauSac");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPhams_MaKichThuoc",
                table: "ChiTietSanPham",
                newName: "IX_ChiTietSanPham_MaKichThuoc");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPhams_MaDanhMuc",
                table: "ChiTietSanPham",
                newName: "IX_ChiTietSanPham_MaDanhMuc");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDons_SanPhamMaSanPham",
                table: "ChiTietHoaDon",
                newName: "IX_ChiTietHoaDon_SanPhamMaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDons_HoaDonMaHoaDon",
                table: "ChiTietHoaDon",
                newName: "IX_ChiTietHoaDon_HoaDonMaHoaDon");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietGioHangs_SanPhamMaSanPham",
                table: "ChiTietGioHang",
                newName: "IX_ChiTietGioHang_SanPhamMaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietGioHangs_GioHangMaGioHang",
                table: "ChiTietGioHang",
                newName: "IX_ChiTietGioHang_GioHangMaGioHang");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDonHangs_MaSanPham",
                table: "ChiTietDonHang",
                newName: "IX_ChiTietDonHang_MaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDonHangs_MaGiamGia",
                table: "ChiTietDonHang",
                newName: "IX_ChiTietDonHang_MaGiamGia");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDonHangs_MaDonHang",
                table: "ChiTietDonHang",
                newName: "IX_ChiTietDonHang_MaDonHang");

            migrationBuilder.RenameIndex(
                name: "IX_BaoCaos_AdminMaAdmin",
                table: "BaoCao",
                newName: "IX_BaoCao_AdminMaAdmin");

            migrationBuilder.RenameIndex(
                name: "IX_Admins_MaTaiKhoan",
                table: "Admina",
                newName: "IX_Admina_MaTaiKhoan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThanhToan",
                table: "ThanhToan",
                column: "MaThanhToan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MauSac",
                table: "MauSac",
                column: "MaMauSac");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KichThuoc",
                table: "KichThuoc",
                column: "MaKichThuoc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoaDon",
                table: "HoaDon",
                column: "MaHoaDon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang",
                column: "MaGioHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GiaoHang",
                table: "GiaoHang",
                column: "MaGiaoHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GiamGia",
                table: "GiamGia",
                column: "MaGiamGia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonHang",
                table: "DonHang",
                column: "MaDonHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DanhMuc",
                table: "DanhMuc",
                column: "MaDanhMuc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DanhGia",
                table: "DanhGia",
                column: "MaDanhGia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietSanPham",
                table: "ChiTietSanPham",
                column: "MaChiTietSP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietHoaDon",
                table: "ChiTietHoaDon",
                column: "MaChiTietHoaDon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietGioHang",
                table: "ChiTietGioHang",
                column: "MaChiTietGioHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietDonHang",
                table: "ChiTietDonHang",
                column: "MaChiTietDonHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaoCao",
                table: "BaoCao",
                column: "MaBaoCao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admina",
                table: "Admina",
                column: "MaAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_Admina_TaiKhoan_MaTaiKhoan",
                table: "Admina",
                column: "MaTaiKhoan",
                principalTable: "TaiKhoan",
                principalColumn: "MaTaiKhoan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaoCao_Admina_AdminMaAdmin",
                table: "BaoCao",
                column: "AdminMaAdmin",
                principalTable: "Admina",
                principalColumn: "MaAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHang_DonHang_MaDonHang",
                table: "ChiTietDonHang",
                column: "MaDonHang",
                principalTable: "DonHang",
                principalColumn: "MaDonHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHang_GiamGia_MaGiamGia",
                table: "ChiTietDonHang",
                column: "MaGiamGia",
                principalTable: "GiamGia",
                principalColumn: "MaGiamGia",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHang_SanPham_MaSanPham",
                table: "ChiTietDonHang",
                column: "MaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHang_GioHang_GioHangMaGioHang",
                table: "ChiTietGioHang",
                column: "GioHangMaGioHang",
                principalTable: "GioHang",
                principalColumn: "MaGioHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHang_SanPham_SanPhamMaSanPham",
                table: "ChiTietGioHang",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDon_HoaDon_HoaDonMaHoaDon",
                table: "ChiTietHoaDon",
                column: "HoaDonMaHoaDon",
                principalTable: "HoaDon",
                principalColumn: "MaHoaDon",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDon_SanPham_SanPhamMaSanPham",
                table: "ChiTietHoaDon",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_DanhMuc_MaDanhMuc",
                table: "ChiTietSanPham",
                column: "MaDanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "MaDanhMuc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_KichThuoc_MaKichThuoc",
                table: "ChiTietSanPham",
                column: "MaKichThuoc",
                principalTable: "KichThuoc",
                principalColumn: "MaKichThuoc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_MauSac_MaMauSac",
                table: "ChiTietSanPham",
                column: "MaMauSac",
                principalTable: "MauSac",
                principalColumn: "MaMauSac",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_SanPham_MaSanPham",
                table: "ChiTietSanPham",
                column: "MaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_HoaDon_HoaDonMaHoaDon",
                table: "DanhGia",
                column: "HoaDonMaHoaDon",
                principalTable: "HoaDon",
                principalColumn: "MaHoaDon",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_KhachHang_KhachHangMaKhachHang",
                table: "DanhGia",
                column: "KhachHangMaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_SanPham_SanPhamMaSanPham",
                table: "DanhGia",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonHang_KhachHang_MaKhachHang",
                table: "DonHang",
                column: "MaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoHang_DonHang_DonHangMaDonHang",
                table: "GiaoHang",
                column: "DonHangMaDonHang",
                principalTable: "DonHang",
                principalColumn: "MaDonHang");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHang_KhachHang_KhachHangMaKhachHang",
                table: "GioHang",
                column: "KhachHangMaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_DonHang_DonHangMaDonHang",
                table: "HoaDon",
                column: "DonHangMaDonHang",
                principalTable: "DonHang",
                principalColumn: "MaDonHang");

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToan_DonHang_DonHangMaDonHang",
                table: "ThanhToan",
                column: "DonHangMaDonHang",
                principalTable: "DonHang",
                principalColumn: "MaDonHang");

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToan_KhachHang_KhachHangMaKhachHang",
                table: "ThanhToan",
                column: "KhachHangMaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admina_TaiKhoan_MaTaiKhoan",
                table: "Admina");

            migrationBuilder.DropForeignKey(
                name: "FK_BaoCao_Admina_AdminMaAdmin",
                table: "BaoCao");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHang_DonHang_MaDonHang",
                table: "ChiTietDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHang_GiamGia_MaGiamGia",
                table: "ChiTietDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHang_SanPham_MaSanPham",
                table: "ChiTietDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietGioHang_GioHang_GioHangMaGioHang",
                table: "ChiTietGioHang");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietGioHang_SanPham_SanPhamMaSanPham",
                table: "ChiTietGioHang");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDon_HoaDon_HoaDonMaHoaDon",
                table: "ChiTietHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDon_SanPham_SanPhamMaSanPham",
                table: "ChiTietHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_DanhMuc_MaDanhMuc",
                table: "ChiTietSanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_KichThuoc_MaKichThuoc",
                table: "ChiTietSanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_MauSac_MaMauSac",
                table: "ChiTietSanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_SanPham_MaSanPham",
                table: "ChiTietSanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_HoaDon_HoaDonMaHoaDon",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_KhachHang_KhachHangMaKhachHang",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_SanPham_SanPhamMaSanPham",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK_DonHang_KhachHang_MaKhachHang",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoHang_DonHang_DonHangMaDonHang",
                table: "GiaoHang");

            migrationBuilder.DropForeignKey(
                name: "FK_GioHang_KhachHang_KhachHangMaKhachHang",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_DonHang_DonHangMaDonHang",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToan_DonHang_DonHangMaDonHang",
                table: "ThanhToan");

            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToan_KhachHang_KhachHangMaKhachHang",
                table: "ThanhToan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThanhToan",
                table: "ThanhToan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MauSac",
                table: "MauSac");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KichThuoc",
                table: "KichThuoc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoaDon",
                table: "HoaDon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GiaoHang",
                table: "GiaoHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GiamGia",
                table: "GiamGia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonHang",
                table: "DonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DanhMuc",
                table: "DanhMuc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DanhGia",
                table: "DanhGia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietSanPham",
                table: "ChiTietSanPham");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietHoaDon",
                table: "ChiTietHoaDon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietGioHang",
                table: "ChiTietGioHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietDonHang",
                table: "ChiTietDonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaoCao",
                table: "BaoCao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admina",
                table: "Admina");

            migrationBuilder.RenameTable(
                name: "ThanhToan",
                newName: "ThanhToans");

            migrationBuilder.RenameTable(
                name: "MauSac",
                newName: "MauSacs");

            migrationBuilder.RenameTable(
                name: "KichThuoc",
                newName: "KichThuocs");

            migrationBuilder.RenameTable(
                name: "HoaDon",
                newName: "HoaDons");

            migrationBuilder.RenameTable(
                name: "GioHang",
                newName: "GioHangs");

            migrationBuilder.RenameTable(
                name: "GiaoHang",
                newName: "GiaoHangs");

            migrationBuilder.RenameTable(
                name: "GiamGia",
                newName: "GiamGias");

            migrationBuilder.RenameTable(
                name: "DonHang",
                newName: "DonHangs");

            migrationBuilder.RenameTable(
                name: "DanhMuc",
                newName: "DanhMucs");

            migrationBuilder.RenameTable(
                name: "DanhGia",
                newName: "DanhGias");

            migrationBuilder.RenameTable(
                name: "ChiTietSanPham",
                newName: "ChiTietSanPhams");

            migrationBuilder.RenameTable(
                name: "ChiTietHoaDon",
                newName: "ChiTietHoaDons");

            migrationBuilder.RenameTable(
                name: "ChiTietGioHang",
                newName: "ChiTietGioHangs");

            migrationBuilder.RenameTable(
                name: "ChiTietDonHang",
                newName: "ChiTietDonHangs");

            migrationBuilder.RenameTable(
                name: "BaoCao",
                newName: "BaoCaos");

            migrationBuilder.RenameTable(
                name: "Admina",
                newName: "Admins");

            migrationBuilder.RenameIndex(
                name: "IX_ThanhToan_KhachHangMaKhachHang",
                table: "ThanhToans",
                newName: "IX_ThanhToans_KhachHangMaKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_ThanhToan_DonHangMaDonHang",
                table: "ThanhToans",
                newName: "IX_ThanhToans_DonHangMaDonHang");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_DonHangMaDonHang",
                table: "HoaDons",
                newName: "IX_HoaDons_DonHangMaDonHang");

            migrationBuilder.RenameIndex(
                name: "IX_GioHang_KhachHangMaKhachHang",
                table: "GioHangs",
                newName: "IX_GioHangs_KhachHangMaKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_GiaoHang_DonHangMaDonHang",
                table: "GiaoHangs",
                newName: "IX_GiaoHangs_DonHangMaDonHang");

            migrationBuilder.RenameIndex(
                name: "IX_DonHang_MaKhachHang",
                table: "DonHangs",
                newName: "IX_DonHangs_MaKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_DanhGia_SanPhamMaSanPham",
                table: "DanhGias",
                newName: "IX_DanhGias_SanPhamMaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_DanhGia_KhachHangMaKhachHang",
                table: "DanhGias",
                newName: "IX_DanhGias_KhachHangMaKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_DanhGia_HoaDonMaHoaDon",
                table: "DanhGias",
                newName: "IX_DanhGias_HoaDonMaHoaDon");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPham_MaSanPham",
                table: "ChiTietSanPhams",
                newName: "IX_ChiTietSanPhams_MaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPham_MaMauSac",
                table: "ChiTietSanPhams",
                newName: "IX_ChiTietSanPhams_MaMauSac");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPham_MaKichThuoc",
                table: "ChiTietSanPhams",
                newName: "IX_ChiTietSanPhams_MaKichThuoc");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietSanPham_MaDanhMuc",
                table: "ChiTietSanPhams",
                newName: "IX_ChiTietSanPhams_MaDanhMuc");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDon_SanPhamMaSanPham",
                table: "ChiTietHoaDons",
                newName: "IX_ChiTietHoaDons_SanPhamMaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDon_HoaDonMaHoaDon",
                table: "ChiTietHoaDons",
                newName: "IX_ChiTietHoaDons_HoaDonMaHoaDon");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietGioHang_SanPhamMaSanPham",
                table: "ChiTietGioHangs",
                newName: "IX_ChiTietGioHangs_SanPhamMaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietGioHang_GioHangMaGioHang",
                table: "ChiTietGioHangs",
                newName: "IX_ChiTietGioHangs_GioHangMaGioHang");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDonHang_MaSanPham",
                table: "ChiTietDonHangs",
                newName: "IX_ChiTietDonHangs_MaSanPham");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDonHang_MaGiamGia",
                table: "ChiTietDonHangs",
                newName: "IX_ChiTietDonHangs_MaGiamGia");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDonHang_MaDonHang",
                table: "ChiTietDonHangs",
                newName: "IX_ChiTietDonHangs_MaDonHang");

            migrationBuilder.RenameIndex(
                name: "IX_BaoCao_AdminMaAdmin",
                table: "BaoCaos",
                newName: "IX_BaoCaos_AdminMaAdmin");

            migrationBuilder.RenameIndex(
                name: "IX_Admina_MaTaiKhoan",
                table: "Admins",
                newName: "IX_Admins_MaTaiKhoan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThanhToans",
                table: "ThanhToans",
                column: "MaThanhToan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MauSacs",
                table: "MauSacs",
                column: "MaMauSac");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KichThuocs",
                table: "KichThuocs",
                column: "MaKichThuoc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoaDons",
                table: "HoaDons",
                column: "MaHoaDon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GioHangs",
                table: "GioHangs",
                column: "MaGioHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GiaoHangs",
                table: "GiaoHangs",
                column: "MaGiaoHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GiamGias",
                table: "GiamGias",
                column: "MaGiamGia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonHangs",
                table: "DonHangs",
                column: "MaDonHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DanhMucs",
                table: "DanhMucs",
                column: "MaDanhMuc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DanhGias",
                table: "DanhGias",
                column: "MaDanhGia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietSanPhams",
                table: "ChiTietSanPhams",
                column: "MaChiTietSP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietHoaDons",
                table: "ChiTietHoaDons",
                column: "MaChiTietHoaDon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietGioHangs",
                table: "ChiTietGioHangs",
                column: "MaChiTietGioHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietDonHangs",
                table: "ChiTietDonHangs",
                column: "MaChiTietDonHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaoCaos",
                table: "BaoCaos",
                column: "MaBaoCao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "MaAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_TaiKhoan_MaTaiKhoan",
                table: "Admins",
                column: "MaTaiKhoan",
                principalTable: "TaiKhoan",
                principalColumn: "MaTaiKhoan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaoCaos_Admins_AdminMaAdmin",
                table: "BaoCaos",
                column: "AdminMaAdmin",
                principalTable: "Admins",
                principalColumn: "MaAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHangs_DonHangs_MaDonHang",
                table: "ChiTietDonHangs",
                column: "MaDonHang",
                principalTable: "DonHangs",
                principalColumn: "MaDonHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHangs_GiamGias_MaGiamGia",
                table: "ChiTietDonHangs",
                column: "MaGiamGia",
                principalTable: "GiamGias",
                principalColumn: "MaGiamGia",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHangs_SanPham_MaSanPham",
                table: "ChiTietDonHangs",
                column: "MaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHangs_GioHangs_GioHangMaGioHang",
                table: "ChiTietGioHangs",
                column: "GioHangMaGioHang",
                principalTable: "GioHangs",
                principalColumn: "MaGioHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHangs_SanPham_SanPhamMaSanPham",
                table: "ChiTietGioHangs",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_HoaDons_HoaDonMaHoaDon",
                table: "ChiTietHoaDons",
                column: "HoaDonMaHoaDon",
                principalTable: "HoaDons",
                principalColumn: "MaHoaDon",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_SanPham_SanPhamMaSanPham",
                table: "ChiTietHoaDons",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPhams_DanhMucs_MaDanhMuc",
                table: "ChiTietSanPhams",
                column: "MaDanhMuc",
                principalTable: "DanhMucs",
                principalColumn: "MaDanhMuc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPhams_KichThuocs_MaKichThuoc",
                table: "ChiTietSanPhams",
                column: "MaKichThuoc",
                principalTable: "KichThuocs",
                principalColumn: "MaKichThuoc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPhams_MauSacs_MaMauSac",
                table: "ChiTietSanPhams",
                column: "MaMauSac",
                principalTable: "MauSacs",
                principalColumn: "MaMauSac",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPhams_SanPham_MaSanPham",
                table: "ChiTietSanPhams",
                column: "MaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGias_HoaDons_HoaDonMaHoaDon",
                table: "DanhGias",
                column: "HoaDonMaHoaDon",
                principalTable: "HoaDons",
                principalColumn: "MaHoaDon",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGias_KhachHang_KhachHangMaKhachHang",
                table: "DanhGias",
                column: "KhachHangMaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGias_SanPham_SanPhamMaSanPham",
                table: "DanhGias",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonHangs_KhachHang_MaKhachHang",
                table: "DonHangs",
                column: "MaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoHangs_DonHangs_DonHangMaDonHang",
                table: "GiaoHangs",
                column: "DonHangMaDonHang",
                principalTable: "DonHangs",
                principalColumn: "MaDonHang");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangs_KhachHang_KhachHangMaKhachHang",
                table: "GioHangs",
                column: "KhachHangMaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDons_DonHangs_DonHangMaDonHang",
                table: "HoaDons",
                column: "DonHangMaDonHang",
                principalTable: "DonHangs",
                principalColumn: "MaDonHang");

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToans_DonHangs_DonHangMaDonHang",
                table: "ThanhToans",
                column: "DonHangMaDonHang",
                principalTable: "DonHangs",
                principalColumn: "MaDonHang");

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToans_KhachHang_KhachHangMaKhachHang",
                table: "ThanhToans",
                column: "KhachHangMaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang");
        }
    }
}
