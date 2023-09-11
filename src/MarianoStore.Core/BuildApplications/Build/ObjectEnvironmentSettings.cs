using MarianoStore.Core.BuildApplications.Build;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace MarianoStore.Core.BuildApplications.Build
{
    public static class ObjectEnvironmentSettings
    {
        public static EnvironmentSettings Create(
            IConfiguration configuration,
            string projectName,
            string currentContext)
        {
            var environmentSettings = new EnvironmentSettings(
                currentEnvironment: CurrentEnvironmentBuildConfigurations.GetCurrentEnvironmentName(configuration),
                projectName: projectName,
                currentContext: currentContext);
            AppSettingsJsonBuildConfigurations.GetConfigurationsInAppSettingsJson(configuration, environmentSettings);
            environmentSettings.GetConfigurationsInEnvironment(configuration);

            return environmentSettings;
        }
    }
}
