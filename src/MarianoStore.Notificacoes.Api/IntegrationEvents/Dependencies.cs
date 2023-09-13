using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Notificacoes.Api.IntegrationEvents.Pedidos;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Notificacoes.Api.IntegrationEvents
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            //Pagamento
            services.AddHostedService<Pagamento_PagamentoConsumerHandler>();

            //Pedidos
            services.AddHostedService<Pedido_PedidosConsumerHandler>();
        }

        public static void ConsumersConfigRegister(
            IConnection connectionRabbitMq,
            List<ConsumerSetup> consumersSetup)
        {
            consumersSetup.AddRange(ConsumersConfig.Register(connectionRabbitMq));
        }
    }
}
