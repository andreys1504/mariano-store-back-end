using MarianoStore.Core.ApplicationLayer.ApplicationsServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Application.Services.BaixaEstoque
{
    public class BaixaEstoqueAppService : AppServiceBase, IRequestHandler<BaixaEstoqueRequest, bool>
    {
        public BaixaEstoqueAppService(AppServiceDependencies dependencies) : base(dependencies)
        {
        }

        public Task<bool> Handle(BaixaEstoqueRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
