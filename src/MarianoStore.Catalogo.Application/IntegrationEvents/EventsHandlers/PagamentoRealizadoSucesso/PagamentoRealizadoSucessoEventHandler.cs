using MarianoStore.Catalogo.Application.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Application.IntegrationEvents.EventsHandlers.PagamentoRealizadoSucesso
{
    public class PagamentoRealizadoSucessoEventHandler
    {
        public Task Handle(PagamentoRealizadoSucessoEvent @event)
        {
            //realizar baixa estoque produto

            return Task.CompletedTask;
        }
    }
}
