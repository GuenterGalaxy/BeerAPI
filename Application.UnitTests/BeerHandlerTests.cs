using Application.Interfaces;
using Domain;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests
{
    public class BeerHandlerTests
    {
        private readonly BeerHandler _beerHandler;
        private readonly IBeerRepository _beerRepository = Substitute.For<IBeerRepository>();

        public BeerHandlerTests()
        {
            _beerHandler = new BeerHandler(_beerRepository);
        }

        [Fact]
        public async void GetByPrice_IsSortedByPricePerLitreAscending()
        {
            // Arrange
            var data = new List<Beer> 
            { 
                new Beer(0, "", "pricierBeer", new List<Article> 
                { 
                    new Article(0, "pricierArticle", 17.99M, "", "(2,80 €/Liter)", ""),
                    new Article(0, "cheaperArticle", 17.99M, "", "(1,80 €/Liter)", "") 
                }, ""),
                new Beer(0, "", "cheapestBeer", new List<Article>
                {
                    new Article(0, "pricierArticle", 17.99M, "", "(0,80 €/Liter)", ""),
                    new Article(0, "cheaperArticle", 17.99M, "", "(0,60 €/Liter)", "")
                }, "")
            };

            _beerRepository.GetByUrl("").Returns(data);

            // Act
            var beersByPrice = await _beerHandler.GetByPrice(17.99M, "");

            // Assert
            foreach (var beer in beersByPrice)
            {
                beer.Articles.Select(article => article.PricePerUnit).Should().BeInAscendingOrder();
            }
        }
    }
}