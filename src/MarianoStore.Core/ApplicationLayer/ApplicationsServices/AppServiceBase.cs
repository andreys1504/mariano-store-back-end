using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.ApplicationLayer.ApplicationsServices
{
    public abstract class AppServiceBase
    {
        private readonly AppServiceDependencies _appServiceDependencies;

        public AppServiceBase(
            AppServiceDependencies appServiceDependencies)
        {
            _appServiceDependencies = appServiceDependencies;
        }

        public async Task SendEventToQueueAsync(Event @event)
        {
            await _appServiceDependencies.MediatorHandler.SendEventToQueueAsync(@event);
        }
    }
}
