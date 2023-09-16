using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido;
using MarianoStore.Core.Settings;
using MarianoStore.Infra.Services.ServicosMarianoStore.Pedidos.Pedido;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Linq;

namespace MarianoStore.Infra.Services.ServicosMarianoStore
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services, EnvironmentSettings environmentSettings)
        {
            //Pedidos
            services.AddTransient<IPedido_PedidosStoreService, Pedido_PedidosStoreService>();
            services.AddRefitClient<IPedido_PedidosStoreHttpService>().ConfigureHttpClient(configure =>
            {
                configure.BaseAddress = new Uri(environmentSettings.ServicosMarianoStore.Servicos.First(srv => srv.Servico == Contexts.Pedidos.ToString()).UrlBase);
            });
        }
    }
}
