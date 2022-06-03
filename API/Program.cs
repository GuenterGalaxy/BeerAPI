using Application;
using Application.Interfaces;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IBeerRepository, BeerRepository>();
builder.Services.AddSingleton<IBeerHandler, BeerHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    var response = new { error = exception?.Message };
    await context.Response.WriteAsJsonAsync(response);
}));

app.MapGet("beers/cheapest-and-most-expensive-per-litre", async ([FromServices] IBeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json((await beerHandler.GetCheapestBeer(sourceUrl)).Union(await beerHandler.GetMostExpensiveBeer(sourceUrl)));
});

app.MapGet("beers/priced-seventeen-ninety-nine-euro", async ([FromServices] IBeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(await beerHandler.GetByPrice(17.99M, sourceUrl));
});

app.MapGet("beers/most-bottles", async ([FromServices] IBeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(await beerHandler.GetMostBottles(sourceUrl));
});

app.MapGet("beers/get-all-endpoints", async ([FromServices] IBeerHandler beerHandler, [FromQuery] string sourceUrl) =>
{
    return Results.Json(new Dictionary<string, IEnumerable<Beer>>
    {
        { "cheapestAndMostExpensivePerLitre", (await beerHandler.GetCheapestBeer(sourceUrl)).Union(await beerHandler.GetMostExpensiveBeer(sourceUrl)) },
        { "seventeenNinetyNineEuroPerLitre", await beerHandler.GetByPrice(17.99M, sourceUrl) },
        { "mostBottles", await beerHandler.GetMostBottles(sourceUrl) }
    });
});

app.Run();