using MediatR;
using meurh360backend.Models;

namespace meurh360backend.Commands
{
    public class UpdateUsuarioCommand : IRequest<Unit>
    {
        public int Id { get; set; }public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
