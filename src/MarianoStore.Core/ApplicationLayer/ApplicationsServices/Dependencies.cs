using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Core.ApplicationLayer.ApplicationsServices
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<DependenciesAppServiceBase, DependenciesAppServiceBase>();
        }
    }
}
