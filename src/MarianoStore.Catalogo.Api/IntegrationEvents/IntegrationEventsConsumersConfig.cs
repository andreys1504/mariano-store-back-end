using MarianoStore.Core.Messages;
using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Catalogo.Api.IntegrationEvents
{
    public static class IntegrationEventsConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            {
                //Pagamento
                // Pagamento.Pagamento
                new ConsumerSetup(
                    typeMessage: TypeMessage.Event,
                    consumerChannel: consumerChannelDefault,
                    exchangeName: IntegrationEventsQueuesSettings.PAGAMENTO.PagamentoEventsExchange,
                    queueName: IntegrationEventsQueuesSettings.PAGAMENTO.PagamentoEvents.Queue,
                    routingKey: IntegrationEventsQueuesSettings.PAGAMENTO.PagamentoEvents.RoutingKey)
            };
        }
    }
}
