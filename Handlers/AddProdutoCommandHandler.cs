using MediatR;
using meurh360backend.Commands;
using meurh360backend.Data;
using meurh360backend.Models;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace meurh360backend.Handlers
{
    public class AddProdutoCommandHandler : IRequestHandler<AddProdutoCommand, Produto>
    {
        private readonly ApplicationDbContext _context;

        public AddProdutoCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> Handle(AddProdutoCommand request, CancellationToken cancellationToken)
        {
            if (request.Preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.");

            if (request.QuantidadeEstoque < 0)
                throw new ArgumentException("A quantidade em estoque não pode ser negativa.");

            if (request.DataVencimento <= DateTime.UtcNow)
                throw new ArgumentException("A data de vencimento deve ser maior que a data atual.");

            var produto = new Produto
            {
                Nome = request.Nome,
                Preco = request.Preco,
                QuantidadeEstoque = request.QuantidadeEstoque,
                DataVencimento = request.DataVencimento
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return produto;
        }
    }
}
