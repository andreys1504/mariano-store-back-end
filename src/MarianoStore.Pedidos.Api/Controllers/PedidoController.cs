using MarianoStore.Core.Mediator;
using MarianoStore.Pedidos.Application.Services.NovoPedido;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Api.Controllers
{
    [ApiController]
    [Route("pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoController(
            IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> NovoPedido()
        {
            var request = new NovoPedidoRequest();

            await _mediatorHandler.SendCommandToQueueAsync(request);

            return Ok();
        }
    }
}