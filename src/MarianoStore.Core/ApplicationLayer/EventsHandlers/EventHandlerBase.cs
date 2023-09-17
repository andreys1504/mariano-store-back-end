using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.ApplicationLayer.EventsHandlers
{
    public abstract class EventHandlerBase
    {
        private readonly DependenciesEventHandlerBase _dependencies;

        public EventHandlerBase(
            DependenciesEventHandlerBase dependencies)
        {
            _dependencies = dependencies;
        }

        protected async Task SendCommandToHandlerAsync(Command @request)
        {
            await _dependencies.MediatorHandler.SendCommandToHandlerAsync(@request);
        }
    }
}
