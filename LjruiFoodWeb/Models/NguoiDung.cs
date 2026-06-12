using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LjruiFoodWeb.Models;

public class NguoiDung : IdentityUser<int>
{
    [MaxLength(100)]
    public override string? Email { get; set; }

    [MaxLength(200)]
    public override string? NormalizedEmail { get; set; }

    [MaxLength(500)]
    public string? DiaChi { get; set; }

    [MaxLength(100)]
    public string HoTen { get; set; } = string.Empty;

    public string? GhiChu { get; set; }

    public DateTime NgayTao { get; set; }

    public ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
    public ICollection<DanhGia> DanhGias { get; set; } = new List<DanhGia>();
}
