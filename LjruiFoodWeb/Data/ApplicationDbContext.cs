using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.Data;

public class ApplicationDbContext : IdentityDbContext<NguoiDung, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SanPham> SanPhams { get; set; }
    public DbSet<DanhMuc> DanhMucs { get; set; }
    public DbSet<DonHang> DonHangs { get; set; }
    public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
    public DbSet<DanhGia> DanhGias { get; set; }
    public DbSet<SanPhamHinhAnh> SanPhamHinhAnhs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SanPham>().ToTable("SanPham");
        modelBuilder.Entity<DanhMuc>().ToTable("DanhMuc");
        modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
        modelBuilder.Entity<DonHang>().ToTable("DonHang");
        modelBuilder.Entity<ChiTietDonHang>().ToTable("ChiTietDonHang");
        modelBuilder.Entity<DanhGia>().ToTable("DanhGia");
        modelBuilder.Entity<SanPhamHinhAnh>().ToTable("SanPhamHinhAnh");

        modelBuilder.Entity<NguoiDung>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<DonHang>()
            .HasOne(d => d.NguoiDung)
            .WithMany(n => n.DonHangs)
            .HasForeignKey(d => d.NguoiDungMa)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ChiTietDonHang>()
            .HasOne(c => c.DonHang)
            .WithMany(d => d.ChiTietDonHangs)
            .HasForeignKey(c => c.DonHangMa)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChiTietDonHang>()
            .HasOne(c => c.SanPham)
            .WithMany(s => s.ChiTietDonHangs)
            .HasForeignKey(c => c.SanPhamMa)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DanhGia>()
            .HasOne(dg => dg.NguoiDung)
            .WithMany(n => n.DanhGias)
            .HasForeignKey(dg => dg.NguoiDungMa)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DanhGia>()
            .HasOne(dg => dg.SanPham)
            .WithMany(s => s.DanhGias)
            .HasForeignKey(dg => dg.SanPhamMa)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SanPhamHinhAnh>()
            .HasOne(h => h.SanPham)
            .WithMany(s => s.SanPhamHinhAnhs)
            .HasForeignKey(h => h.SanPhamMa)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
