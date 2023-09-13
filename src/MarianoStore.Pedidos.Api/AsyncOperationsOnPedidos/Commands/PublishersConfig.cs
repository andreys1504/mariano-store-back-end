using MarianoStore.Core.Infra.Services.RabbitMq;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Core.Messages;
using MarianoStore.Pedidos.Application.Services.NovoPedido;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos.Commands
{
    public class PublishersConfig
    {
        public static List<PublisherSetup> Register(IConnection connectionRabbitMq)
        {
            IModel publisherChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<PublisherSetup>
            {
                //NovoPedido
                new PublisherSetup(
                    typeMessage: TypeMessage.Command,
                    @object: typeof(NovoPedidoRequest),
                    publishChannel: publisherChannelDefault,
                    exchangeName: QueuesSettings.CommandsExchange,
                    routingKey: QueuesSettings.CommandsRoutingKey)
            };
        }
    }
}
