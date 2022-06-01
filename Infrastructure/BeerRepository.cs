using Application.Common.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace Infrastructure;

#nullable disable

public class BeerRepository : IBeerRepository
{
    private readonly HttpClient _httpClient = new();

    public async Task<List<Beer>> GetByUrl(string url)
    {
        var stream = await _httpClient.GetStreamAsync(url);
        return await JsonSerializer.DeserializeAsync<List<Beer>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); ;
    }
}