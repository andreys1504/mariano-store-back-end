using MarianoStore.Core.Infra.Services.Email;
using MarianoStore.Core.Infra.Services.Logger;
using MarianoStore.Core.Messages.MessageInBroker;
using MarianoStore.Infra.Services.Email;
using MarianoStore.Infra.Services.Logger;
using MarianoStore.Infra.Services.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Infra.Services
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
