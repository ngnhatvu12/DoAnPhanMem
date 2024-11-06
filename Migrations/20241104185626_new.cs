using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnPhanMem.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    MaDanhMuc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.MaDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "GiamGia",
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
                    table.PrimaryKey("PK_GiamGia", x => x.MaGiamGia);
                });

            migrationBuilder.CreateTable(
                name: "KichThuoc",
                columns: table => new
                {
                    MaKichThuoc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenKichThuoc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KichThuoc", x => x.MaKichThuoc);
                });

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

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    MaMauSac = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenMauSac = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSac", x => x.MaMauSac);
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
                name: "SanPham",
                columns: table => new
                {
                    MaSanPham = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaDanhMuc = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiaGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaLoai = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSanPham);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc_MaDanhMuc",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "MaDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_Loai_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "Loai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admina",
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
                    table.PrimaryKey("PK_Admina", x => x.MaAdmin);
                    table.ForeignKey(
                        name: "FK_Admina_TaiKhoan_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    MaGioHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaTaiKhoan = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.MaGioHang);
                    table.ForeignKey(
                        name: "FK_GioHang_TaiKhoan_MaTaiKhoan",
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
                name: "ChiTietSanPham",
                columns: table => new
                {
                    MaChiTietSP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    HinhAnhBienThe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaKichThuoc = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaMauSac = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietSanPham", x => x.MaChiTietSP);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_KichThuoc_MaKichThuoc",
                        column: x => x.MaKichThuoc,
                        principalTable: "KichThuoc",
                        principalColumn: "MaKichThuoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_MauSac_MaMauSac",
                        column: x => x.MaMauSac,
                        principalTable: "MauSac",
                        principalColumn: "MaMauSac",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_SanPham_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPham",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoCao",
                columns: table => new
                {
                    MaBaoCao = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaAdmin = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCao", x => x.MaBaoCao);
                    table.ForeignKey(
                        name: "FK_BaoCao_Admina_MaAdmin",
                        column: x => x.MaAdmin,
                        principalTable: "Admina",
                        principalColumn: "MaAdmin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachYeuThich",
                columns: table => new
                {
                    MaYeuThich = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachYeuThich", x => x.MaYeuThich);
                    table.ForeignKey(
                        name: "FK_DanhSachYeuThich_KhachHang_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhSachYeuThich_SanPham_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPham",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
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
                    table.PrimaryKey("PK_DonHang", x => x.MaDonHang);
                    table.ForeignKey(
                        name: "FK_DonHang_KhachHang_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHang",
                columns: table => new
                {
                    MaChiTietGioHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaGioHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaChiTietSP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHang", x => x.MaChiTietGioHang);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_ChiTietSanPham_MaChiTietSP",
                        column: x => x.MaChiTietSP,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "MaChiTietSP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_GioHang_MaGioHang",
                        column: x => x.MaGioHang,
                        principalTable: "GioHang",
                        principalColumn: "MaGioHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    MaChiTietDonHang = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaChiTietSP = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaGiamGia = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.MaChiTietDonHang);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_ChiTietSanPham_MaChiTietSP",
                        column: x => x.MaChiTietSP,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "MaChiTietSP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_GiamGia_MaGiamGia",
                        column: x => x.MaGiamGia,
                        principalTable: "GiamGia",
                        principalColumn: "MaGiamGia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiaoHang",
                columns: table => new
                {
                    MaGiaoHang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoHang", x => x.MaGiaoHang);
                    table.ForeignKey(
                        name: "FK_GiaoHang_DonHang_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDon_DonHang_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    MaThanhToan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    PhuongThuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan", x => x.MaThanhToan);
                    table.ForeignKey(
                        name: "FK_ThanhToan_DonHang_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    MaChiTietHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaThoiDiemMua = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDon", x => x.MaChiTietHoaDon);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_HoaDon_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_SanPham_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPham",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    MaDanhGia = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSanPham = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SoDiem = table.Column<int>(type: "int", nullable: false),
                    BinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.MaDanhGia);
                    table.ForeignKey(
                        name: "FK_DanhGia_HoaDon_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGia_SanPham_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPham",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admina_MaTaiKhoan",
                table: "Admina",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCao_MaAdmin",
                table: "BaoCao",
                column: "MaAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaChiTietSP",
                table: "ChiTietDonHang",
                column: "MaChiTietSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaDonHang",
                table: "ChiTietDonHang",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaGiamGia",
                table: "ChiTietDonHang",
                column: "MaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_MaChiTietSP",
                table: "ChiTietGioHang",
                column: "MaChiTietSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_MaGioHang",
                table: "ChiTietGioHang",
                column: "MaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_MaHoaDon",
                table: "ChiTietHoaDon",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_MaSanPham",
                table: "ChiTietHoaDon",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaKichThuoc",
                table: "ChiTietSanPham",
                column: "MaKichThuoc");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaMauSac",
                table: "ChiTietSanPham",
                column: "MaMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_MaSanPham",
                table: "ChiTietSanPham",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_MaHoaDon",
                table: "DanhGia",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_MaSanPham",
                table: "DanhGia",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachYeuThich_MaKhachHang",
                table: "DanhSachYeuThich",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachYeuThich_MaSanPham",
                table: "DanhSachYeuThich",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaKhachHang",
                table: "DonHang",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoHang_MaDonHang",
                table: "GiaoHang",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_MaTaiKhoan",
                table: "GioHang",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaDonHang",
                table: "HoaDon",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_MaTaiKhoan",
                table: "KhachHang",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaDanhMuc",
                table: "SanPham",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaLoai",
                table: "SanPham",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_MaDonHang",
                table: "ThanhToan",
                column: "MaDonHang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaoCao");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "ChiTietGioHang");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "DanhSachYeuThich");

            migrationBuilder.DropTable(
                name: "GiaoHang");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "Admina");

            migrationBuilder.DropTable(
                name: "GiamGia");

            migrationBuilder.DropTable(
                name: "ChiTietSanPham");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "KichThuoc");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "Loai");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "TaiKhoan");
        }
    }
}
