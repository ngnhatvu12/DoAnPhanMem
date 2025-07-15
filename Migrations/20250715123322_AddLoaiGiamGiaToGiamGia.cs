using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnPhanMem.Migrations
{
    /// <inheritdoc />
    public partial class AddLoaiGiamGiaToGiamGia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoaiGiamGia",
                table: "GiamGia",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
            migrationBuilder.Sql("UPDATE GiamGia SET LoaiGiamGia = 'Thường' WHERE LoaiGiamGia IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiGiamGia",
                table: "GiamGia");
        }
    }
}
