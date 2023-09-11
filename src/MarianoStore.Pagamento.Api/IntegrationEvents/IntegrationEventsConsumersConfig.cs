using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pagamento.Api.IntegrationEvents
{
    public static class IntegrationEventsConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            {
                //Pedidos
                // Pedidos.Pedido
                new ConsumerSetup(
                    typeMessage: TypeMessage.Event,
                    consumerChannel: consumerChannelDefault,
                    exchangeName: IntegrationEventsQueuesSettings.PEDIDOS.PedidoEventsExchange,
                    queueName: IntegrationEventsQueuesSettings.PEDIDOS.PedidoEvents.Queue,
                    routingKey: IntegrationEventsQueuesSettings.PEDIDOS.PedidoEvents.RoutingKey)
            };
        }
    }
}
