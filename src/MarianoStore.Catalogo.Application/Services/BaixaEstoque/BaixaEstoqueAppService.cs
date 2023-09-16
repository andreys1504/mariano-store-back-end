using MarianoStore.Core.ApplicationLayer.ApplicationsServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarianoStore.Catalogo.Application.Services.BaixaEstoque
{
    public class BaixaEstoqueAppService : AppServiceBase, IRequestHandler<BaixaEstoqueRequest, bool>
    {
        public BaixaEstoqueAppService(AppServiceDependencies appServiceDependencies) : base(appServiceDependencies)
        {
        }

        public Task<bool> Handle(BaixaEstoqueRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
