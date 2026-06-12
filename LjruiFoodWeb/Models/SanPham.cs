using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LjruiFoodWeb.Models;

public class SanPham
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Ma { get; set; }

    [Required]
    [MaxLength(200)]
    public string Ten { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

    public int? DanhMucMa { get; set; }
    public DanhMuc? DanhMuc { get; set; }

    [MaxLength(500)]
    public string? HinhAnh { get; set; }

    [MaxLength(1000)]
    public string? MoTa { get; set; }

    public string? MoTaDayDu { get; set; }

    [MaxLength(500)]
    public string? ThanhPhan { get; set; }

    [MaxLength(200)]
    public string? NguonGoc { get; set; }

    [MaxLength(500)]
    public string? QuyTrinhCheBien { get; set; }

    [MaxLength(100)]
    public string? KhauPhan { get; set; }

    [MaxLength(200)]
    public string? BaoQuan { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal GiaHienTai { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? GiaGoc { get; set; }

    public int PhanTramGiam { get; set; }

    public int SoLuongDaBan { get; set; }

    public int SoSaoDanhGia { get; set; } = 5;

    public int SoLuotDanhGia { get; set; }

    public int SoLuongTon { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime NgayCapNhat { get; set; }

    public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    public ICollection<DanhGia> DanhGias { get; set; } = new List<DanhGia>();
    public ICollection<SanPhamHinhAnh> SanPhamHinhAnhs { get; set; } = new List<SanPhamHinhAnh>();
}
