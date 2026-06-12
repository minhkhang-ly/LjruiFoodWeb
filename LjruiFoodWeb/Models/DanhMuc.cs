using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LjruiFoodWeb.Models;

public class DanhMuc
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Ma { get; set; }

    [Required]
    [MaxLength(100)]
    public string Ten { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? MoTa { get; set; }

    public ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
