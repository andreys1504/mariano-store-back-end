using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido.Models;
using System;
using System.Threading.Tasks;

namespace MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido
{
    public interface IPedido_PedidosMarianoStoreService
    {
        Task<DadosPedidoModel> DadosPedido(Guid idPedido);
    }
}