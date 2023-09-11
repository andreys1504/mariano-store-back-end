using MarianoStore.Catalogo.Api.IntegrationEvents.Pedidos.Pedido.Events;
using MarianoStore.Catalogo.Api.IntegrationEvents.Pedidos.Pedido.Handlers;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Api.IntegrationEvents.Pedidos.Pedido
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
                queueName: IntegrationEventsQueuesSettings.PEDIDOS.PedidoEvents.Queue,
                consumer: (serializedEvent, eventName) =>
                {
                    if (string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName)) return;

                    using IServiceScope scope = _serviceProvider.CreateScope();

                    if (eventName == typeof(PedidoRealizadoSucessoEvent).FullName)
                        PedidoRealizadoSucessoEventHandler.Handle(JsonConvert.DeserializeObject<PedidoRealizadoSucessoEvent>(serializedEvent), scope);
                });
        }
    }
}
