using Application;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IBeerRepository, BeerRepository>();
builder.Services.AddSingleton<BeerHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("beers/cheapest-and-most-expensive-per-litre", async ([FromServices] BeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json((await beerHandler.GetCheapestBeerPerLitre(sourceUrl)).Union(await beerHandler.GetMostExpensiveBeerPerLitre(sourceUrl)));
});

app.MapGet("beers/priced-seventeen-ninety-nine-euro", async ([FromServices] BeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(await beerHandler.GetByPrice(17.99M, sourceUrl));
});

app.MapGet("beers/most-bottles", async ([FromServices] BeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(await beerHandler.GetMostBottles(sourceUrl));
});

app.MapGet("beers/get-all-endpoints", async ([FromServices] BeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(new Dictionary<string, IEnumerable<Beer>>
    {
        { "cheapestAndMostExpensivePerLitre", (await beerHandler.GetCheapestBeerPerLitre(sourceUrl)).Union(await beerHandler.GetMostExpensiveBeerPerLitre(sourceUrl)) },
        { "seventeenNinetyNineEuroPerLitre", await beerHandler.GetByPrice(17.99M, sourceUrl) },
        { "mostBottles", await beerHandler.GetMostBottles(sourceUrl) }
    });
});

app.Run();