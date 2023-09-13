using MarianoStore.Core.Infra.Services.Email;
using MarianoStore.Notificacoes.Application.IntegrationEvents.Events;
using MediatR;

namespace MarianoStore.Notificacoes.Application.EventsHandlers.PagamentoRealizadoSucesso
{
    public class PagamentoRealizadoSucessoEventHandler : INotificationHandler<PagamentoRealizadoSucessoEvent>
    {
        private readonly IEmailService _emailService;

        public PagamentoRealizadoSucessoEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(PagamentoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            //TODO: criar AppService para
            //Usuário receber notificação que o pagamento foi realizado

            //enviar e-mail
            await _emailService.EnviarAsync();
        }
    }
}
