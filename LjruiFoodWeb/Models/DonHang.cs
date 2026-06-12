using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LjruiFoodWeb.Models;

public class DonHang
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Ma { get; set; }

    public int? NguoiDungMa { get; set; }
    public NguoiDung? NguoiDung { get; set; }

    [Required]
    [MaxLength(100)]
    public string HoTen { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string SoDienThoai { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Email { get; set; }

    [Required]
    [MaxLength(500)]
    public string DiaChiGiaoHang { get; set; } = string.Empty;

    public string? GhiChu { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TongTien { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; } = "ChoXacNhan";

    public DateTime NgayDatHang { get; set; }

    public DateTime NgayCapNhat { get; set; }

    public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}
