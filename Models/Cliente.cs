using System.ComponentModel.DataAnnotations;

namespace meurh360backend.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Saldo { get; set; }

    }
}
