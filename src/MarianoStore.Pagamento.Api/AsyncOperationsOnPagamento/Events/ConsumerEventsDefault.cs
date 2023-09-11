using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Pagamento.Application.EventsHandlers.PagamentoRealizadoSucesso;
using MarianoStore.Pagamento.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Api.AsyncOperationsOnPagamento.Events
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
                consumer: (serializedEvent, eventName) =>
                {
                    using IServiceScope scope = _serviceProvider.CreateScope();

                    if (string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName)) return;


                    if (eventName == typeof(PagamentoRealizadoSucessoEvent).FullName)
                        scope.ServiceProvider.GetService<PagamentoRealizadoSucessoEventHandler>().Handle(JsonConvert.DeserializeObject<PagamentoRealizadoSucessoEvent>(serializedEvent)).Wait();
                });
        }
    }
}
