using BasketServices.Application.Repositories;
using BasketServices.Application.Repositories.Interface;
using BasketServices.Application.Services;
using BasketServices.Application.Services.Interface;
using Microservices.Application.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOracle<ProductDbContext>(builder.Configuration.GetConnectionString("OracleConn"));
builder.Services.AddScoped<ICustomerBasketRepositories, CustomerBasketRepositories>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
