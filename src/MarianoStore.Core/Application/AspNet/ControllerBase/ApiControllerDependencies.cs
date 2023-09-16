using MarianoStore.Core.Mediator;

namespace MarianoStore.Core.Application.AspNet.ControllerBase
{
    public class ApiControllerDependencies
    {
        public ApiControllerDependencies(
            IMediatorHandler mediatorHandler)
        {
            MediatorHandler = mediatorHandler;
        }

        public IMediatorHandler MediatorHandler { get; }
    }
}
