using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using MarianoStore.Core.Ioc;
using MarianoStore.Core.Data;
using RabbitMQ.Client;
using System.Collections.Generic;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Core.Services.RabbitMq.Publisher;

namespace MarianoStore.Ioc
{
    public static class Dependencies
    {
        public static void Register(
            this IServiceCollection services,
            EnvironmentSettings environmentSettings)
        {
            services.AddHttpClient();

            //Core
            // .Core.Data
            services.AddTransientWithRetry<SqlConnection, SqlException>(serviceProvider =>
            {
                return ConnectionDatabase.GetConnection(environmentSettings);
            });

            // .Core.Messages
            Core.Messages.Dependencies.Register(services);

            // .Core.Services
            Core.Services.Dependencies.Register(services);

            // .Core.Settings
            services.AddSingleton(environmentSettings);
        }

        public static void RegisterDependenciesRabbitMq(
            this IServiceCollection services,
            EnvironmentSettings environmentSettings,
            IConnection connection,
            IList<PublisherSetup> publishersSetup,
            IList<ConsumerSetup> consumersSetup)
        {
            Core.Services.RabbitMq.Dependencies.Register(
                services,
                environmentSettings,
                connection,
                publishersSetup,
                consumersSetup);
        }
    }
}