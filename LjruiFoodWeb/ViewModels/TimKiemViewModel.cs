using LjruiFoodWeb.Models;

namespace LjruiFoodWeb.ViewModels;

public class TimKiemViewModel
{
    public string TuKhoa { get; set; } = string.Empty;
    public List<SanPham> KetQua { get; set; } = new List<SanPham>();
}
