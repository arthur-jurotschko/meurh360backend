using Microsoft.AspNetCore.Mvc;
using MediatR;
using meurh360backend.Commands;
using meurh360backend.Queries;
using meurh360backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace meurh360backend.Controllers
{
	[Route("api/clientes")]
	[ApiController]
	public class ClientesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ClientesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
		{
			var query = new GetClientesQuery();
			var clientes = await _mediator.Send(query);
			return clientes.Any() ? Ok(clientes) : NoContent();
		}

		[HttpPost]
		public async Task<ActionResult<Cliente>> AddCliente([FromBody] AddClienteCommand command)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (command.Saldo < 0)
				return BadRequest(new { message = "O saldo inicial não pode ser negativo." });

			var cliente = await _mediator.Send(command);
			return CreatedAtAction(nameof(GetClientes), new { id = cliente.Id }, cliente);
		}
	}
}
