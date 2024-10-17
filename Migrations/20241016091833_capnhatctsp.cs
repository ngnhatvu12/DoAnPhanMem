using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnPhanMem.Migrations
{
    /// <inheritdoc />
    public partial class capnhatctsp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaLoai",
                table: "ChiTietSanPham",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.MaLoai);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaLoai",
                table: "ChiTietSanPham",
                column: "MaLoai");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_Loai_MaLoai",
                table: "ChiTietSanPham",
                column: "MaLoai",
                principalTable: "Loai",
                principalColumn: "MaLoai",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietSanPham_Loai_MaLoai",
                table: "ChiTietSanPham");

            migrationBuilder.DropTable(
                name: "Loai");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietSanPham_MaLoai",
                table: "ChiTietSanPham");

            migrationBuilder.DropColumn(
                name: "MaLoai",
                table: "ChiTietSanPham");
        }
    }
}
