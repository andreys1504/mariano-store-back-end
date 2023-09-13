using MarianoStore.Core.Mediator;
using MarianoStore.Pagamento.Application.IntegrationEvents.Events;
using MarianoStore.Pagamento.Application.Services.PagarPedido;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoRealizadoSucessoEventHandler(
            IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            var pagarPedidoRequest = new PagarPedidoRequest();

            await _mediatorHandler.SendCommandToHandlerAsync(pagarPedidoRequest);
        }
    }
}
