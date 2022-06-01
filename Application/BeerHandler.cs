using Application.Common.Interfaces;
using Domain.Entities;

namespace Application;

#nullable disable

public class BeerHandler : IBeerHandler // love the name
{
    private readonly IBeerRepository _beerRepository;

    public BeerHandler(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<List<Beer>> GetCheapestBeerPerLitre(string sourceUrl)
    {
        var beers = await _beerRepository.GetByUrl(sourceUrl);
        var minPricePerUnit = beers.SelectMany(beer => beer.Articles).Min(x => x.PricePerUnit);
        for (int i = beers.Count - 1; i >= 0; i--)
        {
            var beer = beers[i];

            beer.Articles.RemoveAll(article => article.PricePerUnit != minPricePerUnit);

            if (beer.Articles.Count == 0)
            {
                beers.Remove(beer);
            }
        }

        return beers;
    }

    public async Task<List<Beer>> GetMostExpensiveBeerPerLitre(string sourceUrl)
    {
        var beers = await _beerRepository.GetByUrl(sourceUrl);
        var minPricePerUnit = beers.SelectMany(beer => beer.Articles).Max(x => x.PricePerUnit);
        for (int i = beers.Count - 1; i >= 0; i--)
        {
            var beer = beers[i];

            beer.Articles.RemoveAll(article => article.PricePerUnit != minPricePerUnit);

            if (beer.Articles.Count == 0)
            {
                beers.Remove(beer);
            }
        }

        return beers;
    }

    public async Task<List<Beer>> GetByPrice(decimal price, string sourceUrl)
    {
        var beers = (List<Beer>)await _beerRepository.GetByUrl(sourceUrl);

        for (int i = beers.Count - 1; i >= 0; i--)
        {
            var beer = beers[i];

            beer.Articles.RemoveAll(article => article.Price != price);

            if (beer.Articles.Count == 0)
            {
                beers.Remove(beer);
                continue;
            }

            beer.Articles.Sort((x, y) => Nullable.Compare(x.PricePerUnit, y.PricePerUnit));
        }

        beers.Sort((x, y) => Nullable.Compare(x.Articles.Min(article => article.PricePerUnit), y.Articles.Min(article => article.PricePerUnit)));

        return beers;
    }

    public async Task<List<Beer>> GetMostBottles(string sourceUrl)
    {
        var beers = await _beerRepository.GetByUrl(sourceUrl);
        var highestBottleAmount = beers.SelectMany(beer => beer.Articles).Max(article => article.BottleAmount);

        for (int i = beers.Count - 1; i >= 0; i--)
        {
            var beer = beers[i];

            beer.Articles.RemoveAll(article => article.BottleAmount != highestBottleAmount);

            if (beer.Articles.Count == 0)
            {
                beers.Remove(beer);
            }
        }

        return beers;
    }
}
