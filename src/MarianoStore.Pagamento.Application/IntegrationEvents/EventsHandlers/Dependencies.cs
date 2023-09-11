using MarianoStore.Pagamento.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Pagamento.Application.IntegrationEvents.EventsHandlers
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<PedidoRealizadoSucessoEventHandler, PedidoRealizadoSucessoEventHandler>();
        }
    }
}
