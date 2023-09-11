using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pagamento.Application.IntegrationEvents.Events;
using MarianoStore.Pagamento.Domain.Events;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler
    {
        private readonly IPublisherRabbitMq _publisherRabbitMq;

        public PedidoRealizadoSucessoEventHandler( 
            IPublisherRabbitMq publisherRabbitMq)
        {
            _publisherRabbitMq = publisherRabbitMq;
        }

        public async Task Handle(PedidoRealizadoSucessoEvent @event)
        {
            //acessar gateway pagamento


            //notificar sistema pagamento realizado com sucesso
            var pagamentoRealizadoSucessoEvent = new PagamentoRealizadoSucessoEvent();

            await _publisherRabbitMq.PublishEventAsync(pagamentoRealizadoSucessoEvent);
        }
    }
}
