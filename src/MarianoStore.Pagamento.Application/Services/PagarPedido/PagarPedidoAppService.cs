using MarianoStore.Core.ApplicationLayer.ApplicationsServices;
using MarianoStore.Pagamento.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.Services.PagarPedido
{
    public class PagarPedidoAppService : AppServiceBase, IRequestHandler<PagarPedidoRequest, bool>
    {
        public PagarPedidoAppService(DependenciesAppServiceBase dependencies) : base(dependencies)
        {
        }

        public async Task<bool> Handle(PagarPedidoRequest request, CancellationToken cancellationToken)
        {
            //acessar gateway pagamento


            //notificar sistema: pagamento realizado com sucesso
            var pagamentoRealizadoSucessoEvent = new PagamentoRealizadoSucessoEvent
            {
                SucessoPagamento = true
            };

            await base.SendEventToQueueAsync(pagamentoRealizadoSucessoEvent);

            return true;
        }
    }
}
