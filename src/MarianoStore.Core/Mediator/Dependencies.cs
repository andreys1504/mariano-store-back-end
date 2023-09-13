using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Core.Mediator
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IMediatorHandler, MediatorHandler>();
        }
    }
}
