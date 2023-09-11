using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.Services.RabbitMq.Publisher
{
    public interface IPublisherRabbitMq
    {
        Task PublishCommandAsync<TObject>(TObject @object) where TObject : Message;
        Task PublishEventAsync<TObject>(TObject @object) where TObject : Message;
    }
}