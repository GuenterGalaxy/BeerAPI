using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IBeerRepository
{
    Task<List<Beer>> GetByUrl(string url);
}