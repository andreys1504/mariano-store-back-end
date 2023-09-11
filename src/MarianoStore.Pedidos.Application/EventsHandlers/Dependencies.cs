using MarianoStore.Pedidos.Application.EventsHandlers.PedidoRealizadoSucesso;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Pedidos.Application.EventsHandlers
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<PedidoRealizadoSucessoEventHandler, PedidoRealizadoSucessoEventHandler>();
        }
    }
}
