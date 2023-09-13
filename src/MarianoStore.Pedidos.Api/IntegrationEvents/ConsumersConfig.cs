using MarianoStore.Core.Infra.Services.RabbitMq;
using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.IntegrationEvents
{
    public static class ConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            {
            };
        }
    }
}
