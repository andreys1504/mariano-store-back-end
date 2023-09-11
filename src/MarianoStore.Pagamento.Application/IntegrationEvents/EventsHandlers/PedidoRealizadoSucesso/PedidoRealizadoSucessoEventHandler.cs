using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pagamento.Application.IntegrationEvents.Events;
using MarianoStore.Pagamento.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        private readonly IPublisherRabbitMq _publisherRabbitMq;

        public PedidoRealizadoSucessoEventHandler( 
            IPublisherRabbitMq publisherRabbitMq)
        {
            _publisherRabbitMq = publisherRabbitMq;
        }

        public async Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            //acessar gateway pagamento


            //notificar sistema pagamento realizado com sucesso
            var pagamentoRealizadoSucessoEvent = new PagamentoRealizadoSucessoEvent();

            await _publisherRabbitMq.PublishEventAsync(pagamentoRealizadoSucessoEvent);
        }
    }
}
