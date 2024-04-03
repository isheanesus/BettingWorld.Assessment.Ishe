using BettingWorld.Assessment.Ishe.API.Data;
using BettingWorld.Assessment.Ishe.API.Helpers;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using BettingWorld.Assessment.Ishe.API.Models;
using BettingWorld.Assessment.Ishe.API.Repositories;
using BettingWorld.Assessment.Ishe.API.Services;
using freecurrencyapi;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
string apiKey = builder.Configuration["CurrencyApiKey"];
string conn = builder.Configuration.GetConnectionString("Database");
string cacheConn = builder.Configuration.GetConnectionString("Cache");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Freecurrencyapi>(_ => new Freecurrencyapi(apiKey));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<CurrencyRatesHistory>, RatesHistoryRepository>();
builder.Services.AddScoped<IRatesService, RatesService>();
builder.Services.AddScoped<IExternalRatesService, ExternalRateService>();
builder.Services.AddScoped<IConversionService, ConversionService>();

builder.Services.AddDbContext<RatesContext>(options => options.UseMySQL(conn));
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = cacheConn);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ApplyMigrations();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
