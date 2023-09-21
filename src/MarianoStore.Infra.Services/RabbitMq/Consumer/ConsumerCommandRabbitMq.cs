using MarianoStore.Core.Infra.Data;
using MarianoStore.Core.Infra.Services.Logger;
using MarianoStore.Core.Infra.Services.RabbitMq.Consumer;
using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Core.Messages.MessageInBroker.Models;
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

namespace MarianoStore.Infra.Services.RabbitMq.Consumer
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

        public Task ConsumerCommandAsync(string queueName, Action<string, string, string> consumer)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var consumersSetup = scope.ServiceProvider.GetService<IList<ConsumerSetup>>();
            ConsumerSetup consumerSetup = consumersSetup.First(consumer => consumer.QueueName == queueName);

            var messageInBrokerService = scope.ServiceProvider.GetRequiredService<IMessageInBrokerService>();
            var loggerService = scope.ServiceProvider.GetRequiredService<ILoggerService>();

            SqlConnection sqlConnection = GetNewSqlConnection();

            IModel channel = consumerSetup.ConsumerChannel;

            var eventingBasicConsumer = new EventingBasicConsumer(channel);
            eventingBasicConsumer.Received += (sender, eventArgs) =>
            {
                //TODO:
                //using IServiceScope scope = _serviceProvider2.CreateScope();

                string commandName = null;
                string commandName_FullName = null;
                MessageInBrokerModel messageInBroker = null;
                string serializedCommand = null;

                try
                {
                    (commandName, commandName_FullName, messageInBroker, serializedCommand) = GetMessage(eventArgs, channel, loggerService);
                }
                catch
                {
                    return;
                }


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
                    consumer(serializedCommand, commandName, commandName_FullName);
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

        private SqlConnection GetNewSqlConnection()
        {
            var sqlConnection = ConnectionDatabase.NewConnection(environmentSettings: _environmentSettings);
            _sqlConnections.Add(sqlConnection);
            return sqlConnection;
        }


        //
        private (string commandName, string commandName_FullName, MessageInBrokerModel messageInBroker, string serializedCommand) GetMessage(
            BasicDeliverEventArgs eventArgs,
            IModel channel,
            ILoggerService loggerService)
        {
            string commandName = null;
            string commandName_FullName = null;
            MessageInBrokerModel messageInBroker = null;
            string serializedCommand = null;

            try
            {
                ReadOnlyMemory<byte> body = eventArgs.Body;
                string postMessage = Encoding.UTF8.GetString(body.ToArray());


                commandName = GetValueInBasicProperties(eventArgs, key: "CommandName");
                if (string.IsNullOrWhiteSpace(commandName))
                    throw new ArgumentNullException("commandName is null");


                commandName_FullName = GetValueInBasicProperties(eventArgs, key: "CommandName_FullName");
                if (string.IsNullOrWhiteSpace(commandName_FullName))
                    throw new ArgumentNullException("commandName_FullName is null");


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

                throw;
            }


            return (commandName, commandName_FullName, messageInBroker, serializedCommand);
        }

        private string GetValueInBasicProperties(BasicDeliverEventArgs eventArgs, string key)
        {
            KeyValuePair<string, object> header = eventArgs.BasicProperties.Headers.FirstOrDefault(header_ => header_.Key == key);
            if (header.Equals(default(KeyValuePair<string, object>)))
                return null;

            return Encoding.UTF8.GetString(header.Value as byte[]);
        }
    }
}
