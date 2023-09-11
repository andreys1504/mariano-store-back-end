using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pedidos.Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Application.Services.NovoPedido
{
    public class NovoPedidoAppService : IRequestHandler<NovoPedidoRequest, bool>
    {
        private readonly IPublisherRabbitMq _publisherRabbitMq;

        public NovoPedidoAppService(IPublisherRabbitMq publisherRabbitMq)
        {
            _publisherRabbitMq = publisherRabbitMq;
        }

        public async Task<bool> Handle(NovoPedidoRequest request, CancellationToken cancellationToken)
        {
            //cria novo pedido
            var pedido = new
            {
                Id = Guid.NewGuid(),
            };

            //recupera produto do repositório
            var produto = new
            {
                Id = Guid.NewGuid(),
            };


            //Notifica todo o sistema que um pedido foi realizado com sucesso
            var pedidoRealizadoSucessoEvent = new PedidoRealizadoSucessoEvent
            {
                IdPedido = pedido.Id,
                IdProduto = produto.Id,
                Quantidade = 1
            };

            await _publisherRabbitMq.PublishEventAsync(pedidoRealizadoSucessoEvent);

            return true;
        }
    }
}
