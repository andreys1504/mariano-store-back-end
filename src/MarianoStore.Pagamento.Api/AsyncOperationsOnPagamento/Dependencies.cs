using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Pagamento.Api.AsyncOperationsOnPagamento.Commands;
using MarianoStore.Pagamento.Api.AsyncOperationsOnPagamento.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pagamento.Api.AsyncOperationsOnPagamento
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddHostedService<ConsumerCommandsDefault>();
            services.AddHostedService<ConsumerEventsDefault>();
        }

        public static void PublishersConsumersConfigRegister(
            IConnection connectionRabbitMq,
            List<PublisherSetup> publishersSetup,
            List<ConsumerSetup> consumersSetup)
        {
            publishersSetup.AddRange(Commands.PublishersConfig.Register(connectionRabbitMq));
            publishersSetup.AddRange(Events.PublishersConfig.Register(connectionRabbitMq));

            consumersSetup.AddRange(Commands.ConsumersConfig.Register(connectionRabbitMq));
            consumersSetup.AddRange(Events.ConsumersConfig.Register(connectionRabbitMq));
        }
    }
}
