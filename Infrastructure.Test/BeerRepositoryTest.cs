using FluentAssertions;

namespace Infrastructure.Test;

public class BeerRepositoryTest
{
    public BeerRepository beerRepo = new();

    [Fact]
    public async void GetAll_NotNull()
    {
    }
}