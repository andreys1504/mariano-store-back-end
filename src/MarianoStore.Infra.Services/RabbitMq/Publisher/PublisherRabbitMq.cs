using MarianoStore.Core.Infra.Data;
using MarianoStore.Core.Infra.Services.Logger;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Core.Messages;
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

        public async Task PublishCommandAsync<TCommand>(TCommand @command) where TCommand : Command
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var publishersSetup = scope.ServiceProvider.GetService<IList<PublisherSetup>>();
            PublisherSetup publishSetup = publishersSetup.First(publish => publish.ObjectFullName == @command.GetType().FullName);

            var messageInBrokerService = scope.ServiceProvider.GetService<IMessageInBrokerService>();
            var loggerService = scope.ServiceProvider.GetService<ILoggerService>();

            IModel channel = publishSetup.PublishChannel;


            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>
            {
                { "Content-Type", "application/json" },
                { "CommandName_Name", @command.GetType().Name },
                { "CommandName", @command.GetType().FullName },
                { "CurrentContext", _environmentSettings.CurrentContext }
            };

            if (publishSetup.Priority.HasValue)
                basicProperties.Priority = publishSetup.Priority.Value;

            basicProperties.DeliveryMode = 2;
            basicProperties.Expiration = publishSetup.ExpirationMessage;
            basicProperties.MessageId = Guid.NewGuid().ToString("D");


            using (var sqlConnection = ConnectionDatabase.GetConnection(environmentSettings: _environmentSettings))
            {
                using SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                try
                {
                    MessageInBrokerModel messageInBroker = messageInBrokerService.CreateMessage(
                        sqlConnection: sqlConnection,
                        sqlTransaction: sqlTransaction,
                        name: @command.GetType().FullName,
                        currentContext: _environmentSettings.CurrentContext,
                        body: JsonConvert.SerializeObject(@command),
                        isEvent: false,
                        originalContext: null,
                        messageIdReference: null
                    );

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

        public async Task PublishEventAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var publishersSetup = scope.ServiceProvider.GetService<IList<PublisherSetup>>();
            PublisherSetup publishSetup = publishersSetup.First(publish => publish.ObjectFullName == @event.GetType().FullName);

            var messageInBrokerService = scope.ServiceProvider.GetService<IMessageInBrokerService>();
            var loggerService = scope.ServiceProvider.GetService<ILoggerService>();

            IModel channel = publishSetup.PublishChannel;


            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>
            {
                { "Content-Type", "application/json" },
                { "EventName_Name", @event.GetType().Name },
                { "EventName", @event.GetType().FullName },
                { "CurrentContext", _environmentSettings.CurrentContext }
            };
            basicProperties.DeliveryMode = 2;
            basicProperties.Expiration = publishSetup.ExpirationMessage;
            basicProperties.MessageId = Guid.NewGuid().ToString("D");


            using (var sqlConnection = ConnectionDatabase.GetConnection(environmentSettings: _environmentSettings))
            {
                using SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                try
                {
                    MessageInBrokerModel message = messageInBrokerService.CreateMessage(
                        sqlConnection: sqlConnection,
                        sqlTransaction: sqlTransaction,
                        name: @event.GetType().FullName,
                        currentContext: _environmentSettings.CurrentContext,
                        body: JsonConvert.SerializeObject(@event),
                        isEvent: true,
                        originalContext: null,
                        messageIdReference: null,
                        processed: true
                    );

                    string postMessage = JsonConvert.SerializeObject(message);
                    byte[] body = Encoding.UTF8.GetBytes(postMessage);

                    channel.BasicPublish(
                        exchange: publishSetup.ExchangeName,
                        routingKey: publishSetup.RoutingKey,
                        mandatory: true,
                        basicProperties: basicProperties,
                        body: new ReadOnlyMemory<byte>(body)
                    );

                    channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));

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
