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

        protected async Task SendCommandToQueueAsync(Command @request)
        {
            await _dependencies.MediatorHandler.SendCommandToQueueAsync(@request);
        }

        protected async Task SendCommandToHandlerAsync(object @request)
        {
            await _dependencies.MediatorHandler.SendCommandToHandlerAsync(@request);
        }
    }
}
