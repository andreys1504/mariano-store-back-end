using MarianoStore.Core.Mediator;

namespace MarianoStore.Core.ApplicationLayer.EventsHandlers
{
    public class EventHandlerDependencies
    {
        public EventHandlerDependencies(
            IMediatorHandler mediatorHandler)
        {
            MediatorHandler = mediatorHandler;
        }

        public IMediatorHandler MediatorHandler { get; }
    }
}
