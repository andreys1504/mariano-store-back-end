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
    public class ConsumerEventRabbitMq : IConsumerEventRabbitMq
    {
        private readonly EnvironmentSettings _environmentSettings;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<SqlConnection> _sqlConnections;

        public ConsumerEventRabbitMq(
            EnvironmentSettings environmentSettings,
            IServiceProvider serviceProvider)
        {
            _environmentSettings = environmentSettings;
            _serviceProvider = serviceProvider;
            _sqlConnections = new List<SqlConnection>();
        }

        public Task ConsumerEventAsync(string queueName, Action<string, string, string> consumer)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var consumersSetup = scope.ServiceProvider.GetService<IList<ConsumerSetup>>();
            ConsumerSetup consumerSetup = consumersSetup.First(consumer => consumer.QueueName == queueName);

            var messageInBrokerService = scope.ServiceProvider.GetService<IMessageInBrokerService>();
            var loggerService = scope.ServiceProvider.GetService<ILoggerService>();

            SqlConnection sqlConnection1 = GetNewSqlConnection();
            SqlConnection sqlConnection2 = GetNewSqlConnection();

            IModel channel = consumerSetup.ConsumerChannel;

            var eventingBasicConsumer = new EventingBasicConsumer(channel);
            eventingBasicConsumer.Received += (sender, eventArgs) =>
            {
                //TODO:
                //using IServiceScope scope = _serviceProvider.CreateScope();

                string eventName = null;
                string eventName_FullName = null;
                MessageInBrokerModel messageInBroker = null;
                string serializedEvent = null;

                try
                {
                    (eventName, eventName_FullName, messageInBroker, serializedEvent) = 
                        GetMessage(
                            eventArgs,
                            channel,
                            sqlConnection: sqlConnection1,
                            messageInBrokerService,
                            loggerService);
                }
                catch
                {
                    return;
                }


                if (messageInBroker == null || string.IsNullOrWhiteSpace(serializedEvent) || string.IsNullOrWhiteSpace(eventName))
                    return;

                if (messageInBroker.Processed.HasValue)
                {
                    channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                    return;
                }


                using SqlTransaction sqlTransaction = sqlConnection2.BeginTransaction();
                try
                {
                    messageInBrokerService.MarkAsProcessed(message: messageInBroker, sqlConnection: sqlConnection2, sqlTransaction: sqlTransaction);
                    consumer(serializedEvent, eventName, eventName_FullName);
                    channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (channel.IsOpen)
                        channel.BasicNack(deliveryTag: eventArgs.DeliveryTag, multiple: false, requeue: eventArgs.Redelivered == false);

                    messageInBrokerService.IncrementNum(message: messageInBroker, sqlConnection: sqlConnection2, sqlTransaction: sqlTransaction);
                    sqlTransaction.Commit();
                    loggerService
                        .LogErrorRegisterAsync(ex, "RabbitMQ; ConsumerEventAsync: Erro ao consumir mensagem")
                        .GetAwaiter().GetResult();
                }
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: eventingBasicConsumer);


            return Task.CompletedTask;
        }


        //
        private (string eventName, string eventName_FullName, MessageInBrokerModel messageInBroker, string serializedEvent) GetMessage(
            BasicDeliverEventArgs eventArgs,
            IModel channel,
            SqlConnection sqlConnection,
            IMessageInBrokerService messageInBrokerService,
            ILoggerService loggerService)
        {
            string eventName = null;
            string eventName_FullName = null;
            MessageInBrokerModel messageInBroker = null;
            string serializedEvent = null;


            using SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                ReadOnlyMemory<byte> body = eventArgs.Body;
                string postMessage = Encoding.UTF8.GetString(body.ToArray());

                eventName = GetValueInBasicProperties(eventArgs, key: "EventName");
                if (string.IsNullOrWhiteSpace(eventName))
                    throw new ArgumentNullException("eventName is null");


                eventName_FullName = GetValueInBasicProperties(eventArgs, key: "EventName_FullName");
                if (string.IsNullOrWhiteSpace(eventName_FullName))
                    throw new ArgumentNullException("eventName_FullName is null");


                string currentContextMessage = GetValueInBasicProperties(eventArgs, key: "CurrentContext");


                messageInBroker = JsonConvert.DeserializeObject<MessageInBrokerModel>(postMessage);
                serializedEvent = messageInBroker.Body;


                if (messageInBroker != null
                    && currentContextMessage != _environmentSettings.CurrentContext)
                {
                    MessageInBrokerModel messageReference = messageInBrokerService.GetMessageByMessageIdReference(
                        messageIdReference: messageInBroker.MessageId,
                        sqlConnection: sqlConnection,
                        sqlTransaction: sqlTransaction);

                    if (messageReference == null)
                        messageInBroker = messageInBrokerService.CreateMessage(
                            fullName: messageInBroker.FullName,
                            name: messageInBroker.Name,
                            currentContext: _environmentSettings.CurrentContext,
                            body: serializedEvent,
                            isEvent: true,
                            originalContext: messageInBroker.CurrentContext,
                            messageIdReference: messageInBroker.MessageId,
                            sqlConnection: sqlConnection,
                            sqlTransaction: sqlTransaction);
                    else
                        messageInBroker = messageReference;
                }

                sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (channel.IsOpen)
                    channel.BasicReject(deliveryTag: eventArgs.DeliveryTag, requeue: false);

                sqlTransaction.Rollback();
                loggerService
                    .LogErrorRegisterAsync(ex, "RabbitMQ; GetMessage: Erro ao deserializar mensagem")
                    .GetAwaiter().GetResult();

                throw;
            }


            return (eventName, eventName_FullName, messageInBroker, serializedEvent);
        }

        private string GetValueInBasicProperties(BasicDeliverEventArgs eventArgs, string key)
        {
            KeyValuePair<string, object> header = eventArgs.BasicProperties.Headers.FirstOrDefault(header_ => header_.Key == key);
            if (header.Equals(default(KeyValuePair<string, object>)))
                return null;

            return Encoding.UTF8.GetString(header.Value as byte[]);
        }

        private SqlConnection GetNewSqlConnection()
        {
            SqlConnection sqlConnection = ConnectionDatabase.NewConnection(environmentSettings: _environmentSettings);
            _sqlConnections.Add(sqlConnection);

            return sqlConnection;
        }
    }
}
