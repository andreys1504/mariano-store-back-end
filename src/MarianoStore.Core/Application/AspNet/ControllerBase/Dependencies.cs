using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Core.Application.AspNet.ControllerBase
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ApiControllerDependencies, ApiControllerDependencies>();
        }
    }
}
