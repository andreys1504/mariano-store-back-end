using MarianoStore.Core.Infra.Services.Email;
using MarianoStore.Notificacoes.Application.IntegrationEvents.Events;
using MediatR;

namespace MarianoStore.Notificacoes.Application.IntegrationEvents.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        private readonly IEmailService _emailService;

        public PedidoRealizadoSucessoEventHandler(
            IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            //TODO: criar appService para:
            //Usuário receber notificações que o pedido foi realizado, e seguiu para o fluxo de pagamento

            //Enviar e-mail
            await _emailService.EnviarAsync();

            //Enviar Mensagem para WhatsApp

            //Enviar SMS
        }
    }
}
