using MarianoStore.Core.ApplicationLayer.EventsHandlers;
using MarianoStore.Pedidos.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Application.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : EventHandlerBase, INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        public PedidoRealizadoSucessoEventHandler(DependenciesEventHandlerBase dependencies) : base(dependencies)
        {
        }

        public Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
