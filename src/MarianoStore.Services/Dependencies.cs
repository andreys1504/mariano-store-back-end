using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Core.Services.Email;
using MarianoStore.Core.Services.Logger;
using MarianoStore.Services.Email;
using MarianoStore.Services.Logger;
using MarianoStore.Services.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Services
{
    public static class Dependencies
    {
        //exceto RabbitMq (.RabbitMq.Dependencies)
        public static void Register(IServiceCollection services)
        {
            //Email
            services.AddTransient<IEmailService, EmailService>();

            //Logger
            services.AddTransient<ILoggerService, LoggerService>();

            //Messages
            services.AddTransient<IMessageInBrokerService, MessageInBrokerService>();
        }
    }
}
