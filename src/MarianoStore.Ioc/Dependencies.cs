using MarianoStore.Core.Data;
using MarianoStore.Core.Ioc;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Data.SqlClient;

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

            // .Core.Settings
            services.AddSingleton(environmentSettings);

            //Services
            Services.Dependencies.Register(services);
        }

        public static void RegisterDependenciesRabbitMq(
            this IServiceCollection services,
            EnvironmentSettings environmentSettings,
            IConnection connection,
            IList<PublisherSetup> publishersSetup,
            IList<ConsumerSetup> consumersSetup)
        {
            Services.RabbitMq.Dependencies.Register(
                services,
                environmentSettings,
                connection,
                publishersSetup,
                consumersSetup);
        }
    }
}