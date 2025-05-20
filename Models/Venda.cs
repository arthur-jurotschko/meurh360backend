using System;
using System.ComponentModel.DataAnnotations;

namespace meurh360backend.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade vendida deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Required]
        public decimal ValorTotal { get; set; }

        [Required]
        public DateTime DataVenda { get; set; }
    }
}
