using MarianoStore.Core.Infra.Services.RabbitMq;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Catalogo.Api.AsyncOperationsOnCatalogo.Events
{
    public class PublishersConfig
    {
        public static List<PublisherSetup> Register(IConnection connectionRabbitMq)
        {
            IModel publisherChannelDefault = CreateChannel.Create(connectionRabbitMq);

            return new List<PublisherSetup>
            {
            };
        }
    }
}
