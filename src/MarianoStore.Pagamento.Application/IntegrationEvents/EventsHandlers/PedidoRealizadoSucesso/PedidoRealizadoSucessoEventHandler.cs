using MarianoStore.Core.ApplicationLayer.EventsHandlers;
using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido;
using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido.Models;
using MarianoStore.Pagamento.Application.IntegrationEvents.Events;
using MarianoStore.Pagamento.Application.Services.PagarPedido;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : EventHandlerBase, INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        private readonly IPedido_PedidosMarianoStoreService _pedido_PedidosMarianoStoreService;

        public PedidoRealizadoSucessoEventHandler(
            IPedido_PedidosMarianoStoreService pedido_PedidosMarianoStoreService,
            DependenciesEventHandlerBase dependencies) : base(dependencies)
        {
            _pedido_PedidosMarianoStoreService = pedido_PedidosMarianoStoreService;
        }

        public async Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            var idPedido = Guid.NewGuid();
            DadosPedidoModel pedido = await _pedido_PedidosMarianoStoreService.DadosPedido(idPedido: idPedido);


            var pagarPedidoRequest = new PagarPedidoRequest
            {
                IdPedido = pedido.IdPedido,
            };

            await base.SendCommandToHandlerAsync(pagarPedidoRequest);
        }
    }
}
