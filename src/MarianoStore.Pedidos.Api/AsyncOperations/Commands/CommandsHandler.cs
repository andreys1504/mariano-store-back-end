using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Pedidos.Api.ApplicationServices.NovoPedido;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Api.AsyncOperations.Commands
{
    public class CommandsHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandsHandler(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope1 = _serviceProvider.CreateScope();

            var consumerCommandRabbitMq = scope1.ServiceProvider.GetService<IConsumerCommandRabbitMq>();
            await consumerCommandRabbitMq.ConsumerCommandAsync(
                queueName: CommandsQueuesSettings.CommandsQueue,
                consumer: (serializedCommand, commandName) =>
                {
                    using IServiceScope scope = _serviceProvider.CreateScope();

                    if (string.IsNullOrWhiteSpace(serializedCommand) || string.IsNullOrWhiteSpace(commandName)) return;

                    if (commandName == typeof(NovoPedidoRequest).FullName)
                        new NovoPedidoAppService(scope).Handle(JsonConvert.DeserializeObject<NovoPedidoRequest>(serializedCommand)).Wait();
                });
        }
    }
}
