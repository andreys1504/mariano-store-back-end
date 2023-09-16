using MarianoStore.Core.ApplicationLayer.EventsHandlers;
using MarianoStore.Pagamento.Application.IntegrationEvents.Events;
using MarianoStore.Pagamento.Application.Services.PagarPedido;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : EventHandlerBase, INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        public PedidoRealizadoSucessoEventHandler(
            EventHandlerDependencies eventHandlerDependencies) : base(eventHandlerDependencies)
        {
        }

        public async Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            var pagarPedidoRequest = new PagarPedidoRequest();

            await base.SendCommandToHandlerAsync(pagarPedidoRequest);
        }
    }
}
