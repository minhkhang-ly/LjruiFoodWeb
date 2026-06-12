using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LjruiFoodWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DanhMucMa = table.Column<int>(type: "int", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MoTaDayDu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThanhPhan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NguonGoc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    QuyTrinhCheBien = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LoiIch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KhauPhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BaoQuan = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GiaHienTai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiaGoc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false),
                    SoLuongDaBan = table.Column<int>(type: "int", nullable: false),
                    SoSaoDanhGia = table.Column<int>(type: "int", nullable: false),
                    SoLuotDanhGia = table.Column<int>(type: "int", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Ma);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc_DanhMucMa",
                        column: x => x.DanhMucMa,
                        principalTable: "DanhMuc",
                        principalColumn: "Ma");
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungMa = table.Column<int>(type: "int", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDatHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.Ma);
                    table.ForeignKey(
                        name: "FK_DonHang_NguoiDung_NguoiDungMa",
                        column: x => x.NguoiDungMa,
                        principalTable: "NguoiDung",
                        principalColumn: "Ma");
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamMa = table.Column<int>(type: "int", nullable: false),
                    NguoiDungMa = table.Column<int>(type: "int", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoSao = table.Column<int>(type: "int", nullable: false),
                    BinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.Ma);
                    table.ForeignKey(
                        name: "FK_DanhGia_NguoiDung_NguoiDungMa",
                        column: x => x.NguoiDungMa,
                        principalTable: "NguoiDung",
                        principalColumn: "Ma");
                    table.ForeignKey(
                        name: "FK_DanhGia_SanPham_SanPhamMa",
                        column: x => x.SanPhamMa,
                        principalTable: "SanPham",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangMa = table.Column<int>(type: "int", nullable: false),
                    SanPhamMa = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.Ma);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang_DonHangMa",
                        column: x => x.DonHangMa,
                        principalTable: "DonHang",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_SanPham_SanPhamMa",
                        column: x => x.SanPhamMa,
                        principalTable: "SanPham",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_DonHangMa",
                table: "ChiTietDonHang",
                column: "DonHangMa");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_SanPhamMa",
                table: "ChiTietDonHang",
                column: "SanPhamMa");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_NguoiDungMa",
                table: "DanhGia",
                column: "NguoiDungMa");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_SanPhamMa",
                table: "DanhGia",
                column: "SanPhamMa");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_NguoiDungMa",
                table: "DonHang",
                column: "NguoiDungMa");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_DanhMucMa",
                table: "SanPham",
                column: "DanhMucMa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "DanhMuc");
        }
    }
}
