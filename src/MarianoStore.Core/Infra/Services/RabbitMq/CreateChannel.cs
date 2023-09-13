using RabbitMQ.Client;

namespace MarianoStore.Core.Infra.Services.RabbitMq
{
    public static class CreateChannel
    {
        public static IModel Create(IConnection connectionRabbitMq)
        {
            return connectionRabbitMq.CreateModel();
        }
    }
}
