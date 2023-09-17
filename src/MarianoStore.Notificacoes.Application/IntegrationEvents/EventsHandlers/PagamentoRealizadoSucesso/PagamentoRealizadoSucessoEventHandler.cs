using MarianoStore.Core.ApplicationLayer.EventsHandlers;
using MarianoStore.Notificacoes.Application.IntegrationEvents.Events;
using MediatR;

namespace MarianoStore.Notificacoes.Application.EventsHandlers.PagamentoRealizadoSucesso
{
    public class PagamentoRealizadoSucessoEventHandler : EventHandlerBase, INotificationHandler<PagamentoRealizadoSucessoEvent>
    {
        public PagamentoRealizadoSucessoEventHandler(DependenciesEventHandlerBase dependencies) 
            : base(dependencies)
        {
        }

        public Task Handle(PagamentoRealizadoSucessoEvent notification, CancellationToken cancellationToken)
        {
            //TODO: criar AppService para
            //Usuário receber notificação que o pagamento foi realizado

            //enviar e-mail
            //await _emailService.EnviarAsync();

            return Task.CompletedTask;
        }
    }
}
