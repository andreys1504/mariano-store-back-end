using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Mediator;
using MarianoStore.Notificacoes.Application.IntegrationEvents.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Notificacoes.Api.IntegrationEvents.Pedidos
{
    public class Pedido_PedidosEventHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Pedido_PedidosEventHandler(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope1 = _serviceProvider.CreateScope();

            var consumerEventRabbitMq = scope1.ServiceProvider.GetService<IConsumerEventRabbitMq>();
            await consumerEventRabbitMq.ConsumerEventAsync(
                queueName: QueuesSettings.PEDIDOS.PedidoEvents.Queue,
                consumer: (serializedEvent, eventName, eventName_Name) =>
                {
                    if (string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName)) return;


                    using IServiceScope scope = _serviceProvider.CreateScope();
                    var mediatorHandler = scope.ServiceProvider.GetService<IMediatorHandler>();


                    if (eventName_Name == nameof(PedidoRealizadoSucessoEvent))
                        mediatorHandler.SendEventToHandlerAsync(JsonConvert.DeserializeObject<PedidoRealizadoSucessoEvent>(serializedEvent)).Wait();
                });
        }
    }
}
