using System.ComponentModel.DataAnnotations;

namespace LjruiFoodWeb.ViewModels;

public class GioHangItemViewModel
{
    public string Ma { get; set; } = string.Empty;
    public string Ten { get; set; } = string.Empty;
    public decimal DonGia { get; set; }
    public int SoLuong { get; set; }
    public decimal ThanhTien => DonGia * SoLuong;
}

public class ThanhToanVM
{
    [Display(Name = "Họ và tên người nhận")]
    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [MaxLength(100)]
    public string HoTen { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
    [MaxLength(20)]
    public string SoDienThoai { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    [MaxLength(200)]
    public string? Email { get; set; }

    [Display(Name = "Địa chỉ giao hàng")]
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng.")]
    [MaxLength(500)]
    public string DiaChiGiaoHang { get; set; } = string.Empty;

    [Display(Name = "Ghi chú")]
    [MaxLength(500)]
    public string? GhiChu { get; set; }

    public List<GioHangItemViewModel> GioHang { get; set; } = [];
    public decimal TongTien { get; set; }

    public string? DiaChi { get; set; }
}
