namespace LjruiFoodWeb.ViewModels;

public class MenuPageViewModel
{
    public IReadOnlyList<ProductViewModel> SanPham { get; set; } = [];
    public string DanhMuc { get; set; } = "tatca";
    public string SapXep { get; set; } = "mac-dinh";
}
