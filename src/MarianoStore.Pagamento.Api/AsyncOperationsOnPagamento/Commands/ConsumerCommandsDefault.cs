using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Mediator;
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
                consumer: (serializedCommand, commandName, commandName_Name) =>
                {
                    if (string.IsNullOrWhiteSpace(serializedCommand) || string.IsNullOrWhiteSpace(commandName)) return;


                    using IServiceScope scope = _serviceProvider.CreateScope();
                    var mediatorHandler = scope.ServiceProvider.GetService<IMediatorHandler>();


                    mediatorHandler.SendCommandObjectToHandlerAsync(serializedCommand: serializedCommand, commandName: commandName).Wait();
                });
        }
    }
}
