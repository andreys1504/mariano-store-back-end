using MarianoStore.Core.Messages;
using MediatR;

namespace MarianoStore.Catalogo.Application.Services.BaixaEstoque
{
    public class BaixaEstoqueRequest : Command, IRequest<bool>
    {
    }
}
