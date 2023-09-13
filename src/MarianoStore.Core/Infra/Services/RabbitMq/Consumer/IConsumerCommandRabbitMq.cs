using System;
using System.Threading.Tasks;

namespace MarianoStore.Core.Infra.Services.RabbitMq.Consumer
{
    public interface IConsumerCommandRabbitMq
    {
        Task ConsumerCommandAsync(string queueName, Action<string, string, string> consumer);
    }
}