using MarianoStore.Catalogo.Api.IntegrationEvents.Pedidos.Pedido.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Api.IntegrationEvents.Pedidos.Pedido.Handlers
{
    public class PedidoRealizadoSucessoEventHandler
    {
        public static Task Handle(PedidoRealizadoSucessoEvent @event, IServiceScope scope)
        {
            //scope.ServiceProvider.GetService<>();

            return Task.CompletedTask;
        }
    }
}
