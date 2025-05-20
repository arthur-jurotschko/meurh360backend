using MediatR;
using meurh360backend.Models;
using System;

namespace meurh360backend.Commands
{
    public class AddProdutoCommand : IRequest<Produto>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
