using System.ComponentModel.DataAnnotations;

namespace LjruiFoodWeb.ViewModels;

public class HoSoFormModel
{
    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    public string HoTen { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [StringLength(20, ErrorMessage = "Số điện thoại quá dài.")]
    public string? DienThoai { get; set; }

    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string? Email { get; set; }
}
