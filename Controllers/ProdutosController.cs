using Microsoft.AspNetCore.Mvc;
using MediatR;
using meurh360backend.Commands;
using meurh360backend.Queries;
using meurh360backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace meurh360backend.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var query = new GetProdutosQuery();
            var produtos = await _mediator.Send(query);
            return produtos.Any() ? Ok(produtos) : NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> AddProduto([FromBody] AddProdutoCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (command.Preco <= 0)
                return BadRequest(new { message = "O preço deve ser maior que zero." });

            if (command.QuantidadeEstoque < 0)
                return BadRequest(new { message = "A quantidade em estoque não pode ser negativa." });

            if (command.DataVencimento <= DateTime.UtcNow)
                return BadRequest(new { message = "A data de vencimento deve ser maior que a data atual." });

            var produto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }
    }
}
