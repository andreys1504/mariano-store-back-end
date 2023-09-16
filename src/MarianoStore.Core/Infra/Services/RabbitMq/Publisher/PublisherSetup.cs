using MarianoStore.Core.Messages;
using RabbitMQ.Client;
using System;

namespace MarianoStore.Core.Infra.Services.RabbitMq.Publisher
{
    public class PublisherSetup
    {
        public PublisherSetup(
            TypeMessage typeMessage,
            Type @object,
            IModel publishChannel,
            string exchangeName,
            string routingKey,
            TimeSpan? expirationMessage = null,
            byte? priority = null
          )
        {
            if (typeMessage == TypeMessage.Command)
            {
                publishChannel.ConfirmSelect();

                publishChannel.ExchangeDeclare(
                    exchange: exchangeName,
                    type: ExchangeType.Direct,
                    durable: true,
                    autoDelete: false,
                    arguments: null);
            }
            else
            {
                publishChannel.ConfirmSelect();

                publishChannel.ExchangeDeclare(
                    exchange: exchangeName,
                    type: ExchangeType.Topic,
                    durable: true,
                    autoDelete: false,
                    arguments: null);
            }


            ObjectFullName = @object.FullName;
            PublishChannel = publishChannel;
            ExchangeName = exchangeName;
            RoutingKey = routingKey;
            Priority = priority;
            ExpirationMessage = (expirationMessage == null 
                ? (typeMessage == TypeMessage.Command ? TimeSpan.FromHours(2) : TimeSpan.FromHours(24))
                : expirationMessage.Value).TotalMilliseconds.ToString();
        }

        public string ObjectFullName { get; set; }
        public IModel PublishChannel { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
        public byte? Priority { get; set; }
        public string ExpirationMessage { get; set; }
    }
}
