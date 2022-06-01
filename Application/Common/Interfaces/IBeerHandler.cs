using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IBeerHandler
    {
        Task<List<Beer>> GetByPrice(decimal price, string sourceUrl);
        Task<List<Beer>> GetCheapestBeerPerLitre(string sourceUrl);
        Task<List<Beer>> GetMostBottles(string sourceUrl);
        Task<List<Beer>> GetMostExpensiveBeerPerLitre(string sourceUrl);
    }
}