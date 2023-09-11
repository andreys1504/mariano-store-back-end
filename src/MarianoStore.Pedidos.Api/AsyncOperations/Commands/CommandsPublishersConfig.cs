using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pedidos.Api.ApplicationServices.NovoPedido;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperations.Commands
{
    public class CommandsPublishersConfig
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
                    exchangeName: CommandsQueuesSettings.CommandsExchange,
                    routingKey: CommandsQueuesSettings.CommandsRoutingKey)
            };
        }
    }
}
