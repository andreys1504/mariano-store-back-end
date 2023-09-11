using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.AsyncOperations.Events
{
    public static class EventsConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            {
                new ConsumerSetup(
                    typeMessage: TypeMessage.Event,
                    consumerChannel: consumerChannelDefault,
                    exchangeName: EventsQueuesSettings.EventsExchange,
                    queueName: EventsQueuesSettings.EventsQueue,
                    routingKey: EventsQueuesSettings.EventsRoutingKey)
            };
        }
    }
}
