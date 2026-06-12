using System.Globalization;

namespace LjruiFoodWeb.Helpers;

public static class VndFormat
{
    private static readonly CultureInfo DisplayCulture = CultureInfo.GetCultureInfo("de-DE");

    public static string ToVnd(this decimal amount) => amount.ToString("N0", DisplayCulture) + "đ";
}
