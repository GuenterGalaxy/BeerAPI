using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(builder => builder.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    var response = new { error = exception?.Message };
    await context.Response.WriteAsJsonAsync(response);
}));

app.MapGet("beers/cheapest-and-most-expensive-per-litre", async ([FromServices] IBeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(await CombineCheapestAndMostExpensiveBeer(beerHandler, sourceUrl));
});

app.MapGet("beers/priced-seventeen-ninety-nine-euro", async ([FromServices] IBeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(await beerHandler.GetByPriceAsync(17.99M, sourceUrl));
});

app.MapGet("beers/most-bottles", async ([FromServices] IBeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(await beerHandler.GetMostBottlesAsync(sourceUrl));
});

app.MapGet("beers/get-all-endpoints", async ([FromServices] IBeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(new Dictionary<string, IEnumerable<Beer>>
    {
        { "cheapestAndMostExpensivePerLitre", await CombineCheapestAndMostExpensiveBeer(beerHandler, sourceUrl) },
        { "seventeenNinetyNineEuroPerLitre", await beerHandler.GetByPriceAsync(17.99M, sourceUrl) },
        { "mostBottles", await beerHandler.GetMostBottlesAsync(sourceUrl) }
    });
});

app.Run();

async Task<IEnumerable<Beer>> CombineCheapestAndMostExpensiveBeer(IBeerHandler beerHandler, string sourceUrl)
{
    return (await beerHandler.GetCheapestBeerAsync(sourceUrl)).Union(await beerHandler.GetMostExpensiveBeerAsync(sourceUrl));
}