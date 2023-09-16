using MarianoStore.Core.Messages;
using MediatR;
using System;

namespace MarianoStore.Pagamento.Application.Services.PagarPedido
{
    public class PagarPedidoRequest : Command, IRequest<bool>
    {
        public Guid IdPedido { get; set; }
    }
}
