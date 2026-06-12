namespace LjruiFoodWeb.ViewModels;

public class ProductListViewModel
{
    public List<ProductViewModel> Products { get; set; } = [];
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalProducts { get; set; }
    public string DanhMuc { get; set; } = "tatca";
    public string SapXep { get; set; } = "mac-dinh";

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public int StartItem => (CurrentPage - 1) * PageSize + 1;
    public int EndItem => Math.Min(CurrentPage * PageSize, TotalProducts);
}
