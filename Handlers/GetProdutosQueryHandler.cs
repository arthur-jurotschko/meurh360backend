using MediatR;
using meurh360backend.Data;
using meurh360backend.Models;
using meurh360backend.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace meurh360backend.Handlers
{
    public class GetProdutosQueryHandler : IRequestHandler<GetProdutosQuery, List<Produto>>
    {
        private readonly ApplicationDbContext _context;

        public GetProdutosQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> Handle(GetProdutosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Produtos.ToListAsync();
        }
    }
}
