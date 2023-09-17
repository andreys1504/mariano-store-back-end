using MarianoStore.Core.Mediator;

namespace MarianoStore.Core.ApplicationLayer.ApplicationsServices
{
    public class DependenciesAppServiceBase
    {
        public DependenciesAppServiceBase(
            IMediatorHandler mediatorHandler)
        {
            MediatorHandler = mediatorHandler;
        }

        public IMediatorHandler MediatorHandler { get; }
    }
}
