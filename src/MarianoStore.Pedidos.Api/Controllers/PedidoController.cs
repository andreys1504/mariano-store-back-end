using MarianoStore.Core.Application.AspNet.ControllerBase;
using MarianoStore.Pedidos.Application.Services.NovoPedido;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Api.Controllers
{
    [ApiController]
    [Route("pedido")]
    public class PedidoController : ApiControllerBase
    {
        public PedidoController(ApiControllerDependencies dependencies) : base(dependencies)
        {
        }

        [HttpPost]
        public async Task<IActionResult> NovoPedido()
        {
            var request = new NovoPedidoRequest();

            await base.SendCommandToQueueAsync(request);

            return Ok();
        }
    }
}