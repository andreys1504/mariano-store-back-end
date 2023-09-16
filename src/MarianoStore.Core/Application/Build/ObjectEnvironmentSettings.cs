using MarianoStore.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace MarianoStore.Core.Application.Build
{
    public static class ObjectEnvironmentSettings
    {
        public static EnvironmentSettings Create(
            IConfiguration configuration,
            string projectName,
            string currentContext,
            string applicationLayer,
            string domainLayer)
        {
            var environmentSettings = new EnvironmentSettings(
                currentEnvironment: CurrentEnvironmentBuildConfigurations.GetCurrentEnvironmentName(configuration),
                projectName: projectName,
                currentContext: currentContext,
                applicationLayer: applicationLayer,
                domainLayer: domainLayer);
            AppSettingsJsonBuildConfigurations.GetConfigurationsInAppSettingsJson(configuration, environmentSettings);
            environmentSettings.GetConfigurationsInEnvironment(configuration);

            return environmentSettings;
        }
    }
}
