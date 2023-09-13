using MarianoStore.Catalogo.Application.IntegrationEvents.Events;
using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Mediator;
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
                queueName: QueuesSettings.PAGAMENTO.PagamentoEvents.Queue,
                consumer: (serializedEvent, eventName, eventName_Name) =>
                {
                    if (string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName)) return;


                    using IServiceScope scope = _serviceProvider.CreateScope();
                    var mediatorHandler = scope.ServiceProvider.GetService<IMediatorHandler>();


                    if (eventName_Name == nameof(PagamentoRealizadoSucessoEvent))
                        mediatorHandler.SendEventToHandlerAsync(JsonConvert.DeserializeObject<PagamentoRealizadoSucessoEvent>(serializedEvent)).Wait();
                });
        }
    }
}
