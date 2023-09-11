using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos.Events
{
    public static class ConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            { 
                ConsumerEventsDefault(consumerChannelDefault)

                //
            };
        }

        private static ConsumerSetup ConsumerEventsDefault(IModel consumerChannelDefault)
        {
            return new ConsumerSetup(
                typeMessage: TypeMessage.Event,
                consumerChannel: consumerChannelDefault,
                exchangeName: QueuesSettings.EventsExchange,
                queueName: QueuesSettings.EventsQueue,
                routingKey: QueuesSettings.EventsRoutingKey);
        }
    }
}
