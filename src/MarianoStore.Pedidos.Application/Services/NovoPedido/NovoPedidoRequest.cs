using MarianoStore.Core.Messages;
using MediatR;

namespace MarianoStore.Pedidos.Application.Services.NovoPedido
{
    public class NovoPedidoRequest : Command, IRequest<bool>
    {
    }
}
