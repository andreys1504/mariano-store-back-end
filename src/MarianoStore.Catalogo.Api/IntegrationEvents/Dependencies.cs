using MarianoStore.Catalogo.Api.IntegrationEvents.Pedidos.Pedido;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Catalogo.Api.IntegrationEvents
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            //Pedidos
            services.AddHostedService<Pedido_PedidosEventHandler>();
        }

        public static void RegisterDependenciesRabbitMq(
            IConnection connectionRabbitMq,
            List<PublisherSetup> publishersSetup,
            List<ConsumerSetup> consumersSetup)
        {
            consumersSetup.AddRange(IntegrationEventsConsumersConfig.Register(connectionRabbitMq));
        }
    }
}
