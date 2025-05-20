using MediatR;
using meurh360backend.Commands;
using meurh360backend.Models;
using meurh360backend.Data; 
using System.Threading;
using System.Threading.Tasks;

namespace meurh360backend.Handlers
{
	public class AddUsuarioCommandHandler : IRequestHandler<AddUsuarioCommand, Usuario>
	{
		private readonly ApplicationDbContext _context;

		public AddUsuarioCommandHandler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Usuario> Handle(AddUsuarioCommand request, CancellationToken cancellationToken)
		{
			var usuario = new Usuario
			{
				Nome = request.Nome,
				Email = request.Email
			};

			_context.Usuarios.Add(usuario);
			await _context.SaveChangesAsync(cancellationToken);

			return usuario;
		}
	}
}
