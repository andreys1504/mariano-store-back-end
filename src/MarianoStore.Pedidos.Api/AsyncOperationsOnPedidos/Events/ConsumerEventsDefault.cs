﻿using MarianoStore.Core.Services.RabbitMq;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos.Events
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


                    HelpersRabbitMq.SendEventToHandler(
                        serializedEvent: serializedEvent,
                        eventName: eventName,
                        scope);

                    //if (eventName == typeof(PedidoRealizadoSucessoEvent).FullName)
                    //    scope.ServiceProvider.GetService<PedidoRealizadoSucessoEventHandler>().Handle(JsonConvert.DeserializeObject<PedidoRealizadoSucessoEvent>(serializedEvent)).Wait();
                });
        }
    }
}
