using MarianoStore.Core.Services.Email;
using MarianoStore.Pagamento.Domain.Events;
using System.Threading.Tasks;

namespace MarianoStore.Pagamento.Application.EventsHandlers.PagamentoRealizadoSucesso
{
    public class PagamentoRealizadoSucessoEventHandler
    {
        private readonly IEmailService _emailService;

        public PagamentoRealizadoSucessoEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(PagamentoRealizadoSucessoEvent @event)
        {
            //Usuário receber e-mail que o pagamento foi realizado
            await _emailService.EnviarAsync();
        }
    }
}
