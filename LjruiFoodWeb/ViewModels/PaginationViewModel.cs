namespace LjruiFoodWeb.ViewModels;

public class PaginationViewModel
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; } = 6;
    public string? DanhMuc { get; set; }
    public string? SapXep { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public int FromItem => (CurrentPage - 1) * PageSize + 1;
    public int ToItem => Math.Min(CurrentPage * PageSize, TotalItems);
}
