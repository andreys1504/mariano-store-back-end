using RabbitMQ.Client;

namespace MarianoStore.Core.Infra.Services.RabbitMq
{
    public class CreateConnection
    {
        public static IConnection Create(ConnectionFactory connectionFactory)
        {
            return connectionFactory.CreateConnection();
        }
    }
}
