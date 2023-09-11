using MarianoStore.Core.Messages.MessageInBroker;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Core.Messages
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            //MessageInBroker
            services.AddTransient<MessageInBrokerService, MessageInBrokerService>();
        }
    }
}
