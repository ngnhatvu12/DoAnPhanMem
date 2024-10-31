using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnPhanMem.Migrations
{
    /// <inheritdoc />
    public partial class chitietsanpham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietGioHang_ChiTietSanPham_ChiTietSanPhamMaChiTietSP",
                table: "ChiTietGioHang");

            migrationBuilder.DropColumn(
                name: "MaChiTietSanPham",
                table: "ChiTietGioHang");

            migrationBuilder.RenameColumn(
                name: "ChiTietSanPhamMaChiTietSP",
                table: "ChiTietGioHang",
                newName: "MaChiTietSP");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietGioHang_ChiTietSanPhamMaChiTietSP",
                table: "ChiTietGioHang",
                newName: "IX_ChiTietGioHang_MaChiTietSP");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHang_ChiTietSanPham_MaChiTietSP",
                table: "ChiTietGioHang",
                column: "MaChiTietSP",
                principalTable: "ChiTietSanPham",
                principalColumn: "MaChiTietSP",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietGioHang_ChiTietSanPham_MaChiTietSP",
                table: "ChiTietGioHang");

            migrationBuilder.RenameColumn(
                name: "MaChiTietSP",
                table: "ChiTietGioHang",
                newName: "ChiTietSanPhamMaChiTietSP");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietGioHang_MaChiTietSP",
                table: "ChiTietGioHang",
                newName: "IX_ChiTietGioHang_ChiTietSanPhamMaChiTietSP");

            migrationBuilder.AddColumn<string>(
                name: "MaChiTietSanPham",
                table: "ChiTietGioHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHang_ChiTietSanPham_ChiTietSanPhamMaChiTietSP",
                table: "ChiTietGioHang",
                column: "ChiTietSanPhamMaChiTietSP",
                principalTable: "ChiTietSanPham",
                principalColumn: "MaChiTietSP",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
