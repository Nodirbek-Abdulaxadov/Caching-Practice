using CountriesAPI.BusinessLogicLayer.Interfaces;
using CountriesAPI.BusinessLogicLayer.Service;
using CountriesAPI.DataLayer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConfigurationOptions>(
        builder.Configuration.GetSection("RedisCacheOptions"));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
    options.InstanceName = "CountriesAPI";
});

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue;
});

//Add Response Caching
//builder.Services.AddResponseCaching();

//Add Output Caching to Services
//builder.Services.AddOutputCache();

//Add Db Context
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration
                                .GetConnectionString("LocalDB")));
builder.Services.AddTransient<ICountryInterface, CountryService>();
builder.Services.AddTransient<IRedisService, RedisService>();
builder.Services.AddTransient<IFileService, FileService>(); 

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();

//Add Response Caching to Middleware
//app.UseResponseCaching();

//Add Output Caching to Middleware
//app.UseOutputCache();

app.MapControllers();

app.Run();