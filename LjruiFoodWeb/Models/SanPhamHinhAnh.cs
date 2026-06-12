using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LjruiFoodWeb.Models;

public class SanPhamHinhAnh
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string DuongDan { get; set; } = string.Empty;

    public int SanPhamMa { get; set; }

    [ForeignKey("SanPhamMa")]
    public SanPham? SanPham { get; set; }
}
