using MarianoStore.Core.Ioc;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Collections.Generic;

namespace MarianoStore.Core.Services.RabbitMq
{
    public static class Dependencies
    {
        public static void Register(
            this IServiceCollection services,
            EnvironmentSettings environmentSettings,
            IConnection connection,
            IList<PublisherSetup> publishersSetup,
            IList<ConsumerSetup> consumersSetup)
        {
            services.AddSingletonWithRetry<IConnection, BrokerUnreachableException>(serviceProvider => connection);

            if (publishersSetup != null)
                services.AddSingleton(publishersSetup);

            if (consumersSetup != null)
                services.AddSingleton(consumersSetup);

            RegisterConsumersPublishs(services);

            //TODO: Dispose para Singletons
        }


        //
        private static void RegisterConsumersPublishs(IServiceCollection services)
        {
            //Consumer
            services.AddTransient<IConsumerCommandRabbitMq, ConsumerCommandRabbitMq>();
            services.AddTransient<IConsumerEventRabbitMq, ConsumerEventRabbitMq>();

            //Publisher
            services.AddTransient<IPublisherRabbitMq, PublisherRabbitMq>();
        }
    }
}
