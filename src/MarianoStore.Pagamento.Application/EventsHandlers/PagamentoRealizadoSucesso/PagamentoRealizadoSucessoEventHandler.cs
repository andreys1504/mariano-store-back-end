using MarianoStore.Core.Services.Email;
using MarianoStore.Pagamento.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.EventsHandlers.PagamentoRealizadoSucesso
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
            //Usuário receber e-mail que o pagamento foi realizado
            await _emailService.EnviarAsync();
        }
    }
}
