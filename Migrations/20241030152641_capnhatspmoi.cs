using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnPhanMem.Migrations
{
    /// <inheritdoc />
    public partial class capnhatspmoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_DanhMuc_MaDanhMuc",
                table: "ChiTietSanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_Loai_MaLoai",
                table: "ChiTietSanPham");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietSanPham_MaDanhMuc",
                table: "ChiTietSanPham");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietSanPham_MaLoai",
                table: "ChiTietSanPham");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietSanPham_MaSanPham",
                table: "ChiTietSanPham");

            migrationBuilder.DropColumn(
                name: "MaDanhMuc",
                table: "ChiTietSanPham");

            migrationBuilder.DropColumn(
                name: "MaLoai",
                table: "ChiTietSanPham");

            migrationBuilder.RenameColumn(
                name: "HinhAnh",
                table: "ChiTietSanPham",
                newName: "HinhAnhBienThe");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "SanPham",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaDanhMuc",
                table: "SanPham",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaLoai",
                table: "SanPham",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaDanhMuc",
                table: "SanPham",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaLoai",
                table: "SanPham",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaSanPham",
                table: "ChiTietSanPham",
                column: "MaSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_DanhMuc_MaDanhMuc",
                table: "SanPham",
                column: "MaDanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "MaDanhMuc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_Loai_MaLoai",
                table: "SanPham",
                column: "MaLoai",
                principalTable: "Loai",
                principalColumn: "MaLoai",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_DanhMuc_MaDanhMuc",
                table: "SanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_Loai_MaLoai",
                table: "SanPham");

            migrationBuilder.DropIndex(
                name: "IX_SanPham_MaDanhMuc",
                table: "SanPham");

            migrationBuilder.DropIndex(
                name: "IX_SanPham_MaLoai",
                table: "SanPham");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietSanPham_MaSanPham",
                table: "ChiTietSanPham");

            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "MaDanhMuc",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "MaLoai",
                table: "SanPham");

            migrationBuilder.RenameColumn(
                name: "HinhAnhBienThe",
                table: "ChiTietSanPham",
                newName: "HinhAnh");

            migrationBuilder.AddColumn<string>(
                name: "MaDanhMuc",
                table: "ChiTietSanPham",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaLoai",
                table: "ChiTietSanPham",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaDanhMuc",
                table: "ChiTietSanPham",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaLoai",
                table: "ChiTietSanPham",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaSanPham",
                table: "ChiTietSanPham",
                column: "MaSanPham",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_DanhMuc_MaDanhMuc",
                table: "ChiTietSanPham",
                column: "MaDanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "MaDanhMuc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_Loai_MaLoai",
                table: "ChiTietSanPham",
                column: "MaLoai",
                principalTable: "Loai",
                principalColumn: "MaLoai",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
