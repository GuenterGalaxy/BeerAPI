using System.Text.RegularExpressions;

namespace Domain;

public record Article(int Id, string ShortDescription, decimal Price, string Unit, string PricePerUnitText, string Image)
{
    public decimal? PricePerUnit
    {
        get => decimal.TryParse(Regex.Match(PricePerUnitText, @"(\d*,\d*)").Value, out decimal result) ? result : null;
    }

    public int? BottleAmount
    {
        get => int.TryParse(Regex.Match(ShortDescription, @"(\d+) x").Groups[1].Value, out int result) ? result : null;
    }
}