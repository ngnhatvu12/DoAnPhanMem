using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnPhanMem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    MaDanhMuc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.MaDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "GiamGias",
                columns: table => new
                {
                    MaGiamGia = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MucGiamGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DieuKien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiamGias", x => x.MaGiamGia);
                });

            migrationBuilder.CreateTable(
                name: "KichThuocs",
                columns: table => new
                {
                    MaKichThuoc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenKichThuoc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KichThuocs", x => x.MaKichThuoc);
                });

            migrationBuilder.CreateTable(
                name: "MauSacs",
                columns: table => new
                {
                    MaMauSac = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenMauSac = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSacs", x => x.MaMauSac);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    MaSanPham = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiaGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.MaSanPham);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.MaTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietSanPhams",
                columns: table => new
                {
                    MaChiTietSP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaDanhMuc = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaKichThuoc = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaMauSac = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietSanPhams", x => x.MaChiTietSP);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPhams_DanhMucs_MaDanhMuc",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMucs",
                        principalColumn: "MaDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPhams_KichThuocs_MaKichThuoc",
                        column: x => x.MaKichThuoc,
                        principalTable: "KichThuocs",
                        principalColumn: "MaKichThuoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPhams_MauSacs_MaMauSac",
                        column: x => x.MaMauSac,
                        principalTable: "MauSacs",
                        principalColumn: "MaMauSac",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPhams_SanPhams_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    MaAdmin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenNhanVien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaTaiKhoan = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.MaAdmin);
                    table.ForeignKey(
                        name: "FK_Admins_TaiKhoan_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKhachHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DacQuyen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaTaiKhoan = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKhachHang);
                    table.ForeignKey(
                        name: "FK_KhachHang_TaiKhoan_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoCaos",
                columns: table => new
                {
                    MaBaoCao = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaAdmin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminMaAdmin = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaos", x => x.MaBaoCao);
                    table.ForeignKey(
                        name: "FK_BaoCaos_Admins_AdminMaAdmin",
                        column: x => x.AdminMaAdmin,
                        principalTable: "Admins",
                        principalColumn: "MaAdmin");
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    MaDonHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChiGiao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.MaDonHang);
                    table.ForeignKey(
                        name: "FK_DonHangs_KhachHang_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    MaGioHang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhachHangMaKhachHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.MaGioHang);
                    table.ForeignKey(
                        name: "FK_GioHangs_KhachHang_KhachHangMaKhachHang",
                        column: x => x.KhachHangMaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHangs",
                columns: table => new
                {
                    MaChiTietDonHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaGiamGia = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHangs", x => x.MaChiTietDonHang);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_DonHangs_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_GiamGias_MaGiamGia",
                        column: x => x.MaGiamGia,
                        principalTable: "GiamGias",
                        principalColumn: "MaGiamGia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_SanPhams_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiaoHangs",
                columns: table => new
                {
                    MaGiaoHang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonHangMaDonHang = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoHangs", x => x.MaGiaoHang);
                    table.ForeignKey(
                        name: "FK_GiaoHangs_DonHangs_DonHangMaDonHang",
                        column: x => x.DonHangMaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang");
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonHangMaDonHang = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDons_DonHangs_DonHangMaDonHang",
                        column: x => x.DonHangMaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToans",
                columns: table => new
                {
                    MaThanhToan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhachHangMaKhachHang = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    MaDonHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonHangMaDonHang = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    PhuongThuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToans", x => x.MaThanhToan);
                    table.ForeignKey(
                        name: "FK_ThanhToans_DonHangs_DonHangMaDonHang",
                        column: x => x.DonHangMaDonHang,
                        principalTable: "DonHangs",
                        principalColumn: "MaDonHang");
                    table.ForeignKey(
                        name: "FK_ThanhToans_KhachHang_KhachHangMaKhachHang",
                        column: x => x.KhachHangMaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHangs",
                columns: table => new
                {
                    MaChiTietGioHang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaGioHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioHangMaGioHang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanPhamMaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHangs", x => x.MaChiTietGioHang);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHangs_GioHangs_GioHangMaGioHang",
                        column: x => x.GioHangMaGioHang,
                        principalTable: "GioHangs",
                        principalColumn: "MaGioHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHangs_SanPhams_SanPhamMaSanPham",
                        column: x => x.SanPhamMaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDons",
                columns: table => new
                {
                    MaChiTietHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoaDonMaHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanPhamMaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaThoiDiemMua = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDons", x => x.MaChiTietHoaDon);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_HoaDons_HoaDonMaHoaDon",
                        column: x => x.HoaDonMaHoaDon,
                        principalTable: "HoaDons",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_SanPhams_SanPhamMaSanPham",
                        column: x => x.SanPhamMaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGias",
                columns: table => new
                {
                    MaDanhGia = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhachHangMaKhachHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoaDonMaHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanPhamMaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoDiem = table.Column<int>(type: "int", nullable: false),
                    BinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGias", x => x.MaDanhGia);
                    table.ForeignKey(
                        name: "FK_DanhGias_HoaDons_HoaDonMaHoaDon",
                        column: x => x.HoaDonMaHoaDon,
                        principalTable: "HoaDons",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGias_KhachHang_KhachHangMaKhachHang",
                        column: x => x.KhachHangMaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGias_SanPhams_SanPhamMaSanPham",
                        column: x => x.SanPhamMaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_MaTaiKhoan",
                table: "Admins",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaos_AdminMaAdmin",
                table: "BaoCaos",
                column: "AdminMaAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_MaDonHang",
                table: "ChiTietDonHangs",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_MaGiamGia",
                table: "ChiTietDonHangs",
                column: "MaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_MaSanPham",
                table: "ChiTietDonHangs",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHangs_GioHangMaGioHang",
                table: "ChiTietGioHangs",
                column: "GioHangMaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHangs_SanPhamMaSanPham",
                table: "ChiTietGioHangs",
                column: "SanPhamMaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_HoaDonMaHoaDon",
                table: "ChiTietHoaDons",
                column: "HoaDonMaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_SanPhamMaSanPham",
                table: "ChiTietHoaDons",
                column: "SanPhamMaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPhams_MaDanhMuc",
                table: "ChiTietSanPhams",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPhams_MaKichThuoc",
                table: "ChiTietSanPhams",
                column: "MaKichThuoc");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPhams_MaMauSac",
                table: "ChiTietSanPhams",
                column: "MaMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPhams_MaSanPham",
                table: "ChiTietSanPhams",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGias_HoaDonMaHoaDon",
                table: "DanhGias",
                column: "HoaDonMaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGias_KhachHangMaKhachHang",
                table: "DanhGias",
                column: "KhachHangMaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGias_SanPhamMaSanPham",
                table: "DanhGias",
                column: "SanPhamMaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_MaKhachHang",
                table: "DonHangs",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoHangs_DonHangMaDonHang",
                table: "GiaoHangs",
                column: "DonHangMaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_KhachHangMaKhachHang",
                table: "GioHangs",
                column: "KhachHangMaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_DonHangMaDonHang",
                table: "HoaDons",
                column: "DonHangMaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_MaTaiKhoan",
                table: "KhachHang",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToans_DonHangMaDonHang",
                table: "ThanhToans",
                column: "DonHangMaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToans_KhachHangMaKhachHang",
                table: "ThanhToans",
                column: "KhachHangMaKhachHang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaoCaos");

            migrationBuilder.DropTable(
                name: "ChiTietDonHangs");

            migrationBuilder.DropTable(
                name: "ChiTietGioHangs");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDons");

            migrationBuilder.DropTable(
                name: "ChiTietSanPhams");

            migrationBuilder.DropTable(
                name: "DanhGias");

            migrationBuilder.DropTable(
                name: "GiaoHangs");

            migrationBuilder.DropTable(
                name: "ThanhToans");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "GiamGias");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "KichThuocs");

            migrationBuilder.DropTable(
                name: "MauSacs");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "TaiKhoan");
        }
    }
}
