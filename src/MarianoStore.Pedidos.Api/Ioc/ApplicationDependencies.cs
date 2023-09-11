using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Pedidos.Api.Ioc
{
    public static class ApplicationDependencies
    {
        public static void Register(
            this IServiceCollection services,
            EnvironmentSettings environmentSettings)
        {
            MarianoStore.Ioc.Dependencies.Register(services, environmentSettings);
            RegisterDependenciesRabbitMq(services, environmentSettings);
        }

        //
        private static void RegisterDependenciesRabbitMq(IServiceCollection services, EnvironmentSettings environmentSettings)
        {
            AsyncOperations.Dependencies.Register(services);


            ConnectionFactory connectionFactory = CreateConnectionFactory.Create(environmentSettings);
            IConnection connectionRabbitMq = CreateConnection.Create(connectionFactory);

            var publishersSetup = new List<PublisherSetup>();
            var consumersSetup = new List<ConsumerSetup>();

            AsyncOperations.Dependencies.RegisterDependenciesRabbitMq(
                connectionRabbitMq,
                publishersSetup,
                consumersSetup);

            //
            MarianoStore.Ioc.Dependencies.RegisterDependenciesRabbitMq(
                services,
                environmentSettings,
                connection: connectionRabbitMq,
                publishersSetup,
                consumersSetup);
        }
    }
}
