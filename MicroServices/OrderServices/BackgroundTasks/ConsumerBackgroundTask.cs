using System.Text.Json;
using Confluent.Kafka;
using Manonero.MessageBus.Kafka.Abstractions;
using OrderServices.Application.Entities;
using OrderServices.Application.Services;

namespace OrderServices.BackgroundTasks
{
    public class ConsumerBackgroundTask:IConsumingTask<string, string>
    {
        private readonly ILogger<ConsumerBackgroundTask> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerBackgroundTask(ILogger<ConsumerBackgroundTask> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public void Execute(ConsumeResult<string, string> result)
        {
            try
            {
                var message = result.Message.Value;

                // Process the Kafka message here
                ConsumerCallbackAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error consuming message: {ex}");
            }
        }

        private async void ConsumerCallbackAsync(string message)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                    var messageValue = JsonSerializer.Deserialize<Order>(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
