using MarianoStore.Core.ApplicationLayer.EventsHandlers;
using MarianoStore.Notificacoes.Application.IntegrationEvents.Events;
using MediatR;

namespace MarianoStore.Notificacoes.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : EventHandlerBase, INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        public PedidoRealizadoSucessoEventHandler(EventHandlerDependencies eventHandlerDependencies) : base(eventHandlerDependencies)
        {
        }

        public Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            //TODO: criar appService para:
            //Usuário receber notificações que o pedido foi realizado, e seguiu para o fluxo de pagamento

            //Enviar e-mail
            //await _emailService.EnviarAsync();

            //Enviar Mensagem para WhatsApp

            //Enviar SMS

            return Task.CompletedTask;
        }
    }
}
