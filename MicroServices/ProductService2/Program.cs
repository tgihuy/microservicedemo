using Manonero.MessageBus.Kafka.Extensions;
using Microsoft.EntityFrameworkCore;
using ProductService2.Application.Database;
using ProductService2.Application.Repositories;
using ProductService2.Application.Repositories.Interface;
using ProductService2.Application.Services;
using ProductService2.Application.Services.Interface;
using ProductService2.BackgroundTasks;
using ProductService2.Setting;
using static Confluent.Kafka.ConfigPropertyNames;

var builder = WebApplication.CreateBuilder(args);
var consumerId = builder.Configuration.GetSection("ConsumerSettings:0:Id").Value;
var appSetting = AppSetting.MapValue(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddOracle<ProductContext>(builder.Configuration.GetConnectionString("OracleConn"));
builder.Services.AddScoped<IProductRepositories, ProductRepositories>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKafkaConsumers(ConsumerBuilder =>
{
    ConsumerBuilder.AddConsumer<ConsumerBackgroundTasks>(appSetting.GetConsumerSetting(consumerId));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseKafkaMessageBus(messageBus =>
{
    messageBus.RunConsumer(consumerId);
}
);

app.Run();