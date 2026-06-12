using System.ComponentModel.DataAnnotations;

namespace LjruiFoodWeb.ViewModels;

public class LienHeFormModel
{
    [Display(Name = "Nội dung tin nhắn")]
    [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
    [MinLength(5, ErrorMessage = "Nội dung quá ngắn (tối thiểu 5 ký tự).")]
    public string NoiDung { get; set; } = string.Empty;
}
