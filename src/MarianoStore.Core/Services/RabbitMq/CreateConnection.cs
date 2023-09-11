using RabbitMQ.Client;

namespace MarianoStore.Core.Services.RabbitMq
{
    public class CreateConnection
    {
        public static IConnection Create(ConnectionFactory connectionFactory)
        {
            return connectionFactory.CreateConnection();
        }
    }
}
