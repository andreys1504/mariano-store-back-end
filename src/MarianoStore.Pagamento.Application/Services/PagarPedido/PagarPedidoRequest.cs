using MarianoStore.Core.Messages;
using MediatR;

namespace MarianoStore.Pagamento.Application.Services.PagarPedido
{
    public class PagarPedidoRequest : Command, IRequest<bool>
    {
    }
}
