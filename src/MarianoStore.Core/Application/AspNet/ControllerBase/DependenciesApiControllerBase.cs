using MarianoStore.Core.Mediator;

namespace MarianoStore.Core.Application.AspNet.ControllerBase
{
    public class DependenciesApiControllerBase
    {
        public DependenciesApiControllerBase(
            IMediatorHandler mediatorHandler)
        {
            MediatorHandler = mediatorHandler;
        }

        public IMediatorHandler MediatorHandler { get; }
    }
}
