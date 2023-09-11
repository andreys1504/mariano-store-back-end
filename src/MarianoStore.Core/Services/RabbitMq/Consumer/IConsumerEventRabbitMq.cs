using System;
using System.Threading.Tasks;

namespace MarianoStore.Core.Services.RabbitMq.Consumer
{
    public interface IConsumerEventRabbitMq
    {
        Task ConsumerEventAsync(string queueName, Action<string, string> consumer);
    }
}