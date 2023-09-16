using MarianoStore.Core.Messages;
using System.Threading.Tasks;

namespace MarianoStore.Core.Application.AspNet.ControllerBase
{
    public abstract class ApiControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly ApiControllerDependencies _dependencies;

        public ApiControllerBase(ApiControllerDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public async Task SendCommandToQueueAsync(Command @request)
        {
            await _dependencies.MediatorHandler.SendCommandToQueueAsync(@request);
        }

        public async Task SendCommandToHandlerAsync(object @request)
        {
            await _dependencies.MediatorHandler.SendCommandToHandlerAsync(@request);
        }
    }
}
