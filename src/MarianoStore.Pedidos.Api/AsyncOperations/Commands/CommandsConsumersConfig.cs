using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperations.Commands
{
    public static class CommandsConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            {
                new ConsumerSetup(
                    typeMessage: TypeMessage.Command,
                    consumerChannel: consumerChannelDefault,
                    exchangeName: CommandsQueuesSettings.CommandsExchange,
                    queueName: CommandsQueuesSettings.CommandsQueue,
                    routingKey: CommandsQueuesSettings.CommandsRoutingKey)
            };
        }
    }
}
