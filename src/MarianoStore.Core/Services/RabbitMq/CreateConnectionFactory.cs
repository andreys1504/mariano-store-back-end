using MarianoStore.Core.Settings;
using RabbitMQ.Client;
using System;

namespace MarianoStore.Core.Services.RabbitMq
{
    public class CreateConnectionFactory
    {
        public static ConnectionFactory Create(EnvironmentSettings environmentSettings)
        {
            return new ConnectionFactory()
            {
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                AutomaticRecoveryEnabled = true,
                HostName = environmentSettings.RabbitMq.HostName,
                Port = environmentSettings.RabbitMq.Port,
                UserName = environmentSettings.RabbitMq.UserName,
                Password = environmentSettings.RabbitMq.Password,
                VirtualHost = "/",
                ClientProvidedName = environmentSettings.ProjectName
            };
        }
    }
}
