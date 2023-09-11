using MarianoStore.Core.Messages;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace MarianoStore.Core.Services.RabbitMq.Consumer
{
    public class ConsumerSetup
    {
        public ConsumerSetup(
            TypeMessage typeMessage,
            IModel consumerChannel,
            string exchangeName,
            string queueName,
            string routingKey,
            ushort prefetchCount = 10)
        {
            string deadLetterExchange = ConfigDeadLetterQueue(consumerChannel, typeMessage);

            var argumentsQueue = new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", deadLetterExchange }
            };

            consumerChannel.ExchangeDeclare(exchange: exchangeName, type: typeMessage == TypeMessage.Event ? ExchangeType.Topic : ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            consumerChannel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: argumentsQueue);
            consumerChannel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

            consumerChannel.BasicQos(prefetchSize: 0, prefetchCount: prefetchCount, global: false);

            ConsumerChannel = consumerChannel;
            ExchangeName = exchangeName;
            QueueName = queueName;
            RoutingKey = routingKey;
            PrefetchCount = prefetchCount;
        }

        public IModel ConsumerChannel { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public ushort PrefetchCount { get; set; }

        //
        private string ConfigDeadLetterQueue(IModel channel, TypeMessage typeMessage)
        {
            string exchange = "dead_letter__events_exchange";
            string queue = "dead_letter__events_queue";

            if (typeMessage == TypeMessage.Command)
            {
                exchange = "dead_letter__commands_exchange";
                queue = "dead_letter__commands_queue";
            }

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: queue, exchange: exchange, routingKey: "");

            return exchange;
        }
    }
}
