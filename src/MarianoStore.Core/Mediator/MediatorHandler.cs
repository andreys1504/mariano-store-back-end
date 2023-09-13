using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Core.Messages;
using MarianoStore.Core.Settings;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MarianoStore.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IPublisherRabbitMq _publisherRabbitMq;
        private readonly IMediator _mediator;
        private readonly EnvironmentSettings _environmentSettings;

        public MediatorHandler(
            IPublisherRabbitMq publisherRabbitMq,
            IMediator mediator,
            EnvironmentSettings environmentSettings)
        {
            _publisherRabbitMq = publisherRabbitMq;
            _mediator = mediator;
            _environmentSettings = environmentSettings;
        }

        public async Task SendEventToHandlerAsync<TEvent>(TEvent @event)
        {
            await _mediator.Publish(notification: @event);
        }

        public async Task SendEventObjectToHandlerAsync(string serializedEvent, string eventName)
        {
            Assembly assembly = AppDomain.CurrentDomain.Load(_environmentSettings.DomainLayer);
            Type type = assembly.GetType(eventName);
            await SendEventToHandlerAsync(JsonConvert.DeserializeObject(serializedEvent, type));
        }

        public async Task SendCommandToHandlerAsync<TCommand>(TCommand @command)
        {
            await _mediator.Send(request: @command);
        }

        public async Task SendCommandObjectToHandlerAsync(string serializedCommand, string commandName)
        {
            Assembly assembly = AppDomain.CurrentDomain.Load(_environmentSettings.ApplicationLayer);
            Type type = assembly.GetType(commandName);
            await SendCommandToHandlerAsync(JsonConvert.DeserializeObject(serializedCommand, type));
        }

        //

        public async Task SendEventToQueueAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            await _publisherRabbitMq.PublishEventAsync(@event);
        }

        public async Task SendCommandToQueueAsync<TCommand>(TCommand @command) where TCommand : Command
        {
            await _publisherRabbitMq.PublishCommandAsync(@command);
        }
    }
}
