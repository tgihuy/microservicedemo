using System.Text.Json;
using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using ProductService2.Application.DTOs;
using ProductService2.Application.Services.Interface;

namespace ProductService2.BackgroundTasks
{
    public class ConsumerBackgroundTasks:IConsumingTask<string, string>
    {
        private readonly ILogger<ConsumerBackgroundTasks> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IKafkaConsumer<string, string> _consumer;

        public ConsumerBackgroundTasks(ILogger<ConsumerBackgroundTasks> logger, IServiceProvider serviceProvider,IKafkaConsumerManager kafkaConsumerManager)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _consumer = kafkaConsumerManager.GetConsumer<string, string>("Order");
        }

        public void Execute(ConsumeResult<string, string> result)
        {
            try
            {
                var message = result.Message.Value;

                // Process the Kafka message here
                ConsumerCallbackAsync(message);
                _consumer.Commit(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error consuming message: {ex}");
            }
        }

        public async void ConsumerCallbackAsync(string message)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                    // Use customerBasketService here
                    var messageValue = JsonSerializer.Deserialize<Order>(message);
                    foreach (var item in messageValue.Items)
                    {
                        var product = await productService.GetByIdAsync(item.ProductId);
                        var productUpdateQuantity = new ProductAvailableQuantityDTO()
                        {
                            ProductId = product.Id,
                            Quantity = product.AvailableQuantity - item.Quantity,
                        };

                        var result = await productService.UpdateProductQuantity(productUpdateQuantity.ProductId, productUpdateQuantity.Quantity);
                        if (result != null)
                        {
                            _logger.LogInformation(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
