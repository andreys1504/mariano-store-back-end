using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Core.ApplicationLayer.EventsHandlers
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<DependenciesEventHandlerBase, DependenciesEventHandlerBase>();
        }
    }
}
