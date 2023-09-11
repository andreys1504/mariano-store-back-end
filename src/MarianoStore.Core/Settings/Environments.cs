using System.Collections.Generic;

namespace MarianoStore
{
    public enum Environment
    {
        Local,
        Development,
        Production
    }

    public static class Environments
    {
        public static IList<Environment> GetEnvironments
        {
            get
            {
                return new List<Environment>
                {
                    Environment.Local,
                    Environment.Development,
                    Environment.Production
                };
            }
        }

        public static IList<(Environment, string)> GetEnvironmentShortName
        {
            get
            {
                return new List<(Environment, string)>
                {
                    (Environment.Local, "local"),
                    (Environment.Development, "dev"),
                    (Environment.Production, "prod")
                };
            }
        }
    }
}
