using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnPhanMem.Migrations
{
    /// <inheritdoc />
    public partial class AddHinhAnhToChiTietSanPham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHangs_SanPhams_MaSanPham",
                table: "ChiTietDonHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietGioHangs_SanPhams_SanPhamMaSanPham",
                table: "ChiTietGioHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_SanPhams_SanPhamMaSanPham",
                table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPhams_SanPhams_MaSanPham",
                table: "ChiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGias_SanPhams_SanPhamMaSanPham",
                table: "DanhGias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SanPhams",
                table: "SanPhams");

            migrationBuilder.RenameTable(
                name: "SanPhams",
                newName: "SanPham");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "ChiTietSanPhams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SanPham",
                table: "SanPham",
                column: "MaSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHangs_SanPham_MaSanPham",
                table: "ChiTietDonHangs",
                column: "MaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHangs_SanPham_SanPhamMaSanPham",
                table: "ChiTietGioHangs",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_SanPham_SanPhamMaSanPham",
                table: "ChiTietHoaDons",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPhams_SanPham_MaSanPham",
                table: "ChiTietSanPhams",
                column: "MaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGias_SanPham_SanPhamMaSanPham",
                table: "DanhGias",
                column: "SanPhamMaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDonHangs_SanPham_MaSanPham",
                table: "ChiTietDonHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietGioHangs_SanPham_SanPhamMaSanPham",
                table: "ChiTietGioHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_SanPham_SanPhamMaSanPham",
                table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPhams_SanPham_MaSanPham",
                table: "ChiTietSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGias_SanPham_SanPhamMaSanPham",
                table: "DanhGias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SanPham",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "ChiTietSanPhams");

            migrationBuilder.RenameTable(
                name: "SanPham",
                newName: "SanPhams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SanPhams",
                table: "SanPhams",
                column: "MaSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDonHangs_SanPhams_MaSanPham",
                table: "ChiTietDonHangs",
                column: "MaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHangs_SanPhams_SanPhamMaSanPham",
                table: "ChiTietGioHangs",
                column: "SanPhamMaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_SanPhams_SanPhamMaSanPham",
                table: "ChiTietHoaDons",
                column: "SanPhamMaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPhams_SanPhams_MaSanPham",
                table: "ChiTietSanPhams",
                column: "MaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGias_SanPhams_SanPhamMaSanPham",
                table: "DanhGias",
                column: "SanPhamMaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
