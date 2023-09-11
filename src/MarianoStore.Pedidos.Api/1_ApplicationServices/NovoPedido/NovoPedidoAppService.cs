using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pedidos.Api.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Api.ApplicationServices.NovoPedido
{
    public class NovoPedidoAppService
    {
        private readonly IPublisherRabbitMq _publisherRabbitMq;

        public NovoPedidoAppService(IServiceScope scope)
        {
            _publisherRabbitMq = scope.ServiceProvider.GetService<IPublisherRabbitMq>();
        }

        public async Task Handle(NovoPedidoRequest request)
        {
            var pedido = new
            {
                Id = Guid.NewGuid(),
            };

            var produto = new
            {
                Id = Guid.NewGuid(),
            };


            var pedidoRealizadoSucessoEvent = new PedidoRealizadoSucessoEvent
            {
                IdPedido = pedido.Id,
                IdProduto = produto.Id,
                Quantidade = 1
            };

            await _publisherRabbitMq.PublishEventAsync(pedidoRealizadoSucessoEvent);
        }
    }
}
