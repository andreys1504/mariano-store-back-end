using MarianoStore.Core.Messages;
using MediatR;

namespace MarianoStore.Notificacoes.Application.IntegrationEvents.Events
{
    public class PedidoRealizadoSucessoEvent : Event, INotification
    {
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
