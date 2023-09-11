using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pedidos.Domain.Events;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos.Events
{
    public class PublishersConfig
    {
        public static List<PublisherSetup> Register(IConnection connectionRabbitMq)
        {
            IModel publisherChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<PublisherSetup>
            {
                //PedidoRealizadoSucesso
                new PublisherSetup(
                    typeMessage: TypeMessage.Event,
                    @object: typeof(PedidoRealizadoSucessoEvent),
                    publishChannel: publisherChannelDefault,
                    exchangeName: QueuesSettings.EventsExchange,
                    routingKey: QueuesSettings.PedidoRealizadoSucessoEvent.RoutingKey)
            };
        }
    }
}
