using MarianoStore.Core.Data;
using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Core.Messages.MessageInBroker.Models;
using MarianoStore.Core.Services.Logger;
using MarianoStore.Core.Services.RabbitMq.Consumer;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianoStore.Services.RabbitMq.Consumer
{
    public class ConsumerCommandRabbitMq : IConsumerCommandRabbitMq
    {
        private readonly EnvironmentSettings _environmentSettings;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<SqlConnection> _sqlConnections;

        public ConsumerCommandRabbitMq(
            EnvironmentSettings environmentSettings,
            IServiceProvider serviceProvider)
        {
            _environmentSettings = environmentSettings;
            _serviceProvider = serviceProvider;
            _sqlConnections = new List<SqlConnection>();
        }

        public Task ConsumerCommandAsync(string queueName, Action<string, string> consumer)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var consumersSetup = scope.ServiceProvider.GetService<IList<ConsumerSetup>>();
            ConsumerSetup consumerSetup = consumersSetup.First(consumer => consumer.QueueName == queueName);

            var messageInBrokerService = scope.ServiceProvider.GetRequiredService<IMessageInBrokerService>();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService>();


            var sqlConnection = ConnectionDatabase.GetConnection(environmentSettings: _environmentSettings);
            _sqlConnections.Add(sqlConnection);

            IModel channel = consumerSetup.ConsumerChannel;
            string deadLetterExchange = ConfigDeadLetterQueue(channel);

            var argumentsQueue = new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", deadLetterExchange }
            };
            channel.ExchangeDeclare(exchange: consumerSetup.ExchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: argumentsQueue);
            channel.QueueBind(queue: queueName, exchange: consumerSetup.ExchangeName, routingKey: consumerSetup.RoutingKey);

            channel.BasicQos(prefetchSize: 0, prefetchCount: consumerSetup.PrefetchCount, global: false);

            var eventingBasicConsumer = new EventingBasicConsumer(channel);
            eventingBasicConsumer.Received += (sender, eventArgs) =>
            {
                //TODO:
                //using IServiceScope scope = _serviceProvider2.CreateScope();

                (string commandName, MessageInBrokerModel messageInBroker, string serializedCommand) = GetMessage(eventArgs, channel, loggerService);

                if (messageInBroker == null || string.IsNullOrWhiteSpace(serializedCommand) || string.IsNullOrWhiteSpace(commandName))
                    return;

                if (messageInBroker.Processed.HasValue)
                {
                    channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                    return;
                }


                using SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                try
                {
                    messageInBrokerService.MarkAsProcessed(message: messageInBroker, sqlConnection: sqlConnection, sqlTransaction: sqlTransaction);
                    consumer(serializedCommand, commandName);
                    channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (channel.IsOpen)
                        channel.BasicNack(deliveryTag: eventArgs.DeliveryTag, multiple: false, requeue: eventArgs.Redelivered == false);

                    messageInBrokerService.IncrementNum(message: messageInBroker, sqlConnection: sqlConnection, sqlTransaction: sqlTransaction);
                    sqlTransaction.Commit();
                    loggerService
                        .LogErrorRegisterAsync(ex, "RabbitMQ; ConsumerCommandAsync: Erro ao consumir mensagem")
                        .GetAwaiter().GetResult();
                }
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: eventingBasicConsumer);


            return Task.CompletedTask;
        }


        //
        private (string commandName, MessageInBrokerModel messageInBroker, string serializedCommand) GetMessage(
            BasicDeliverEventArgs eventArgs,
            IModel channel,
            ILoggerService loggerService)
        {
            string commandName = null;
            MessageInBrokerModel messageInBroker = null;
            string serializedCommand = null;

            try
            {
                ReadOnlyMemory<byte> body = eventArgs.Body;
                string postMessage = Encoding.UTF8.GetString(body.ToArray());

                KeyValuePair<string, object> commandNameProperty = eventArgs.BasicProperties.Headers.FirstOrDefault(header => header.Key == "CommandName");
                if (commandNameProperty.Equals(default(KeyValuePair<string, object>)))
                    throw new ArgumentNullException(message: "commandNameProperty is null", innerException: null);

                commandName = Encoding.UTF8.GetString(commandNameProperty.Value as byte[]);
                if (string.IsNullOrWhiteSpace(commandName))
                    throw new ArgumentNullException(message: "commandName is null", innerException: null);


                messageInBroker = JsonConvert.DeserializeObject<MessageInBrokerModel>(postMessage);
                serializedCommand = messageInBroker.Body;
            }
            catch (Exception ex)
            {
                if (channel.IsOpen)
                    channel.BasicReject(deliveryTag: eventArgs.DeliveryTag, requeue: false);

                loggerService
                    .LogErrorRegisterAsync(ex, "RabbitMQ; GetMessage: Erro ao deserializar mensagem")
                    .GetAwaiter().GetResult();
            }


            return (commandName, messageInBroker, serializedCommand);
        }

        private string ConfigDeadLetterQueue(IModel channel)
        {
            string exchange = "dead_letter__commands_exchange";
            string queue = "dead_letter__commands_queue";

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: queue, exchange: exchange, routingKey: "");

            return exchange;
        }
    }
}
