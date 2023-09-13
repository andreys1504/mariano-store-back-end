using MarianoStore.Core.Infra.Services.RabbitMq;
using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Messages;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pagamento.Api.IntegrationEvents
{
    public static class ConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            {
                //Pedidos
                // .Pedido
                new ConsumerSetup(
                    typeMessage: TypeMessage.Event,
                    consumerChannel: consumerChannelDefault,
                    exchangeName: QueuesSettings.PEDIDOS.PedidoEventsExchange,
                    queueName: QueuesSettings.PEDIDOS.PedidoEvents.Queue,
                    routingKey: QueuesSettings.PEDIDOS.PedidoEvents.RoutingKey)
            };
        }
    }
}
