using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Pagamento.Api.IntegrationEvents.Pedidos;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pagamento.Api.IntegrationEvents
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
            List<ConsumerSetup> consumersSetup)
        {
            consumersSetup.AddRange(ConsumersConfig.Register(connectionRabbitMq));
        }
    }
}
