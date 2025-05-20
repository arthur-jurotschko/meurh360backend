using MediatR;
using meurh360backend.Models;
using System.Collections.Generic;

namespace meurh360backend.Queries
{
    public class GetProdutosQuery : IRequest<List<Produto>> { }
}
