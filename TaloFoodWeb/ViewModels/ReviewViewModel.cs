namespace TaloFoodWeb.ViewModels;

public class ReviewViewModel
{
    public string UserName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int StarRating { get; set; } = 5;
    public string ReviewDate { get; set; } = string.Empty;
    public string AvatarInitial { get; set; } = "?";
}
