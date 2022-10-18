using Microsoft.EntityFrameworkCore;
using HotelListing.Data;
using Serilog;
using HotelListing.Configurations;
using HotelListing.Contracts;
using HotelListing.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conStr = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
builder.Services.AddDbContext<HotelListingDbContext>(options =>
{
    options.UseSqlServer(conStr);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding Cors to the code
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

//lc stands for logger configuration
builder.Host.UseSerilog((context,lc)=> lc.WriteTo.Console().ReadFrom.Configuration(context.Configuration));

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
