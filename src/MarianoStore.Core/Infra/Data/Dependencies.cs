using MarianoStore.Core.Ioc;
using MarianoStore.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace MarianoStore.Core.Infra.Data
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services, EnvironmentSettings environmentSettings)
        {
            services.AddTransientWithRetry<SqlConnection, SqlException>(serviceProvider =>
            {
                return ConnectionDatabase.GetConnection(environmentSettings);
            });
        }
    }
}
