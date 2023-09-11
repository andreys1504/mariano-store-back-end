using RabbitMQ.Client;

namespace MarianoStore.Core.Services.RabbitMq
{
    public static class CreateChannel
    {
        public static IModel Create(IConnection connectionRabbitMq)
        {
            return connectionRabbitMq.CreateModel();
        }
    }
}
