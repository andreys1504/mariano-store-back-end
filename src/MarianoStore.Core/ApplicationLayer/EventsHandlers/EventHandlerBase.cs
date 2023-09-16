using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.ApplicationLayer.EventsHandlers
{
    public abstract class EventHandlerBase
    {
        private readonly EventHandlerDependencies _dependencies;

        public EventHandlerBase(
            EventHandlerDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public async Task SendCommandToHandlerAsync(Command @request)
        {
            await _dependencies.MediatorHandler.SendCommandToHandlerAsync(@request);
        }
    }
}
