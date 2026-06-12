namespace LjruiFoodWeb.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    /// <summary>Nhóm lọc: DoAn hoặc DoUong.</summary>
    public string Nhom { get; set; } = "DoAn";

    public string Name { get; set; } = string.Empty;
    public string CategoryLabel { get; set; } = "ĐỒ ĂN";
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public decimal? OriginalPrice { get; set; }
    public int? DiscountPercent { get; set; }
    public int SoldCount { get; set; }
    public int StarRating { get; set; } = 5;
    public int ReviewCount { get; set; }

    /// <summary>Mô tả đầy đủ cho trang chi tiết.</summary>
    public string MoTaDayDu { get; set; } = string.Empty;

    /// <summary>Danh sách thành phần (một dòng hoặc nhiều dòng).</summary>
    public string ThanhPhan { get; set; } = string.Empty;

    /// <summary>Nguồn gốc nguyên liệu / xuất xứ món.</summary>
    public string NguonGoc { get; set; } = string.Empty;

    /// <summary>Cách chế biến / quy trình ngắn.</summary>
    public string QuyTrinhCheBien { get; set; } = string.Empty;

    /// <summary>Khẩu phần / dung tích.</summary>
    public string KhauPhan { get; set; } = string.Empty;

    /// <summary>Gợi ý bảo quản.</summary>
    public string BaoQuan { get; set; } = string.Empty;
}
