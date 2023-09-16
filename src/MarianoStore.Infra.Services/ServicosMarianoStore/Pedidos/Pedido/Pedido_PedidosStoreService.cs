using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido;
using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido.Models;
using System;
using System.Threading.Tasks;

namespace MarianoStore.Infra.Services.ServicosMarianoStore.Pedidos.Pedido
{
    public class Pedido_PedidosStoreService : IPedido_PedidosStoreService
    {
        private readonly IPedido_PedidosStoreHttpService _pedido_PedidosStoreHttpService;

        public Pedido_PedidosStoreService(IPedido_PedidosStoreHttpService pedido_PedidosStoreHttpService)
        {
            _pedido_PedidosStoreHttpService = pedido_PedidosStoreHttpService;
        }

        public async Task<DadosPedidoModel> DadosPedido(Guid idPedido)
        {
            return await _pedido_PedidosStoreHttpService.DadosPedido(idPedido);
        }
    }
}
