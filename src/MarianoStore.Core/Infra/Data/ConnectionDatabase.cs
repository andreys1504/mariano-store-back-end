using MarianoStore.Core.Settings;
using System.Data.SqlClient;

namespace MarianoStore.Core.Infra.Data
{
    public class ConnectionDatabase
    {
        public static SqlConnection GetConnection(EnvironmentSettings environmentSettings)
        {
            var sqlConnection = new SqlConnection(connectionString: environmentSettings.SqlServerConnectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
