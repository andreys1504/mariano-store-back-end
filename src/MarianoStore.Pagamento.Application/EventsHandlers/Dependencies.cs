using MarianoStore.Pagamento.Application.EventsHandlers.PagamentoRealizadoSucesso;
using Microsoft.Extensions.DependencyInjection;

namespace MarianoStore.Pagamento.Application.EventsHandlers
{
    public static class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<PagamentoRealizadoSucessoEventHandler, PagamentoRealizadoSucessoEventHandler>();
        }
    }
}
