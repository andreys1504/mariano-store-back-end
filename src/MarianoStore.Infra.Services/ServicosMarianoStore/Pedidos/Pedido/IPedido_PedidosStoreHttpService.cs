using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace MarianoStore.Infra.Services.ServicosMarianoStore.Pedidos.Pedido
{
    public interface IPedido_PedidosStoreHttpService
    {
        [Get("/pedido/{idPedido}")]
        Task<DadosPedidoModel> DadosPedido([AliasAs("idPedido")] Guid idPedido);
    }
}
