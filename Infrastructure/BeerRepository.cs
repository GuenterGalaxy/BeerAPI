using Application.Interfaces;
using Domain;
using System.Text.Json;

namespace Infrastructure;

#nullable disable

public class BeerRepository : IBeerRepository
{
    private readonly HttpClient _httpClient = new();

    public async Task<IEnumerable<Beer>> GetByUrl(string url)
    {
        var stream = await _httpClient.GetStreamAsync(url);
        return await JsonSerializer.DeserializeAsync<IEnumerable<Beer>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); ;
    }
}