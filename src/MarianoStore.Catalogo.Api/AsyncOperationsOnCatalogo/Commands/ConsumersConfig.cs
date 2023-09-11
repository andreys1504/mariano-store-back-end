using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Catalogo.Api.AsyncOperationsOnCatalogo.Commands
{
    public static class ConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            { 
                ConsumerCommandsDefault(consumerChannelDefault)

                //
            };
        }

        private static ConsumerSetup ConsumerCommandsDefault(IModel consumerChannelDefault)
        {
            return new ConsumerSetup(
                typeMessage: TypeMessage.Command,
                consumerChannel: consumerChannelDefault,
                exchangeName: QueuesSettings.CommandsExchange,
                queueName: QueuesSettings.CommandsQueue,
                routingKey: QueuesSettings.CommandsRoutingKey);
        }
    }
}
