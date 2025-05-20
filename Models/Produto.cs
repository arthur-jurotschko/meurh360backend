using System;
using System.ComponentModel.DataAnnotations;

namespace meurh360backend.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser menor que zero.")]
        public int QuantidadeEstoque { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }
    }
}
