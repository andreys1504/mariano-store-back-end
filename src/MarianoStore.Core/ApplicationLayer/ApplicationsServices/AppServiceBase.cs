using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.ApplicationLayer.ApplicationsServices
{
    public abstract class AppServiceBase
    {
        private readonly AppServiceDependencies _dependencies;

        public AppServiceBase(
            AppServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public async Task SendEventToQueueAsync(Event @event)
        {
            await _dependencies.MediatorHandler.SendEventToQueueAsync(@event);
        }
    }
}
