using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pedidos.Api.Domain.Events;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperations.Events
{
    public class EventsPublishersConfig
    {
        public static List<PublisherSetup> Register(IConnection connectionRabbitMq)
        {
            IModel publisherChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<PublisherSetup>
            {
                //NovoPedido
                new PublisherSetup(
                    typeMessage: TypeMessage.Event,
                    @object: typeof(PedidoRealizadoSucessoEvent),
                    publishChannel: publisherChannelDefault,
                    exchangeName: EventsQueuesSettings.EventsExchange,
                    routingKey: EventsQueuesSettings.PedidoRealizadoSucessoEvent.RoutingKey)
            };
        }
    }
}
