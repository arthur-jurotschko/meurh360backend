using MediatR;
using meurh360backend.Commands;
using meurh360backend.Data;
using meurh360backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace meurh360backend.Handlers
{
    public class RealizarVendaCommandHandler : IRequestHandler<RealizarVendaCommand, Venda>
    {
        private readonly ApplicationDbContext _context;

        public RealizarVendaCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Venda> Handle(RealizarVendaCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Clientes.FindAsync(request.ClienteId);
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado.");

            var produto = await _context.Produtos.FindAsync(request.ProdutoId);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado.");

            if (produto.QuantidadeEstoque < request.Quantidade)
                throw new ArgumentException("Quantidade insuficiente em estoque.");

            var valorTotal = produto.Preco * request.Quantidade;
            if (cliente.Saldo < valorTotal)
                throw new ArgumentException("Saldo insuficiente para a compra.");

            // Atualizar saldo do cliente
            cliente.Saldo -= valorTotal;

            // Atualizar estoque do produto
            produto.QuantidadeEstoque -= request.Quantidade;

            var venda = new Venda
            {
                ClienteId = request.ClienteId,
                ProdutoId = request.ProdutoId,
                Quantidade = request.Quantidade,
                ValorTotal = valorTotal,
                DataVenda = DateTime.UtcNow
            };

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            return venda;
        }
    }
}
