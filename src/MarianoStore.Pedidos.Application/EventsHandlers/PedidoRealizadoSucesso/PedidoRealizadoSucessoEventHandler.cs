using MarianoStore.Core.ApplicationLayer.EventsHandlers;
using MarianoStore.Pedidos.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Application.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : EventHandlerBase, INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        public PedidoRealizadoSucessoEventHandler(EventHandlerDependencies eventHandlerDependencies) : base(eventHandlerDependencies)
        {
        }

        public Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
