using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Pedidos.Api.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Api.AsyncOperations.Events
{
    public class EventsHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public EventsHandler(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope1 = _serviceProvider.CreateScope();

            var consumerEventRabbitMq = scope1.ServiceProvider.GetService<IConsumerEventRabbitMq>();
            await consumerEventRabbitMq.ConsumerEventAsync(
                queueName: EventsQueuesSettings.EventsQueue,
                consumer: (serializedEvent, eventName) =>
                {
                    using IServiceScope scope = _serviceProvider.CreateScope();

                    if (string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName)) return;

                    if (eventName == typeof(PedidoRealizadoSucessoEvent).FullName)
                    {

                    }
                });
        }
    }
}
