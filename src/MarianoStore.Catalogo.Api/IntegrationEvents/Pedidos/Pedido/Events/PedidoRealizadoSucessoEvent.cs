using MarianoStore.Core.Messages;
using System;

namespace MarianoStore.Catalogo.Api.IntegrationEvents.Pedidos.Pedido.Events
{
    public class PedidoRealizadoSucessoEvent : Message
    {
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
