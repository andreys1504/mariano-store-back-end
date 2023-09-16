using MarianoStore.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MarianoStore.Core.Application.Build
{
    public static class AppSettingsJsonBuildConfigurations
    {
        public static void GetConfigurationsInAppSettingsJson(
            IConfiguration configuration,
            EnvironmentSettings environmentSettings)
        {
            string currentEnvironment = configuration["CURRENT_ENVIRONMENT"];
            if (string.IsNullOrWhiteSpace(currentEnvironment) == false) //não recuperar configs de appsettings.json nos ambientes
                return;

            IConfigurationSection environmentSettingsSection = configuration.GetSection(nameof(EnvironmentSettings));
            new ConfigureFromConfigurationOptions<EnvironmentSettings>(environmentSettingsSection).Configure(environmentSettings);
        }

        public static IConfigurationRoot CreateConfiguration(IConfigurationBuilder configurationBuilder)
        {
            IConfigurationRoot configuration =
                configurationBuilder
                    .AddEnvironmentVariables()
                    .Build();

            string currentEnvironment = configuration["CURRENT_ENVIRONMENT"];
            if (string.IsNullOrWhiteSpace(currentEnvironment) == false)
                return configuration;


            return configurationBuilder
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static IConfigurationRoot CreateConfiguration(string applicationRootPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(applicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
