﻿using MarianoStore.Catalogo.Api.IntegrationEvents.Pagamento;
using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Catalogo.Api.IntegrationEvents
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            //Pedidos
            services.AddHostedService<Pagamento_PagamentoConsumerHandler>();
        }

        public static void ConsumersConfigRegister(
            IConnection connectionRabbitMq,
            List<ConsumerSetup> consumersSetup)
        {
            consumersSetup.AddRange(ConsumersConfig.Register(connectionRabbitMq));
        }
    }
}
