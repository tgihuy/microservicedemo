using Microsoft.EntityFrameworkCore;
using ProductService2.Application.Database;
using ProductService2.Application.Repositories;
using ProductService2.Application.Repositories.Interface;
using ProductService2.Application.Services;
using ProductService2.Application.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOracle<ProductContext>(builder.Configuration.GetConnectionString("OracleConn"));
builder.Services.AddScoped<IProductRepositories, ProductRepositories>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();