using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido;
using MarianoStore.Core.Settings;
using MarianoStore.Infra.Services.ServicosMarianoStore.Pedidos.Pedido;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarianoStore.Infra.Services.ServicosMarianoStore
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services, EnvironmentSettings environmentSettings)
        {
            services.AddTransient<DependenciesMarianoStoreServiceBase, DependenciesMarianoStoreServiceBase>();

            //Pedidos
            services
                .AddHttpClient(name: Contexts.Pedidos.ToString(), client =>
                {
                    client.BaseAddress = new Uri(environmentSettings.ServicosMarianoStore.Servicos.First(srv => srv.Servico == Contexts.Pedidos.ToString()).UrlBase);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .AddTransientHttpErrorPolicy(_ => GetHttpErrorRetryPolicy())
                .AddTransientHttpErrorPolicy(_ => GetCircuitBreakerPolicy());

            services.AddTransient<IPedido_PedidosMarianoStoreService, Pedido_PedidosMarianoStoreService>();
        }

        //
        private static IAsyncPolicy<HttpResponseMessage> GetHttpErrorRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 2,
                    sleepDurationProvider => TimeSpan.FromSeconds(Math.Pow(2, sleepDurationProvider)),
                    onRetryAsync: (exception, _) =>
                    {
                        Console.WriteLine("GetHttpErrorRetryPolicy retrying...");
                        Console.WriteLine(exception.Result.StatusCode);

                        return Task.CompletedTask;
                    });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 3, durationOfBreak: TimeSpan.FromSeconds(5),
                    onBreak: (exception, _) =>
                    {
                        ShowCircuitState("Aberto (onBreak)", ConsoleColor.Red);
                    },
                    onReset: () =>
                    {
                        ShowCircuitState("Fechado (onReset)", ConsoleColor.Green);
                    },
                    onHalfOpen: () =>
                    {
                        ShowCircuitState("Semi-aberto (onHalfOpen)", ConsoleColor.Yellow);
                    });
        }

        private static void ShowCircuitState(string descStatus, ConsoleColor backgroundColor)
        {
            var previousBackgroundColor = Console.BackgroundColor;
            var previousForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Out.WriteLine($" ***** Estado do Circuito: {descStatus} **** ");

            Console.BackgroundColor = previousBackgroundColor;
            Console.ForegroundColor = previousForegroundColor;
        }
    }
}
