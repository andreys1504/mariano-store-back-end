using MarianoStore.Core.Application.Build;
using MarianoStore.Core.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Pagamento.Api
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
            EnvironmentSettings environmentSettings = ObjectEnvironmentSettings.Create(
                builder.Configuration, 
                projectName: "Pagamento.Api", 
                currentContext: Contexts.Pagamento.ToString(), 
                applicationLayer: "MarianoStore.Pagamento.Application",
                domainLayer: "MarianoStore.Pagamento.Domain");
            Ioc.ApplicationDependencies.Register(builder.Services, environmentSettings);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return environmentSettings;
        }

        private static void PostBuildApplication(WebApplication app, EnvironmentSettings environmentSettings)
        {
            if (environmentSettings.CurrentEnvironment != Environment.Production)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}