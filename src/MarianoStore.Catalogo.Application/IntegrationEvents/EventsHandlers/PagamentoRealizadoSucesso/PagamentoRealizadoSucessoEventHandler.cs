using MarianoStore.Catalogo.Application.IntegrationEvents.Events;
using MarianoStore.Catalogo.Application.Services.BaixaEstoque;
using MarianoStore.Core.Mediator;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Application.IntegrationEvents.EventsHandlers.PagamentoRealizadoSucesso
{
    public class PagamentoRealizadoSucessoEventHandler : INotificationHandler<PagamentoRealizadoSucessoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoRealizadoSucessoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(PagamentoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            var baixaEstoqueRequest = new BaixaEstoqueRequest();

            await _mediatorHandler.SendCommandToHandlerAsync(baixaEstoqueRequest);
        }
    }
}
