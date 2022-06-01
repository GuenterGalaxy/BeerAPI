using Domain;

namespace Application.Interfaces
{
    public interface IBeerHandler
    {
        Task<IEnumerable<Beer>> GetByPrice(decimal price, string sourceUrl);
        Task<IEnumerable<Beer>> GetCheapestBeer(string sourceUrl);
        Task<IEnumerable<Beer>> GetMostBottles(string sourceUrl);
        Task<IEnumerable<Beer>> GetMostExpensiveBeer(string sourceUrl);
    }
}