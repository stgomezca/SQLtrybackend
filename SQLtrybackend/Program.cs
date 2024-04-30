using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SQLtrybackend.AutoMappers;
using SQLtrybackend.DTOs;
using SQLtrybackend.Models;
using SQLtrybackend.Repository;
using SQLtrybackend.Services;
using SQLtrybackend.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService > ("beerService");

builder.Services.AddScoped<IRepository<Beer>, BeerRepository> ();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// Validators

builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidation>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
