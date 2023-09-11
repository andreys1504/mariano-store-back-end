using MarianoStore.Core.Messages;
using MediatR;

namespace MarianoStore.Pagamento.Domain.Events
{
    public class PagamentoRealizadoSucessoEvent : Event, INotification
    {
    }
}
