using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LjruiFoodWeb.Models;

public class ChiTietDonHang
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Ma { get; set; }

    [Required]
    public int DonHangMa { get; set; }
    public DonHang? DonHang { get; set; }

    [Required]
    public int SanPhamMa { get; set; }
    public SanPham? SanPham { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DonGia { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ThanhTien { get; set; }
}
