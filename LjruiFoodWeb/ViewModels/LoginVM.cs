using System.ComponentModel.DataAnnotations;

namespace LjruiFoodWeb.ViewModels;

public class LoginVM
{
    [Display(Name = "Tên đăng nhập / Email")]
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập hoặc email.")]
    public string UserNameOrEmail { get; set; } = string.Empty;

    [Display(Name = "Mật khẩu")]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [DataType(DataType.Password)]
    public string MatKhau { get; set; } = string.Empty;

    [Display(Name = "Ghi nhớ đăng nhập")]
    public bool GhiNho { get; set; }
}
