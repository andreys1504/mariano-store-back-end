using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task SendEventToHandlerAsync<TEvent>(TEvent @event);

        Task SendEventObjectToHandlerAsync(string serializedEvent, string eventName);

        Task SendCommandToHandlerAsync<TCommand>(TCommand @command);

        Task SendCommandObjectToHandlerAsync(string serializedCommand, string commandName);

        //

        Task SendEventToQueueAsync<TEvent>(TEvent @event) where TEvent : Event;

        Task SendCommandToQueueAsync<TCommand>(TCommand @command) where TCommand : Command;
    }
}