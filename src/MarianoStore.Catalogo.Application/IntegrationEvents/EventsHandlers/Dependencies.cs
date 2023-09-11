using MarianoStore.Catalogo.Application.IntegrationEvents.EventsHandlers.PagamentoRealizadoSucesso;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Catalogo.Application.IntegrationEvents.EventsHandlers
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<PagamentoRealizadoSucessoEventHandler, PagamentoRealizadoSucessoEventHandler>();
        }
    }
}
