using MarianoStore.Core.Messages;
using MediatR;
using System;

namespace MarianoStore.Pedidos.Domain.Events
{
    public class PedidoRealizadoSucessoEvent : Event, INotification
    {
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
