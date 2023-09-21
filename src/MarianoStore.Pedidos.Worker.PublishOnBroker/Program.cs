using MarianoStore.Core.Settings;
using Microsoft.AspNetCore.Builder;

namespace MarianoStore.Pedidos.Worker.PublishOnBroker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            EnvironmentSettings environmentSettings = BuildApplication(builder);

            WebApplication app = builder.Build();
            PostBuildApplication(app, environmentSettings);
        }


        //
        private static EnvironmentSettings BuildApplication(WebApplicationBuilder builder)
        {
            EnvironmentSettings environmentSettings = Ioc.ObjectEnvironmentSettings.Create(
                builder.Configuration,
                projectName: "Pedidos.Worker.PublishOnBroker");
            ApplicationDependencies.Register(builder.Services, environmentSettings);


            return environmentSettings;
        }

        private static void PostBuildApplication(WebApplication app, EnvironmentSettings environmentSettings)
        {
            app.Run();
        }
    }
}