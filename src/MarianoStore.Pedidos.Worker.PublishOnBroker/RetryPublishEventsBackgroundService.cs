using MarianoStore.Core.Infra.Data;
using MarianoStore.Core.Infra.Services.RabbitMq.Publisher;
using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Core.Messages.MessageInBroker.Models;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Worker.PublishOnBroker
{
    public class RetryPublishEventsBackgroundService : BackgroundService
    {
        private readonly EnvironmentSettings _environmentSettings;
        private readonly IServiceProvider _serviceProvider;

        public RetryPublishEventsBackgroundService(
            EnvironmentSettings environmentSettings,
            IServiceProvider serviceProvider)
        {
            _environmentSettings = environmentSettings;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                await PublishEventsAsync(stoppingToken);
            }
        }

        //
        public async Task PublishEventsAsync(CancellationToken stoppingToken)
        {
            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();

                IEnumerable<MessageInBrokerModel> messages;
                using (SqlConnection sqlConnection = ConnectionDatabase.NewConnection(environmentSettings: _environmentSettings))
                {
                    var messageInBrokerService = scope.ServiceProvider.GetService<IMessageInBrokerService>();
                    messages = messageInBrokerService.GetMessagesToPublish(isEvent: true, secondsDelay: 3, sqlConnection: sqlConnection, sqlTransaction: null);
                }


                if (messages?.Count() == 0)
                    return;


                var publisherRabbitMq = scope.ServiceProvider.GetService<IPublisherRabbitMq>();

                foreach (MessageInBrokerModel message in messages)
                    await publisherRabbitMq.PublishEventAsync(@event: null, messageInBroker: message);


                await Task.Delay(1000, stoppingToken);
            }
            catch (Exception)
            {
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
