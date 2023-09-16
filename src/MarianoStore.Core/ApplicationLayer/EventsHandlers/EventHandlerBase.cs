using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.ApplicationLayer.EventsHandlers
{
    public abstract class EventHandlerBase
    {
        private readonly EventHandlerDependencies _eventHandlerDependencies;

        public EventHandlerBase(
            EventHandlerDependencies eventHandlerDependencies)
        {
            _eventHandlerDependencies = eventHandlerDependencies;
        }

        public async Task SendCommandToHandlerAsync(Command @request)
        {
            await _eventHandlerDependencies.MediatorHandler.SendCommandToHandlerAsync(@request);
        }
    }
}
