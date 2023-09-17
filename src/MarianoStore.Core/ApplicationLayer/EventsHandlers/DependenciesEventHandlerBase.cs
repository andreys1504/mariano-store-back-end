using MarianoStore.Core.Mediator;

namespace MarianoStore.Core.ApplicationLayer.EventsHandlers
{
    public class DependenciesEventHandlerBase
    {
        public DependenciesEventHandlerBase(
            IMediatorHandler mediatorHandler)
        {
            MediatorHandler = mediatorHandler;
        }

        public IMediatorHandler MediatorHandler { get; }
    }
}
