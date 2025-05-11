using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using meurh360backend.Commands;
using meurh360backend.Queries;
using meurh360backend.Data;
using meurh360backend.Models;

namespace meurh360backend.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public UsuariosController(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var query = new GetUsuariosQuery();
            var usuarios = await _mediator.Send(query);
            return usuarios.Any() ? Ok(usuarios) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioPorId(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "O ID deve ser positivo." });

            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario == null ? NotFound(new { message = "Usuário não encontrado." }) : Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> AddUsuario([FromBody] AddUsuarioCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool usuarioExistente = await _context.Usuarios.AnyAsync(u => u.Email == command.Email);
            if (usuarioExistente)
                return Conflict(new { message = "E-mail já cadastrado." });

            var usuario = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "ID não corresponde." });

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            usuario.Nome = command.Nome;
            usuario.Email = command.Email;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
