using MarianoStore.Core.Services.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Core.Services
{
    public static class Dependencies
    {
        //exceto RabbitMq (.RabbitMq.Dependencies)
        public static void Register(IServiceCollection services)
        {
            //Logger
            services.AddTransient<ILoggerService, LoggerService>();
        }
    }
}
