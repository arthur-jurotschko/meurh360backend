using MediatR;
using meurh360backend.Models;

namespace meurh360backend.Commands
{
    public class AddUsuarioCommand : IRequest<Usuario>
    {
      public string Nome { get; set; } = string.Empty;
      public string Email { get; set; } = string.Empty;

    }
}
