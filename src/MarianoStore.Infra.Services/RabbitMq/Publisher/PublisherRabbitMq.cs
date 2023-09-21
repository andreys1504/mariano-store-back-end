using MarianoStore.Core.Infra.Data;
using MarianoStore.Core.Infra.Services.Logger;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Core.Messages.MessageInBroker.Models;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianoStore.Infra.Services.RabbitMq.Publisher
{
    public class PublisherRabbitMq : IPublisherRabbitMq
    {
        private readonly EnvironmentSettings _environmentSettings;
        private readonly IServiceProvider _serviceProvider;

        public PublisherRabbitMq(
            EnvironmentSettings environmentSettings,
            IServiceProvider serviceProvider)
        {
            _environmentSettings = environmentSettings;
            _serviceProvider = serviceProvider;
        }

        public async Task PublishCommandAsync(object @command, MessageInBrokerModel messageInBroker = null)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var messageInBrokerService = scope.ServiceProvider.GetService<IMessageInBrokerService>();

            string commandName_FullName = "";
            string commandName = "";
            if (@command == null)
            {
                commandName_FullName = messageInBroker.FullName;
                commandName = messageInBroker.Name;
            }
            else
            {
                commandName_FullName = @command.GetType().FullName;
                commandName = @command.GetType().Name;
            }

            #region CreateMessage

            if (messageInBroker == null)
                using (var sqlConnection = ConnectionDatabase.NewConnection(environmentSettings: _environmentSettings))
                {
                    try
                    {
                        messageInBroker = messageInBrokerService.CreateMessage(
                            fullName: commandName_FullName,
                            name: commandName,
                            currentContext: _environmentSettings.CurrentContext,
                            body: JsonConvert.SerializeObject(@command),
                            isEvent: false,
                            originalContext: null,
                            messageIdReference: null,
                            sqlConnection: sqlConnection,
                            sqlTransaction: null
                        );
                    }
                    catch
                    {
                        return;
                    }
                }

            #endregion


            var publishersSetup = scope.ServiceProvider.GetService<IList<PublisherSetup>>();
            PublisherSetup publishSetup = publishersSetup.First(publish => publish.ObjectFullName == commandName_FullName);
            var loggerService = scope.ServiceProvider.GetService<ILoggerService>();

            IModel channel = publishSetup.PublishChannel;


            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>
            {
                { "Content-Type", "application/json" },
                { "CommandName_FullName", commandName_FullName },
                { "CommandName", commandName },
                { "CurrentContext", _environmentSettings.CurrentContext }
            };

            if (publishSetup.Priority.HasValue)
                basicProperties.Priority = publishSetup.Priority.Value;

            basicProperties.DeliveryMode = 2;
            basicProperties.Expiration = publishSetup.ExpirationMessage;
            basicProperties.MessageId = Guid.NewGuid().ToString("D");

            using (var sqlConnection = ConnectionDatabase.NewConnection(environmentSettings: _environmentSettings))
            {
                using SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                try
                {
                    messageInBroker = messageInBrokerService.GetMessageByMessageId(messageId: messageInBroker.MessageId, sqlConnection: sqlConnection, sqlTransaction: sqlTransaction);
                    if (messageInBroker.MessageInBroker.HasValue)
                        return;


                    string postMessage = JsonConvert.SerializeObject(messageInBroker);
                    byte[] body = Encoding.UTF8.GetBytes(postMessage);

                    channel.BasicPublish(
                        exchange: publishSetup.ExchangeName,
                        routingKey: publishSetup.RoutingKey,
                        mandatory: true,
                        basicProperties: basicProperties,
                        body: new ReadOnlyMemory<byte>(body)
                    );

                    channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));

                    messageInBrokerService.MarkAsMessageInBroker(messageInBroker, sqlConnection: sqlConnection, sqlTransaction: sqlTransaction);
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    await loggerService.LogErrorRegisterAsync(ex, "RabbitMQ; PublishCommandAsync: Erro ao publicar mensagem");

                    throw;
                }
            }
        }

        public async Task PublishEventAsync(object @event, MessageInBrokerModel messageInBroker = null)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var messageInBrokerService = scope.ServiceProvider.GetService<IMessageInBrokerService>();

            string eventName_FullName = "";
            string eventName = "";
            if (@event == null)
            {
                eventName_FullName = messageInBroker.FullName;
                eventName = messageInBroker.Name;
            }
            else
            {
                eventName_FullName = @event.GetType().FullName;
                eventName = @event.GetType().Name;
            }

            #region CreateMessage

            if (messageInBroker == null)
                using (var sqlConnection = ConnectionDatabase.NewConnection(environmentSettings: _environmentSettings))
                {
                    try
                    {
                        messageInBroker = messageInBrokerService.CreateMessage(
                            fullName: eventName_FullName,
                            name: eventName,
                            currentContext: _environmentSettings.CurrentContext,
                            body: JsonConvert.SerializeObject(@event),
                            isEvent: true,
                            originalContext: null,
                            messageIdReference: null,
                            sqlConnection: sqlConnection,
                            sqlTransaction: null
                        );
                    }
                    catch
                    {
                        return;
                    }
                }

            #endregion


            var publishersSetup = scope.ServiceProvider.GetService<IList<PublisherSetup>>();
            PublisherSetup publishSetup = publishersSetup.First(publish => publish.ObjectFullName == eventName_FullName);
            var loggerService = scope.ServiceProvider.GetService<ILoggerService>();

            IModel channel = publishSetup.PublishChannel;


            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>
            {
                { "Content-Type", "application/json" },
                { "EventName_FullName", eventName_FullName },
                { "EventName", eventName },
                { "CurrentContext", _environmentSettings.CurrentContext }
            };
            basicProperties.DeliveryMode = 2;
            basicProperties.Expiration = publishSetup.ExpirationMessage;
            basicProperties.MessageId = Guid.NewGuid().ToString("D");

            using (var sqlConnection = ConnectionDatabase.NewConnection(environmentSettings: _environmentSettings))
            {
                using SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                try
                {
                    messageInBroker = messageInBrokerService.GetMessageByMessageId(messageId: messageInBroker.MessageId, sqlConnection: sqlConnection, sqlTransaction: sqlTransaction);
                    if (messageInBroker.MessageInBroker.HasValue)
                        return;


                    string postMessage = JsonConvert.SerializeObject(messageInBroker);
                    byte[] body = Encoding.UTF8.GetBytes(postMessage);

                    channel.BasicPublish(
                        exchange: publishSetup.ExchangeName,
                        routingKey: publishSetup.RoutingKey,
                        mandatory: true,
                        basicProperties: basicProperties,
                        body: new ReadOnlyMemory<byte>(body)
                    );

                    channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));

                    messageInBrokerService.MarkAsMessageInBroker(messageInBroker, sqlConnection: sqlConnection, sqlTransaction: sqlTransaction);
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    await loggerService.LogErrorRegisterAsync(ex, "RabbitMQ; PublishEventAsync: Erro ao publicar mensagem");

                    throw;
                }
            }
        }
    }
}
