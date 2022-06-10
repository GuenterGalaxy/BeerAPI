using Domain;

namespace Application.Interfaces
{
    public interface IBeerHandler
    {
        Task<IEnumerable<Beer>> GetByPriceAsync(decimal price, string sourceUrl);
        Task<IEnumerable<Beer>> GetCheapestBeerAsync(string sourceUrl);
        Task<IEnumerable<Beer>> GetMostBottlesAsync(string sourceUrl);
        Task<IEnumerable<Beer>> GetMostExpensiveBeerAsync(string sourceUrl);
    }
}