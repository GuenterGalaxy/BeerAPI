using Application.Interfaces;
using Domain;

namespace Application;

public class BeerHandler : IBeerHandler // love the name
{
    private readonly IBeerRepository _beerRepository;

    public BeerHandler(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<IEnumerable<Beer>> GetCheapestBeerAsync(string sourceUrl)
    {
        var beers = (List<Beer>)await _beerRepository.GetByUrl(sourceUrl);
        var minPricePerUnit = beers
            .SelectMany(beer => beer.Articles)
            .Min(beer => beer.PricePerUnit);

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

    public async Task<IEnumerable<Beer>> GetMostExpensiveBeerAsync(string sourceUrl)
    {
        var beers = (List<Beer>)await _beerRepository.GetByUrl(sourceUrl);
        var minPricePerUnit = beers
            .SelectMany(beer => beer.Articles)
            .Max(beer => beer.PricePerUnit);

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

    public async Task<IEnumerable<Beer>> GetByPriceAsync(decimal price, string sourceUrl)
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

            beer.Articles.Sort((currentArticle, nextArticle) => Nullable.Compare(currentArticle.PricePerUnit, nextArticle.PricePerUnit));
        }

        beers.Sort((currentBeer, nextBeer) => Nullable.Compare( currentBeer.Articles.Min(article => article.PricePerUnit), 
                                                                nextBeer.Articles.Min(article => article.PricePerUnit)));

        return beers;
    }

    public async Task<IEnumerable<Beer>> GetMostBottlesAsync(string sourceUrl)
    {
        var beers = (List<Beer>)await _beerRepository.GetByUrl(sourceUrl);
        var highestBottleAmount = beers
            .SelectMany(beer => beer.Articles)
            .Max(article => article.BottleAmount);

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
