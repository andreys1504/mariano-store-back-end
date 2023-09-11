using MarianoStore.Core.Services.RabbitMq.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Api.AsyncOperationsOnPagamento.Commands
{
    public class ConsumerCommandsDefault : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ConsumerCommandsDefault(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope1 = _serviceProvider.CreateScope();

            var consumerCommandRabbitMq = scope1.ServiceProvider.GetService<IConsumerCommandRabbitMq>();
            await consumerCommandRabbitMq.ConsumerCommandAsync(
                queueName: QueuesSettings.CommandsQueue,
                consumer: (serializedCommand, commandName) =>
                {
                    using IServiceScope scope = _serviceProvider.CreateScope();

                    if (string.IsNullOrWhiteSpace(serializedCommand) || string.IsNullOrWhiteSpace(commandName)) return;

                });
        }
    }
}
