using MarianoStore.Core.Infra.Services.RabbitMq;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Core.Messages;
using MarianoStore.Pagamento.Domain.Events;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pagamento.Api.AsyncOperationsOnPagamento.Events
{
    public class PublishersConfig
    {
        public static List<PublisherSetup> Register(IConnection connectionRabbitMq)
        {
            IModel publisherChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<PublisherSetup>
            {
                //PagamentoRealizadoSucesso
                new PublisherSetup(
                    typeMessage: TypeMessage.Event,
                    @object: typeof(PagamentoRealizadoSucessoEvent),
                    publishChannel: publisherChannelDefault,
                    exchangeName: QueuesSettings.EventsExchange,
                    routingKey: QueuesSettings.PagamentoRealizadoSucessoEvent.RoutingKey)
            };
        }
    }
}
