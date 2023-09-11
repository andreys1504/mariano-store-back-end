using MarianoStore.Core.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace MarianoStore.Core.Services.RabbitMq
{
    public class HelpersRabbitMq
    {
        public static void SendCommandToHandler(
            string serializedCommand, 
            string commandName, 
            IServiceScope scope)
        {
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            var environmentSettings = scope.ServiceProvider.GetService<EnvironmentSettings>();

            Assembly assembly = AppDomain.CurrentDomain.Load(environmentSettings.ApplicationLayer);
            Type type = assembly.GetType(commandName);
            mediator.Send(JsonConvert.DeserializeObject(serializedCommand, type)).Wait();
        }

        public static void SendEventToHandler(
            string serializedEvent,
            string eventName,
            IServiceScope scope)
        {
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            var environmentSettings = scope.ServiceProvider.GetService<EnvironmentSettings>();

            Assembly assembly = AppDomain.CurrentDomain.Load(environmentSettings.DomainLayer);
            Type type = assembly.GetType(eventName);
            mediator.Publish(JsonConvert.DeserializeObject(serializedEvent, type)).Wait();
        }
    }
}
