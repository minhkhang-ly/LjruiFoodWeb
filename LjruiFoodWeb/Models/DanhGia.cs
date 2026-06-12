using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LjruiFoodWeb.Models;

public class DanhGia
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Ma { get; set; }

    [Required]
    public int SanPhamMa { get; set; }
    public SanPham? SanPham { get; set; }

    public int? NguoiDungMa { get; set; }
    public NguoiDung? NguoiDung { get; set; }

    [Required]
    [MaxLength(100)]
    public string HoTen { get; set; } = string.Empty;

    public int SoSao { get; set; } = 5;

    public string? BinhLuan { get; set; }

    public DateTime NgayDanhGia { get; set; }
}
