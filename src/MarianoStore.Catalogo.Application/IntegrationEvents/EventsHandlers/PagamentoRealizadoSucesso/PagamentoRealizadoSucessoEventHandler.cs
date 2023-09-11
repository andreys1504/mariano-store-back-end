using MarianoStore.Catalogo.Application.IntegrationEvents.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Application.IntegrationEvents.EventsHandlers.PagamentoRealizadoSucesso
{
    public class PagamentoRealizadoSucessoEventHandler : INotificationHandler<PagamentoRealizadoSucessoEvent>
    {
        public Task Handle(PagamentoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            //realizar baixa estoque produto

            return Task.CompletedTask;
        }
    }
}
