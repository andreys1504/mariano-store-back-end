using MarianoStore.Core.Infra.Services.RabbitMq;
using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Messages;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Catalogo.Api.IntegrationEvents
{
    public static class ConsumersConfig
    {
        public static List<ConsumerSetup> Register(IConnection connectionRabbitMq)
        {
            IModel consumerChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<ConsumerSetup>
            {
                //Pagamento
                // .Pagamento
                new ConsumerSetup(
                    typeMessage: TypeMessage.Event,
                    consumerChannel: consumerChannelDefault,
                    exchangeName: QueuesSettings.PAGAMENTO.PagamentoEventsExchange,
                    queueName: QueuesSettings.PAGAMENTO.PagamentoEvents.Queue,
                    routingKey: QueuesSettings.PAGAMENTO.PagamentoEvents.RoutingKey)
            };
        }
    }
}
