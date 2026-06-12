using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.ViewModels;

public class SanPhamViewModel
{
    public int Ma { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
    [MaxLength(200)]
    public string Ten { get; set; } = string.Empty;

    [Required(ErrorMessage = "Danh mục là bắt buộc")]
    public int? DanhMucMa { get; set; }

    public IFormFile? HinhAnhUpload { get; set; }
    
    public string? HinhAnhHienTai { get; set; }

    public string? MoTa { get; set; }

    [Required(ErrorMessage = "Giá hiện tại là bắt buộc")]
    public decimal GiaHienTai { get; set; }

    public decimal? GiaGoc { get; set; }

    [Required(ErrorMessage = "Số lượng tồn là bắt buộc")]
    public int SoLuongTon { get; set; }
}
