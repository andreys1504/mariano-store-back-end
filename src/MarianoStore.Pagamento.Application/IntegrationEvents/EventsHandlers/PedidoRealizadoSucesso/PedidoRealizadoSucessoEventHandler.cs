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
        private readonly IPedido_PedidosStoreService _pedido_PedidosStoreService;

        public PedidoRealizadoSucessoEventHandler(
            IPedido_PedidosStoreService pedido_PedidosStoreService,
            EventHandlerDependencies dependencies) : base(dependencies)
        {
            _pedido_PedidosStoreService = pedido_PedidosStoreService;
        }

        public async Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            var idPedido = Guid.NewGuid();
            DadosPedidoModel pedido = await _pedido_PedidosStoreService.DadosPedido(idPedido: idPedido);


            var pagarPedidoRequest = new PagarPedidoRequest
            {
                IdPedido = pedido.IdPedido,
            };

            await base.SendCommandToHandlerAsync(pagarPedidoRequest);
        }
    }
}
