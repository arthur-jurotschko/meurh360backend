using MediatR;
using meurh360backend.Commands;
using meurh360backend.Data;
using meurh360backend.Models;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace meurh360backend.Handlers
{
    public class AddClienteCommandHandler : IRequestHandler<AddClienteCommand, Cliente>
    {
        private readonly ApplicationDbContext _context;

        public AddClienteCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> Handle(AddClienteCommand request, CancellationToken cancellationToken)
        {
            if (request.Saldo < 0)
                throw new ArgumentException("O saldo inicial não pode ser negativo.");

            var cliente = new Cliente
            {
                Nome = request.Nome,
                Saldo = request.Saldo
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }
    }
}
