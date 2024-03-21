using Manonero.MessageBus.Kafka.Extensions;
using OrderServices.Application.Database;
using OrderServices.Application.Repositories;
using OrderServices.Application.Services;
using OrderServices.Repository;
using OrderServices.Setting;

var builder = WebApplication.CreateBuilder(args);
var appSetting = AppSetting.MapValue(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOracle<OrderDbContext>(builder.Configuration.GetConnectionString("OracleConn"));
builder.Services.AddScoped<IOrderRepositories, OrderRepositories>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerRepositories, CustomerRepositories>();
builder.Services.AddScoped<ICustomerServices,  CustomerServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddKafkaProducers(producerBuilder =>
{
    producerBuilder.AddProducer(appSetting.GetProducerSetting(builder.Configuration.GetSection("ProducerSettings:0:Id").Value));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseKafkaMessageBus(messageBus =>
{
    messageBus.RunConsumer(builder.Configuration.GetSection("ProducerSettings:0:Id").Value);
});

app.Run();
