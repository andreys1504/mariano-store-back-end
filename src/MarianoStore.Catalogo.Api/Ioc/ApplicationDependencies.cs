using MarianoStore.Core.Infra.Services.RabbitMq;
using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace MarianoStore.Catalogo.Api.Ioc
{
    public static class ApplicationDependencies
    {
        public static void Register(
            this IServiceCollection services,
            EnvironmentSettings environmentSettings)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load(environmentSettings.DomainLayer)));
            services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load(environmentSettings.ApplicationLayer)));

            AsyncOperationsOnCatalogo.Dependencies.Register(services);
            IntegrationEvents.Dependencies.Register(services);

            MarianoStore.Ioc.Dependencies.Register(services, environmentSettings);
            RegisterDependenciesRabbitMq(services, environmentSettings);
        }

        private static void RegisterDependenciesRabbitMq(IServiceCollection services, EnvironmentSettings environmentSettings)
        {
            ConnectionFactory connectionFactory = CreateConnectionFactory.Create(environmentSettings);
            IConnection connectionRabbitMq = CreateConnection.Create(connectionFactory);

            var publishersSetup = new List<PublisherSetup>();
            var consumersSetup = new List<ConsumerSetup>();

            AsyncOperationsOnCatalogo.Dependencies.RegisterDependenciesRabbitMq(
                connectionRabbitMq,
                publishersSetup,
                consumersSetup);

            IntegrationEvents.Dependencies.RegisterDependenciesRabbitMq(
                connectionRabbitMq,
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
