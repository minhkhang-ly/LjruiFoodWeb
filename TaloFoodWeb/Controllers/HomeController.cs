using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaloFoodWeb.Models;
using TaloFoodWeb.ViewModels;

namespace TaloFoodWeb.Controllers;

public class HomeController : Controller
{
    private static readonly IReadOnlyList<ProductViewModel> MenuProducts =
    [
        new()
        {
            Name = "Pizza Margherita",
            CategoryLabel = "ĐỒ ĂN",
            ImageUrl = "/images/food-pizza.svg",
            Description = "Pizza Cổ Điển Với Sốt Cà Chua, Phô Mai Mozzarella Và Húng Quế Tươi.",
            CurrentPrice = 95_000m,
            OriginalPrice = 100_000m,
            DiscountPercent = 5,
            SoldCount = 25,
            StarRating = 4,
            ReviewCount = 4
        },
        new()
        {
            Name = "Bún Bò Huế",
            CategoryLabel = "ĐỒ ĂN",
            ImageUrl = "/images/food-bun-bo.svg",
            Description = "Món Bún Nổi Tiếng Của Miền Trung Với Nước Dùng Đậm Đà.",
            CurrentPrice = 48_000m,
            OriginalPrice = 50_000m,
            DiscountPercent = 4,
            SoldCount = 2,
            StarRating = 5,
            ReviewCount = 1
        },
        new()
        {
            Name = "Hamburger Bò",
            CategoryLabel = "ĐỒ ĂN",
            ImageUrl = "/images/food-hamburger.svg",
            Description = "Bánh Hamburger Kẹp Bò Với Rau Và Phô Mai.",
            CurrentPrice = 75_000m,
            OriginalPrice = 80_000m,
            DiscountPercent = 6,
            SoldCount = 25,
            StarRating = 4,
            ReviewCount = 4
        }
    ];

    private static readonly IReadOnlyList<ReviewViewModel> FeaturedReviews =
    [
        new()
        {
            UserName = "Test",
            ProductName = "Pizza Margherita",
            Comment = "Ngon",
            StarRating = 5,
            ReviewDate = "13/11/2025",
            AvatarInitial = "T"
        },
        new()
        {
            UserName = "Longpro03",
            ProductName = "Bún Bò Huế",
            Comment = "Bún Bò Huế Ăn Rất Là Oke",
            StarRating = 5,
            ReviewDate = "11/05/2025",
            AvatarInitial = "L"
        },
        new()
        {
            UserName = "Longpro03",
            ProductName = "Pizza Margherita",
            Comment = "Ngon Quá Tui Mua 5 Cái Về Ăn",
            StarRating = 5,
            ReviewDate = "08/05/2025",
            AvatarInitial = "L"
        }
    ];

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Menu()
    {
        return View(MenuProducts.ToList());
    }

    public IActionResult Review()
    {
        return View(FeaturedReviews.ToList());
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Blog()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
