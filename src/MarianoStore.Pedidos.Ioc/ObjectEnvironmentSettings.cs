using MarianoStore.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace MarianoStore.Pedidos.Ioc
{
    public class ObjectEnvironmentSettings
    {
        public static EnvironmentSettings Create(IConfiguration configuration, string projectName)
        {
             return Core.Application.Build.ObjectEnvironmentSettings.Create(
                configuration,
                projectName: projectName,
                currentContext: Contexts.Pedidos.ToString(),
                applicationLayer: "MarianoStore.Pedidos.Application",
                domainLayer: "MarianoStore.Pedidos.Domain");
        }
    }
}
