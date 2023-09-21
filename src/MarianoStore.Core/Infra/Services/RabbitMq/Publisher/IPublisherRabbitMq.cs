using MarianoStore.Core.Messages.MessageInBroker.Models;
using System.Threading.Tasks;

namespace MarianoStore.Core.Infra.Services.RabbitMq.Publisher
{
    public interface IPublisherRabbitMq
    {
        Task PublishCommandAsync(object @command, MessageInBrokerModel messageInBroker = null);

        Task PublishEventAsync(object @event, MessageInBrokerModel messageInBroker = null);
    }
}