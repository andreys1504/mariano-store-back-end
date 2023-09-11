using MarianoStore.Core.Services.RabbitMq.Publisher;
using MarianoStore.Pedidos.Application.Services.NovoPedido;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Api.Controllers
{
    [ApiController]
    [Route("pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IPublisherRabbitMq _publisherRabbitMq;

        public PedidoController(
            IPublisherRabbitMq publisherRabbitMq)
        {
            _publisherRabbitMq = publisherRabbitMq;
        }

        [HttpPost]
        public async Task<IActionResult> NovoPedido()
        {
            var request = new NovoPedidoRequest();

            await _publisherRabbitMq.PublishCommandAsync(request);

            return Ok();
        }
    }
}