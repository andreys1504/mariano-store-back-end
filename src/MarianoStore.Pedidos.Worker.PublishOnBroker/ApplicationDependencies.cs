using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Pedidos.Worker.PublishOnBroker
{
    public static class ApplicationDependencies
    {
        public static void Register(
            this IServiceCollection services,
            EnvironmentSettings environmentSettings)
        {
            Ioc.ApplicationDependencies.Register(services, environmentSettings);

            Ioc.ApplicationDependencies.RegisterDependenciesRabbitMq(services, environmentSettings);

            services.AddHostedService<RetryPublishCommandsBackgroundService>();
            services.AddHostedService<RetryPublishEventsBackgroundService>();
        }
    }
}
