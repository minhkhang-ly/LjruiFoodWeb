namespace TaloFoodWeb.ViewModels;

public class ProductViewModel
{
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
}
