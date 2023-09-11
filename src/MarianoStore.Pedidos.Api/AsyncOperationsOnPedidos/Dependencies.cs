using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos.Commands;
using MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddHostedService<ConsumerCommandsDefault>();
            services.AddHostedService<ConsumerEventsDefault>();
        }

        public static void RegisterDependenciesRabbitMq(
            IConnection connectionRabbitMq,
            List<PublisherSetup> publishersSetup,
            List<ConsumerSetup> consumersSetup)
        {
            publishersSetup.AddRange(Commands.PublishersConfig.Register(connectionRabbitMq));
            publishersSetup.AddRange(Events.PublishersConfig.Register(connectionRabbitMq));

            consumersSetup.AddRange(Commands.ConsumersConfig.Register(connectionRabbitMq));
            consumersSetup.AddRange(Events.ConsumersConfig.Register(connectionRabbitMq));
        }
    }
}
