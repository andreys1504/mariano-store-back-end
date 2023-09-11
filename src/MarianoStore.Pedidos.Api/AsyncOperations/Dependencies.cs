using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pedidos.Api.AsyncOperations.Commands;
using MarianoStore.Pedidos.Api.AsyncOperations.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperations
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddHostedService<CommandsHandler>();
            services.AddHostedService<EventsHandler>();
        }

        public static void RegisterDependenciesRabbitMq(
            IConnection connectionRabbitMq,
            List<PublisherSetup> publishersSetup,
            List<ConsumerSetup> consumersSetup)
        {
            publishersSetup.AddRange(CommandsPublishersConfig.Register(connectionRabbitMq));
            publishersSetup.AddRange(EventsPublishersConfig.Register(connectionRabbitMq));

            consumersSetup.AddRange(CommandsConsumersConfig.Register(connectionRabbitMq));
            consumersSetup.AddRange(EventsConsumersConfig.Register(connectionRabbitMq));
        }
    }
}
