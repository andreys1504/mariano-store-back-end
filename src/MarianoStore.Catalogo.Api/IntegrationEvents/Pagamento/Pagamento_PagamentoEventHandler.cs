using MarianoStore.Catalogo.Application.IntegrationEvents.Events;
using MarianoStore.Catalogo.Application.IntegrationEvents.EventsHandlers.PagamentoRealizadoSucesso;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Api.IntegrationEvents.Pagamento
{
    public class Pagamento_PagamentoEventHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Pagamento_PagamentoEventHandler(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope1 = _serviceProvider.CreateScope();

            var consumerEventRabbitMq = scope1.ServiceProvider.GetService<IConsumerEventRabbitMq>();
            await consumerEventRabbitMq.ConsumerEventAsync(
                queueName: IntegrationEventsQueuesSettings.PAGAMENTO.PagamentoEvents.Queue,
                consumer: (serializedEvent, eventName) =>
                {
                    using IServiceScope scope = _serviceProvider.CreateScope();

                    if (string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName)) return;

                    
                    if (eventName.Contains($".{nameof(PagamentoRealizadoSucessoEvent)}"))
                        scope.ServiceProvider.GetService<PagamentoRealizadoSucessoEventHandler>()
                            .Handle(JsonConvert.DeserializeObject<PagamentoRealizadoSucessoEvent>(serializedEvent), CancellationToken.None).Wait();
                });
        }
    }
}
