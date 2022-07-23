using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theft.Service.Mappers;
using System.Reflection;
using Theft.Api.Configuration;
using Theft.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("TheftDb"));
builder.Services.AddControllers();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient("BikeHttpClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("BikeIndexBaseUrl").Value);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "BikeIndexClient");
}).SetHandlerLifetime(TimeSpan.FromMinutes(1));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml"));
});

var AllowSpecificOrigins = "_AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://localhost:3001")
                          .AllowAnyHeader().AllowAnyMethod();
                      });
});

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Configure dependencies
builder.Services.ConfigureDependencies();

builder.Services.AddHttpContextAccessor();
builder.Services.SeedData();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
