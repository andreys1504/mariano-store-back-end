using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.ApplicationLayer.ApplicationsServices
{
    public abstract class AppServiceBase
    {
        private readonly DependenciesAppServiceBase _dependencies;

        public AppServiceBase(
            DependenciesAppServiceBase dependencies)
        {
            _dependencies = dependencies;
        }

        protected async Task SendEventToQueueAsync(Event @event)
        {
            await _dependencies.MediatorHandler.SendEventToQueueAsync(@event);
        }
    }
}
