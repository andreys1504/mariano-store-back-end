using MarianoStore.Core.Services.Email;
using MarianoStore.Pedidos.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Application.EventsHandlers.PedidoRealizadoSucesso
{
    public class PedidoRealizadoSucessoEventHandler : INotificationHandler<PedidoRealizadoSucessoEvent>
    {
        private readonly IEmailService _emailService;

        public PedidoRealizadoSucessoEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(PedidoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            //Usuário receber e-mail que o pedido foi realizado, e seguiu para o fluxo de pagamento
            await _emailService.EnviarAsync();
        }
    }
}
