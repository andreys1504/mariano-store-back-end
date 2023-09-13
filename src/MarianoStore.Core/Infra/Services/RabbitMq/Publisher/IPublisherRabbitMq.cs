using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.Infra.Services.RabbitMq.Publisher
{
    public interface IPublisherRabbitMq
    {
        Task PublishCommandAsync<TCommand>(TCommand @command) where TCommand : Command;

        Task PublishEventAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}