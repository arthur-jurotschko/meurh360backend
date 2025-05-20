using Microsoft.AspNetCore.Mvc;
using MediatR;
using meurh360backend.Commands;
using meurh360backend.Models;
using System.Threading.Tasks;

namespace meurh360backend.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Venda>> RealizarVenda([FromBody] RealizarVendaCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var venda = await _mediator.Send(command);
            return CreatedAtAction(nameof(RealizarVenda), new { id = venda.Id }, venda);
        }
    }
}
