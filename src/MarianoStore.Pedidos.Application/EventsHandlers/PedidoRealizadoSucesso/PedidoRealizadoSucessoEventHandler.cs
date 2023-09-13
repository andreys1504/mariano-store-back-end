using MarianoStore.Pedidos.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Application.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        public PedidoRealizadoSucessoEventHandler()
        {
        }

        public Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
