﻿using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

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
            // .Core.Application
            Core.Application.AspNet.ControllerBase.Dependencies.Register(services);
            // .Core.ApplicationLayer
            Core.ApplicationLayer.ApplicationsServices.Dependencies.Register(services);
            Core.ApplicationLayer.EventsHandlers.Dependencies.Register(services);
            // .Core.Infra
            Core.Infra.Data.Dependencies.Register(services, environmentSettings);
            // .Core.Mediator
            Core.Mediator.Dependencies.Register(services);
            // .Core.Settings
            services.AddSingleton(environmentSettings);

            //Infra.Services
            Infra.Services.Dependencies.Register(services, environmentSettings);
        }

        public static void RegisterDependenciesRabbitMq(
            this IServiceCollection services,
            EnvironmentSettings environmentSettings,
            IConnection connection,
            IList<PublisherSetup> publishersSetup,
            IList<ConsumerSetup> consumersSetup)
        {
            Infra.Services.RabbitMq.Dependencies.Register(
                services,
                environmentSettings,
                connection,
                publishersSetup,
                consumersSetup);
        }
    }
}