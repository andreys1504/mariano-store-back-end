using MarianoStore.Core.Messages;
using MediatR;

namespace MarianoStore.Notificacoes.Application.IntegrationEvents.Events
{
    public class PagamentoRealizadoSucessoEvent : Event, INotification
    {
        public bool SucessoPagamento { get; set; }
    }
}
