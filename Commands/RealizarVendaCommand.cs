using MediatR;
using meurh360backend.Models;

namespace meurh360backend.Commands
{
    public class RealizarVendaCommand : IRequest<Venda>
    {
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
