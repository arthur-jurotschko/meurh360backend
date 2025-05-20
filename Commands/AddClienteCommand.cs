using MediatR;
using meurh360backend.Models;

namespace meurh360backend.Commands
{
    public class AddClienteCommand : IRequest<Cliente>
    {
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
    }
}
