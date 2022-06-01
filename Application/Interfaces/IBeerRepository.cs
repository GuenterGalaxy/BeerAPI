using Domain;

namespace Application.Interfaces;

public interface IBeerRepository
{
    Task<IEnumerable<Beer>> GetByUrl(string url);
}