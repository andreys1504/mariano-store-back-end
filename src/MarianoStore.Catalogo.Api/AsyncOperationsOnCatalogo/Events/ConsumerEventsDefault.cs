using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Api.AsyncOperationsOnCatalogo.Events
{
    public class ConsumerEventsDefault : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ConsumerEventsDefault(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope1 = _serviceProvider.CreateScope();

            var consumerEventRabbitMq = scope1.ServiceProvider.GetService<IConsumerEventRabbitMq>();
            await consumerEventRabbitMq.ConsumerEventAsync(
                queueName: QueuesSettings.EventsQueue,
                consumer: (serializedEvent, eventName, eventName_FullName) =>
                {
                    if (string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName_FullName)) return;


                    using IServiceScope scope = _serviceProvider.CreateScope();
                    var mediatorHandler = scope.ServiceProvider.GetService<IMediatorHandler>();


                    mediatorHandler.SendEventObjectToHandlerAsync(serializedEvent: serializedEvent, eventName: eventName_FullName).Wait();
                });
        }
    }
}
