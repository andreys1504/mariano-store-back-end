using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Pagamento.Application.IntegrationEvents.Events;
using MarianoStore.Pagamento.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Api.IntegrationEvents.Pedidos
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
                    using IServiceScope scope = _serviceProvider.CreateScope();

                    if (string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName)) return;

                    
                    if (eventName.Contains($".{nameof(PedidoRealizadoSucessoEvent)}"))
                        scope.ServiceProvider.GetService<PedidoRealizadoSucessoEventHandler>()
                            .Handle(JsonConvert.DeserializeObject<PedidoRealizadoSucessoEvent>(serializedEvent), CancellationToken.None).Wait();
                });
        }
    }
}
