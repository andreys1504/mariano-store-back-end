using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.IntegrationEvents
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
        }

        public static void RegisterDependenciesRabbitMq(
            IConnection connectionRabbitMq,
            List<ConsumerSetup> consumersSetup)
        {
            consumersSetup.AddRange(ConsumersConfig.Register(connectionRabbitMq));
        }
    }
}
