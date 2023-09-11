using Microsoft.Extensions.Configuration;

namespace MarianoStore.Core.BuildApplications.Build
{
    public static class CurrentEnvironmentBuildConfigurations
    {
        public static string GetCurrentEnvironmentName(IConfiguration configuration)
        {
            string currentEnvironment = configuration["CURRENT_ENVIRONMENT"];
            if (string.IsNullOrWhiteSpace(currentEnvironment))
                currentEnvironment = configuration.GetSection("EnvironmentSettings:CurrentEnvironment").Value;

            return currentEnvironment;
        }
    }
}
