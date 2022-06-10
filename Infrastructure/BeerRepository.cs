using Application.Exceptions;
using Application.Interfaces;
using Domain;
using System.Text.Json;

namespace Infrastructure;

public class BeerRepository : IBeerRepository
{
    private readonly HttpClient _httpClient = new();

    public async Task<IEnumerable<Beer>> GetByUrl(string url)
    {
        try
        {
            var stream = await _httpClient.GetStreamAsync(url);
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var beers = await JsonSerializer.DeserializeAsync<IEnumerable<Beer>>(stream, jsonOptions) ?? new List<Beer>();
            return beers;
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidSourceUrlException("Source URL is not a valid URL.", ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidJsonException("The JSON from the source URL is in an invalid format.", ex);
        }
    }
}