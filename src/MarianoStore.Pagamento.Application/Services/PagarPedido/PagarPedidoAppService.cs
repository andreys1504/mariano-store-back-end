using MarianoStore.Core.Mediator;
using MarianoStore.Pagamento.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.Services.PagarPedido
{
    public class PagarPedidoAppService : IRequestHandler<PagarPedidoRequest, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PagarPedidoAppService(
            IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(PagarPedidoRequest request, CancellationToken cancellationToken)
        {
            //acessar gateway pagamento


            //notificar sistema: pagamento realizado com sucesso
            var pagamentoRealizadoSucessoEvent = new PagamentoRealizadoSucessoEvent
            {
                SucessoPagamento = true
            };

            await _mediatorHandler.SendEventToQueueAsync(pagamentoRealizadoSucessoEvent);

            return true;
        }
    }
}
