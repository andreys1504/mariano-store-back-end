using MarianoStore.Pedidos.Application.Services.NovoPedido;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Pedidos.Application.Services
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<NovoPedidoAppService, NovoPedidoAppService>();
        }
    }
}
