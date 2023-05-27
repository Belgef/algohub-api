namespace AlgoHub.BLL;

public static class Extensions
{
    public static int? ParseIntOrNull(this string? str) => int.TryParse(str, out int res) ? res : null;
    public static double? ParseDoubleOrNull(this string? str) => double.TryParse(str, out double res) ? res : null;
}