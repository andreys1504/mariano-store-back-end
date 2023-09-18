using MarianoStore.Core.Application.AspNet.ControllerBase;
using MarianoStore.Pedidos.Application.Services.NovoPedido;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MarianoStore.Pedidos.Api.Controllers
{
    [ApiController]
    [Route("pedido")]
    public class PedidoController : ApiControllerBase
    {
        public PedidoController(DependenciesApiControllerBase dependencies) : base(dependencies)
        {
        }

        [HttpPost]
        public async Task<IActionResult> NovoPedido()
        {
            var request = new NovoPedidoRequest();

            await base.SendCommandToQueueAsync(request);

            return Ok();
        }

        [HttpGet("{idPedido}")]
        public IActionResult Pedido(Guid idPedido)
        {
            return Ok(new
            {
                IdPedido = idPedido,
            });
        }
    }
}