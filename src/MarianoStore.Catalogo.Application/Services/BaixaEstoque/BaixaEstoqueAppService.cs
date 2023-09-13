using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Application.Services.BaixaEstoque
{
    public class BaixaEstoqueAppService : IRequestHandler<BaixaEstoqueRequest, bool>
    {
        public Task<bool> Handle(BaixaEstoqueRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
