using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido;
using MarianoStore.Core.Infra.Services.ServicosMarianoStore.Pedidos.Pedido.Models;
using MarianoStore.Core.Settings;
using System;
using System.Threading.Tasks;

namespace MarianoStore.Infra.Services.ServicosMarianoStore.Pedidos.Pedido
{
    public class Pedido_PedidosMarianoStoreService : MarianoStoreServiceBase, IPedido_PedidosMarianoStoreService
    {
        public Pedido_PedidosMarianoStoreService(
            DependenciesMarianoStoreServiceBase dependencies) : base(dependencies)
        {
        }

        public async Task<DadosPedidoModel> DadosPedido(Guid idPedido)
        {
            return await ExecuteRequestAsync<DadosPedidoModel>(
                context: Contexts.Pedidos, 
                executeAsync: async (httpClient) => await httpClient.GetAsync($"/pedido/{idPedido}"));
        }
    }
}
