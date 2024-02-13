using WorldMinimalRESTapi;
using WorldViewLibrary.Services;
using WorldViewLibrary.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// hente config data fra appsettings.json
var worldDataConfig = builder.Configuration.GetSection("WorldDataConfig");

// knytte sammen config klassen vi laget med config data ( NAVN VIKTIG !!!)
builder.Services.Configure<WorldServiceConfig>(worldDataConfig);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// vi register i DI (depencency injection) container
builder.Services.AddSingleton<IWorldDataService, WorldDataService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// endpunkt
//https://localhost:7181/helloworld
app.MapGet("/helloworld", HelloWorld).WithName("HelloWorld").WithOpenApi();

//https://localhost:7181/cities

app.MapGet("/cities", GetCities).WithName("GetCities").WithOpenApi();


// https://localhost:7181/cities/34
app.MapGet("/cities/{cityId}", (
    [FromServices] IWorldDataService worldService,
    [FromServices] IOptions<WorldServiceConfig> config,
    [FromRoute] int cityId) => {


    if (!worldService.IsLoaded)
        worldService.LoadData(config.Value.CountryFileName,
            config.Value.CityFileName, config.Value.CountryLanguageFileName);

    var city = worldService.GetCityById(cityId);

        if (city.Id == 0)
            return Results.NotFound($"Fant ingen by med id = {cityId}");

        return Results.Ok(city);
    
}).WithName("GetCityWithIdOne").WithOpenApi();

// https://localhost:7181/countires?name=norway
app.MapGet("/countries", (
    [FromServices] IWorldDataService worldService,
    [FromServices] IOptions<WorldServiceConfig> config,
    [FromQuery] string? name) =>
{
    if(!worldService.IsLoaded)
        worldService.LoadData(config.Value.CountryFileName,
            config.Value.CityFileName, config.Value.CountryLanguageFileName);

    // value = test ? true : false
    var restValue = name is null
    ? worldService.Countries
    : worldService.GetCountriesByName(name);

    if (restValue.Any())
        return Results.Ok(restValue);

    return Results.NotFound("Fant ingen land");

}).WithName("GetCountries").WithOpenApi();

app.Run();


static IResult HelloWorld()
{
    return Results.Ok("Hello world");
}

static IResult GetCities([FromServices] IWorldDataService worldService,
    [FromServices] IOptions<WorldServiceConfig> config)
{

    // her kommer koden for å hente ut cities
    // vi har registerert i IWorldDataService i container- og vi trenger den nå

    if (!worldService.IsLoaded)
    {
        worldService.LoadData(config.Value.CountryFileName,
            config.Value.CityFileName, config.Value.CountryLanguageFileName);
    }

    var cities = worldService.Cities;

    if (cities.Any())
        return Results.Ok(worldService.Cities);

    return Results.NotFound("Fant ingen byer");
}